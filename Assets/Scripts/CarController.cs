using UnityEngine;
using TMPro;  // Import TextMesh Pro namespace
using UnityEngine.SceneManagement;  // Import the Scene Management namespace to load new scenes

public class CarController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Reference to the TextMesh Pro UI component for the score
    public TextMeshProUGUI timerText;  // Reference to the TextMesh Pro UI component for the timer
    
    private int deliveries = 0;  // Score variable to track deliveries
    private float timeElapsed = 0f;  // Timer variable (counts up)
    private bool gameEnded = false;  // Flag to check if the game has ended
    private static float highScore = Mathf.Infinity;  // Static variable to store the highscore

    void Start()
    {
        // Load highscore from PlayerPrefs (default to Mathf.Infinity if not found)
        highScore = PlayerPrefs.GetFloat("HighScore", Mathf.Infinity);

        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        // Only update the timer if the game hasn't ended
        if (!gameEnded)
        {
            // Increase timeElapsed every frame based on Time.deltaTime
            timeElapsed += Time.deltaTime;

            // Update the timer display
            UpdateTimerText();
        }

        // Check if the player presses the "R" key to restart the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // Method to increase the score (called when the car reaches a checkpoint)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint") && !gameEnded)  // Check if the car collides with a checkpoint and the game hasn't ended
        {
            deliveries++;  // Increase the score
            UpdateScoreText();
            Destroy(other.gameObject);  // Destroy the checkpoint after collision

            // If the player reaches 10 deliveries, end the game
            if (deliveries >= 10)
            {
                EndGame();
            }
        }
    }

    // Method to end the game
    private void EndGame()
    {
        gameEnded = true;  // Set the game-ended flag to true

        // Store the current score (deliveries) in PlayerPrefs
        PlayerPrefs.SetFloat("CurrentScore", timeElapsed);

        // Check if the current time is a new highscore
        if (timeElapsed < highScore)
        {
            highScore = timeElapsed;  // Update the highscore if this time is better
            PlayerPrefs.SetFloat("HighScore", highScore);  // Save the new highscore to PlayerPrefs
        }

        // Optionally, wait before transitioning to a new scene
        Invoke("GoToEndingScene", 3f);  // Wait for 3 seconds before transitioning to the ending scene
    }

    // Method to update the UI text displaying the score
    private void UpdateScoreText()
    {
        scoreText.text = "Deliveries: " + deliveries;
    }

    // Method to update the UI text displaying the timer (counting up)
    private void UpdateTimerText()
    {
        timerText.text = "Time: " + timeElapsed;  // Display the elapsed time rounded to an integer
    }

    // Method to load the ending scene
    private void GoToEndingScene()
    {
        // Replace "Ending" with the name of the actual ending scene in your project
        SceneManager.LoadScene("Ending");
    }

    // Method to restart the game
    private void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        // Optionally, reset any PlayerPrefs if needed for a fresh start
        // PlayerPrefs.DeleteKey("CurrentScore");
        // PlayerPrefs.DeleteKey("HighScore");
    }
}
