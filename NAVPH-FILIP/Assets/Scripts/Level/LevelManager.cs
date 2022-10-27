using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public GameObject levelContainer;
    public Transform playerSpawnPosition;
    public List<Heart> hearts; 

    void Start()
    {
        Debug.Log("x = " + levelContainer.transform.position.x);
        player = Instantiate(player, playerSpawnPosition.position, 
            Quaternion.identity, levelContainer.transform);

        foreach (var heart in hearts)
        {
            heart.SetPlayer(player);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}