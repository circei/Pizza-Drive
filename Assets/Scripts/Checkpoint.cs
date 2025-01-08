using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Reference to the GameManager
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the car (or other player) enters the checkpoint
        if (other.CompareTag("Car"))
        {
            // Increase the score in GameManager
            gameManager.IncreaseScore();

            // Destroy the checkpoint after it is passed
            Destroy(gameObject);
        }
    }
}
