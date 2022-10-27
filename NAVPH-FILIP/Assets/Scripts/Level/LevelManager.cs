using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public GameObject levelContainer;
    public List<Heart> hearts;
    public GameObject heartSpawner1;
    public GameObject heartSpawner2;
    public FullHeart heart;
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DrawHeart()
    {
        if (player.GetHearts() == 1)
        {
            Instantiate(heart, new Vector2(0, 0), Quaternion.identity, heartSpawner2.transform);
        }
        else if (player.GetHearts() == 0)
        {
            Instantiate(heart, new Vector2(0, 0), Quaternion.identity, heartSpawner1.transform);
        }
    }
}