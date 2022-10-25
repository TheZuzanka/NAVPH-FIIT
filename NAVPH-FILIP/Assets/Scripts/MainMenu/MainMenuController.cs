using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }
}
