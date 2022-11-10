using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public FullHeart heart1;
    public FullHeart heart2;
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
            heart2.gameObject.SetActive(true);
        }
        else if (player.GetHearts() == 0)
        {
            heart1.gameObject.SetActive(true);
        }
    }

    public void DestroyHeart()
    {
        switch (player.GetHearts())
        {
            case 0:
                ReturnToMainMenu();
                break;
                
            case 1: 
                heart2.gameObject.SetActive(false);
                break;
            case 2:
                heart1.gameObject.SetActive(false);
                break;
        }
    }
}