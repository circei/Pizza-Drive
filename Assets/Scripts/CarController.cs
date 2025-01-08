using UnityEngine;
using TMPro;  // Import TextMesh Pro namespace
using UnityEngine.SceneManagement;  // Import the Scene Management namespace to load new scenes

public class CarController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Reference to the TextMesh Pro UI component for the score
    public TextMeshProUGUI timerText;  // Reference to the TextMesh Pro UI component for the timer
    public TextMeshProUGUI endMessageText;  // Reference to the TextMesh Pro UI component for the end message
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
        endMessageText.gameObject.SetActive(false);  // Hide end message initially
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
    }

    // Method to increase the score (called when the car reaches a checkpoint)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint") && !gameEnded)  // Check if the car collides with a checkpoint and the game hasn't ended
        {
            deliveries++;  // Increase the score
            UpdateScoreText();
            Destroy(other.gameObject);  // Destroy the checkpoint after collision (fixed here)

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
    PlayerPrefs.SetInt("CurrentScore", deliveries);

    // Check if the current time is a new highscore
    if (timeElapsed < highScore)
    {
        highScore = timeElapsed;  // Update the highscore if this time is better
        PlayerPrefs.SetFloat("HighScore", highScore);  // Save the new highscore to PlayerPrefs
    }

    // Show the end message with the score and time taken
    endMessageText.gameObject.SetActive(true);
    endMessageText.text = $"Congratulations!\nDeliveries: {deliveries}\nTime: {Mathf.Round(timeElapsed)} seconds\nHighscore: {Mathf.Round(highScore)} seconds";

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
        timerText.text = "Time: " + Mathf.Round(timeElapsed);  // Display the elapsed time rounded to an integer
    }

    // Method to load the ending scene
    private void GoToEndingScene()
    {
        // Replace "EndingScene" with the name of the actual ending scene in your project
        SceneManager.LoadScene("EndingScene");
    }
}
