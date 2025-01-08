using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Replace "GameScene" with the name of your main gameplay scene.
        SceneManager.LoadScene("SampleScene");
    }

    public void ToggleMute()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
