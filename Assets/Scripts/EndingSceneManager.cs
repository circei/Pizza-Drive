using UnityEngine;
using TMPro;  // Import TextMesh Pro namespace
using UnityEngine.SceneManagement;  // Import the Scene Management namespace to load new scenes

public class EndingSceneManager : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;  // Reference to TextMesh Pro UI component for current score
    public TextMeshProUGUI highScoreText;  // Reference to TextMesh Pro UI component for high score
    public GameObject restartButton;  // Reference to the Restart Button UI element

    void Start()
    {
        // Retrieve the current score and high score from PlayerPrefs
        float currentScore = PlayerPrefs.GetFloat("CurrentScore", 0f);  // Default to 0 if not found
        float highScore = PlayerPrefs.GetFloat("HighScore", Mathf.Infinity);  // Default to Mathf.Infinity if not found

        // Update the UI with the current score and high score
        currentScoreText.text = "Current time: " + currentScore;
        highScoreText.text = "Highscore: " + highScore + " seconds";

        // Optionally, enable or disable the restart button if needed
        restartButton.SetActive(true);  // Ensure the restart button is visible
    }

    // This method will restart the game by reloading the main scene
    public void RestartGame()
    {
        // Reload the current scene (replace "SampleScene" with the name of your game scene)
        SceneManager.LoadScene("SampleScene");
        
        // Optionally, reset any PlayerPrefs if needed
        // PlayerPrefs.DeleteKey("CurrentScore");
        // PlayerPrefs.DeleteKey("HighScore");
    }
}
