using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI score;
    
    // public = player accesses this attribute
    public float toKillY = -6;

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
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}