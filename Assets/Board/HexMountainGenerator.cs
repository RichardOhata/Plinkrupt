using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMountainGenerator : MonoBehaviour
{

    [Header("Hex Grid Generation")]
    public GameObject[] hexPrefabs; // Array of 6 prefabs
    public int baseSize = 5;  // Width of the base hex layer
    public int height = 5;     // Number of layers
    public float hexSize = 1f; // Size of each hex block
    public float roughnessFactor = 0.3f; // How chaotic the shape is (0 = smooth, 1 = very rough)
    public float heightVariation = 0.2f; // Random height offsets
    private Dictionary<Vector2Int, int> prefabMap = new Dictionary<Vector2Int, int>(); // Stores prefab types

    [Header("Scoring Area Generation")]
    public GameObject scoringAreaPrefab; //placeholder prefab
    public List<GameObject> scoringAreaGameObject;
    public ScoringAreaConfigSO[] scoringAreaConfigSOs; // Array of scoring area configurations
    public int numOfScoringAreasPerSide = 2; // Number of scoring areas to generate
    public int HeightOffset = -6; // Height offset for scoring areas

    private void Start()
    {
        GenerateMountain();
        GenerateScoringAreas();
    }

    void GenerateScoringAreas(){
        if (scoringAreaPrefab == null)
        {
            Debug.LogWarning("Scoring Area Prefab is not assigned!");
            return;
        }
        float scoreAreaBaseLength = (baseSize + 1) / hexSize;
        print("Score Area Base Length: " + scoreAreaBaseLength);
        float scoreAreaSizeOffset = scoreAreaBaseLength / (numOfScoringAreasPerSide - 1);
        print("Score Area Size Offset: " + scoreAreaSizeOffset);
        List<Vector2> hexPositions = GenerateHexGridScoring(scoreAreaBaseLength, scoreAreaSizeOffset);

        foreach (Vector2 pos in hexPositions){
            // Generate scoring area prefab
            Vector3 worldPos = HexToWorldPosition(pos.x, pos.y, -HeightOffset);

            // Instantiate scoring area prefab
            GameObject scoreAreaPrefab = Instantiate(scoringAreaPrefab, worldPos, Quaternion.identity, transform);

            // Scale scoring area prefab
            scoreAreaPrefab.transform.localScale = new Vector3(scoreAreaSizeOffset, scoreAreaSizeOffset, scoreAreaSizeOffset);
            scoreAreaPrefab.TryGetComponent<IElementScoreInteraction>(out IElementScoreInteraction scoringArea);

            // Load a random scoring area configuration
            ElementMultiplierConfig scoringAreaConfig = scoringAreaConfigSOs[Random.Range(0, scoringAreaConfigSOs.Length)].getScoringAreaConfig();
            scoringArea.LoadSetting(scoringAreaConfig);
            scoringAreaGameObject.Add(scoreAreaPrefab);
        }
    }
    List<Vector2> GenerateHexGridScoring(float radius, float offsets)
    {
        List<Vector2> hexes = new List<Vector2>();

        for (float q = -radius; q < radius + offsets; q += offsets)
        {
            float r1 = Mathf.Max(-radius, -q - radius);
            float r2 = Mathf.Min(radius, -q + radius);
            for (float r = r1; r < r2 + offsets; r += offsets)
            {
                hexes.Add(new Vector2(q, r));
            }
        }
        return hexes;
    }

    void GenerateMountain()
    {
        for (int y = 0; y < height; y++) // Loop through layers
        {
            int layerSize = baseSize - y; // Reduce size per layer
            if (layerSize <= 0) break;    // Stop when no more layers fit

            List<Vector2Int> hexPositions = GenerateHexGrid(layerSize, y);

            // Ensure the top layer has a center block
            if (y == height - 1 && !hexPositions.Contains(Vector2Int.zero))
            {
                hexPositions.Add(Vector2Int.zero);
            }

            foreach (Vector2Int pos in hexPositions)
            {
                Vector3 worldPos = HexToWorldPosition(pos.x, pos.y, y);
                GameObject selectedPrefab = SelectClusteredPrefab(pos);
                Instantiate(selectedPrefab, worldPos, Quaternion.identity, transform);
            }
        }
    }


    List<Vector2Int> GenerateHexGrid(int radius, int layer)
    {
        List<Vector2Int> hexes = new List<Vector2Int>();

        for (int q = -radius; q <= radius; q++)
        {
            int r1 = Mathf.Max(-radius, -q - radius);
            int r2 = Mathf.Min(radius, -q + radius);
            for (int r = r1; r <= r2; r++)
            {
                float removalChance = Mathf.Lerp(0.05f, roughnessFactor, (float)layer / height); 
                if (Random.value > removalChance) 
                {
                    hexes.Add(new Vector2Int(q, r));
                }
            }
        }
        return hexes;
    }

    Vector3 HexToWorldPosition(float q, float r, float y)
    {
        float x = hexSize * 1.5f * q;
        float z = hexSize * Mathf.Sqrt(3) * (r + q / 2f);
        
        float elevation = (y * hexSize * 1.1f) + Random.Range(-heightVariation, heightVariation);

        // Ensure the topmost center is at least as tall as surrounding blocks
        if (y == height - 1 && q == 0 && r == 0) 
        {
            elevation += hexSize * 0.3f; // Raise or keep it level with surroundings
        }

        return new Vector3(x, elevation, z);
    }

    GameObject SelectClusteredPrefab(Vector2Int pos)
    {
        // Check neighboring hexes to determine the most common nearby prefab
        List<Vector2Int> neighbors = new List<Vector2Int>
        {
            new Vector2Int(pos.x + 1, pos.y), new Vector2Int(pos.x - 1, pos.y),
            new Vector2Int(pos.x, pos.y + 1), new Vector2Int(pos.x, pos.y - 1),
            new Vector2Int(pos.x + 1, pos.y - 1), new Vector2Int(pos.x - 1, pos.y + 1)
        };

        Dictionary<int, int> prefabCount = new Dictionary<int, int>();
        foreach (var neighbor in neighbors)
        {
            if (prefabMap.TryGetValue(neighbor, out int neighborPrefab))
            {
                if (!prefabCount.ContainsKey(neighborPrefab))
                    prefabCount[neighborPrefab] = 0;
                prefabCount[neighborPrefab]++;
            }
        }

        int selectedPrefabIndex;
        if (prefabCount.Count > 0 && Random.value > 0.3f) // 70% chance to match neighbors
        {
            int mostCommonPrefab = -1, maxCount = 0;
            foreach (var kvp in prefabCount)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    mostCommonPrefab = kvp.Key;
                }
            }
            selectedPrefabIndex = mostCommonPrefab;
        }
        else
        {
            selectedPrefabIndex = Random.Range(0, hexPrefabs.Length);
        }

        prefabMap[pos] = selectedPrefabIndex; // Store assigned prefab
        return hexPrefabs[selectedPrefabIndex];
    }
}
