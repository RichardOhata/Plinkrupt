using UnityEngine;

public class HexGridSnap : MonoBehaviour
{
    public float hexSize = 1f; // Size of each hex tile
    private float width;
    private float height;

    void Start()
    {
        width = hexSize * 1.5f;  // Distance between hex centers in x
        height = hexSize * Mathf.Sqrt(3); // Distance in z
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click to snap
        {
            Vector3 worldPos = GetMouseWorldPosition();
            Vector2Int hexCoords = WorldToHex(worldPos);
            Vector3 snappedPos = HexToWorld(hexCoords.x, hexCoords.y);
            transform.position = snappedPos;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    Vector2Int WorldToHex(Vector3 worldPos)
    {
        float q = worldPos.x / width;
        float r = (worldPos.z / height) - (q / 2f);
        
        return HexRound(q, r);
    }

    Vector2Int HexRound(float q, float r)
    {
        float x = q;
        float y = -q - r;
        float z = r;

        int rx = Mathf.RoundToInt(x);
        int ry = Mathf.RoundToInt(y);
        int rz = Mathf.RoundToInt(z);

        float xDiff = Mathf.Abs(rx - x);
        float yDiff = Mathf.Abs(ry - y);
        float zDiff = Mathf.Abs(rz - z);

        if (xDiff > yDiff && xDiff > zDiff)
        {
            rx = -ry - rz;
        }
        else if (yDiff > zDiff)
        {
            ry = -rx - rz;
        }
        else
        {
            rz = -rx - ry;
        }

        return new Vector2Int(rx, rz);
    }

    Vector3 HexToWorld(int q, int r)
    {
        float x = q * width;
        float z = r * height + (q * height / 2f);
        return new Vector3(x, transform.position.y, z);
    }
}
