using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public GameObject levelContainer;
    public Transform playerSpawnPosition;

    void Start()
    {
        Debug.Log("x = " + levelContainer.transform.position.x);
        player = Instantiate(player, playerSpawnPosition.position, 
            Quaternion.identity, levelContainer.transform);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}