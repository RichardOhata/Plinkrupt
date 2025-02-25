using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    public int blockType; // Type of block (1-6)
    public int collisionThreshold = 5; // Number of hits before destruction
    private int collisionCount = 0; // Tracks collisions with the ball

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            collisionCount++;

            if (collisionCount >= collisionThreshold)
            {
                SelfDestruct();
            }
        }
    }

    void SelfDestruct()
    {
        Destroy(gameObject); // Destroys the block
    }
}
