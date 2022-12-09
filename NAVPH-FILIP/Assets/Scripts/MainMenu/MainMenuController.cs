using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void EnterSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void EnterAchievements()
    {
        SceneManager.LoadScene("Achievements");
    }
}