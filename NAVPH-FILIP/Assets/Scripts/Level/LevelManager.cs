using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public GameObject levelContainer;
    public List<FullHeart> hearts;
    public GameObject heartSpawner1;
    public GameObject heartSpawner2;
    public FullHeart heart;
    public TextMeshProUGUI score;

    public void Start()
    {
        score.text = "Score: " + player.score;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void DrawHeart()
    {
        if (player.GetHearts() == 1)
        {
            var createdHeart = Instantiate(heart, new Vector2(heartSpawner2.transform.position.x, 
                heartSpawner2.transform.position.y), Quaternion.identity, heartSpawner2.transform);
            hearts.Add(createdHeart);
        }
        else if (player.GetHearts() == 0)
        {
            var createdHeart = Instantiate(heart, new Vector2(heartSpawner1.transform.position.x, 
                heartSpawner1.transform.position.y), Quaternion.identity, heartSpawner1.transform);
            hearts.Add(createdHeart);
        }
    }

    public void DestroyHeart()
    {
        if (player.GetHearts() == 0)
        {
            ReturnToMainMenu();
        }
        else
        {
            Destroy(hearts[^1].GameObject());
        }
    }
}