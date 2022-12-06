using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private Camera mainCamera;

    // public = player sets self as reference when spawned
    public List<Enemy> enemies;
    public Boss boss;

    // public = player accesses this attribute
    public float toKillY = -6;

    private void MoveCameraWhenPlayerNotInViewport()
    {
        Vector3 screenPositionOfPlayer = mainCamera.WorldToScreenPoint(player.transform.position);
        if (screenPositionOfPlayer.x < 0)
        {
            // player is off the left boundary
            
            mainCamera.transform.position -= new Vector3(20, 0, 0);
        } 
        else if (screenPositionOfPlayer.x > Screen.width)
        {
            // player is off the right boundary
            
            mainCamera.transform.position += new Vector3(20, 0, 0);
        }
    }
    

    public void Update()
    {
        MoveCameraWhenPlayerNotInViewport();
    }

    public void Start()
    {
        player = Instantiate(player, new Vector2(-11, -3), Quaternion.identity, levelContainer);
        
        // this is an observer of health system
        player.heartDelegate += OnHeartsChanged;
        
        player.SetSpeed(3, 5);
        score.text = "Score: " + player.score;

        player.SetLevelManager(this);
    }

    private void OnHeartsChanged(int heartCount)
    {
        if (heartCount == 0)
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}