using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI score;
    public float toKillY;

    public void Update()
    {
        if (player.transform.position.x >= Camera.main.transform.position.x)
        {
            Camera.main.transform.position += new Vector3(10, 0, 0);
        }
    }

    public void Start()
    {
        score.text = "Score: " + player.score;
        toKillY = -6;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}