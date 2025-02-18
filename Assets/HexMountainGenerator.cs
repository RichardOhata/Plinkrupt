using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMountainGenerator : MonoBehaviour
{
    public GameObject[] hexPrefabs; // Array to hold 6 different hex prefabs
    public int baseSize = 5;  // Width of the base hex layer
    public int height = 5;     // Number of layers
    public float hexSize = 1f; // Size of each hex block
    public float roughnessFactor = 0.3f; // Higher = more irregular shape
    public float heightVariation = 0.2f; // Random height offsets
    private Dictionary<Vector2Int, int> regionMap = new Dictionary<Vector2Int, int>(); // Stores prefab choices

    private void Start()
    {
        GenerateMountain();
    }

    void GenerateMountain()
    {
        for (int y = 0; y < height; y++) // Loop through layers
        {
            int layerSize = baseSize - y; // Reduce size per layer
            if (layerSize <= 0) break;    // Stop when no more layers fit

            List<Vector2Int> hexPositions = GenerateHexGrid(layerSize, y);

            foreach (Vector2Int pos in hexPositions)
            {
                Vector3 worldPos = HexToWorldPosition(pos.x, pos.y, y);
                GameObject selectedPrefab = SelectPrefab(pos);
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

    Vector3 HexToWorldPosition(int q, int r, int y)
    {
        float x = hexSize * 1.5f * q;
        float z = hexSize * Mathf.Sqrt(3) * (r + q / 2f);
        float elevation = (y * hexSize * 1.1f) + Random.Range(-heightVariation, heightVariation);
        return new Vector3(x, elevation, z);
    }

    GameObject SelectPrefab(Vector2Int pos)
    {
        // Check if a nearby hex already has a prefab assigned
        List<Vector2Int> neighbors = new List<Vector2Int>
        {
            pos + new Vector2Int(1, 0),
            pos + new Vector2Int(-1, 0),
            pos + new Vector2Int(0, 1),
            pos + new Vector2Int(0, -1),
            pos + new Vector2Int(1, -1),
            pos + new Vector2Int(-1, 1)
        };

        List<int> nearbyPrefabIndices = new List<int>();

        foreach (Vector2Int neighbor in neighbors)
        {
            if (regionMap.ContainsKey(neighbor))
            {
                nearbyPrefabIndices.Add(regionMap[neighbor]);
            }
        }

        int selectedIndex;
        if (nearbyPrefabIndices.Count > 0 && Random.value > 0.5f) // 50% chance to pick a similar type
        {
            selectedIndex = nearbyPrefabIndices[Random.Range(0, nearbyPrefabIndices.Count)];
        }
        else
        {
            selectedIndex = Random.Range(0, hexPrefabs.Length);
        }

        regionMap[pos] = selectedIndex; // Store the chosen prefab for future reference
        return hexPrefabs[selectedIndex];
    }
}
