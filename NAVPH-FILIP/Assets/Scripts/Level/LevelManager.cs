using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Transform levelContainer;

    // public = player sets self as reference when spawned
    public List<Enemy> enemies;
    public Boss boss;

    // public = player accesses this attribute
    public float toKillY = -6;

    private void MoveCameraWhenPlayerNotInViewport()
    {
        Vector3 screenPositionOfPlayer = Camera.main.WorldToScreenPoint(player.transform.position);
        if (screenPositionOfPlayer.x < 0)
        {
            // player is off the left boundary
            
            Camera.main.transform.position -= new Vector3(20, 0, 0);
        } 
        else if (screenPositionOfPlayer.x > Screen.width)
        {
            // player is off the right boundary
            
            Camera.main.transform.position += new Vector3(20, 0, 0);
        }
    }
    

    public void Update()
    {
        MoveCameraWhenPlayerNotInViewport();
    }

    public void Start()
    {
        player = Instantiate(player, new Vector2(-11, -3), Quaternion.identity, levelContainer);
        player.SetSpeed(3, 5);
        score.text = "Score: " + player.score;

        List<GameObject> hearts = new List<GameObject>();
        var foundHearts = FindObjectsOfType<FullHeart>();
        
        foreach (var heart in foundHearts)
        {
            hearts.Add(heart.gameObject);
        }
        
        // list is reversed so hearts decrease from the end of list
        hearts.Reverse();
        
        player.SetPlayersAttributesFromScene(this, hearts);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}