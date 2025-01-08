using UnityEngine;
using TMPro;  // Import TextMesh Pro namespace

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Reference to the TextMesh Pro UI component for the score
    public TextMeshProUGUI timerText;  // Reference to the TextMesh Pro UI component for the timer
    private int deliveries = 0;  // Score variable to track deliveries
    private float timeElapsed = 0f;  // Timer variable (counts up)

    void Start()
    {
        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        // Increase timeElapsed every frame based on Time.deltaTime
        timeElapsed += Time.deltaTime;

        // Update the timer display
        UpdateTimerText();
    }

    // Method to increase the score (called when the car reaches a checkpoint)
    public void IncreaseScore()
    {
        deliveries++;
        UpdateScoreText();
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
}
