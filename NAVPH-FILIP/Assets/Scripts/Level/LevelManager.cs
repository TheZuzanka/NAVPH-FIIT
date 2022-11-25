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

    public void Update()
    {
        Vector3 screenPosOfPlayer = Camera.main.WorldToScreenPoint(player.transform.position);
        if (screenPosOfPlayer.x < 0)
        {
            Camera.main.transform.position -= new Vector3(20, 0, 0);
        } 
        else if (screenPosOfPlayer.x > Screen.width)
        {
            Camera.main.transform.position += new Vector3(20, 0, 0);
        }
    }

    public void Start()
    {
        player = Instantiate(player, new Vector2(-11, -3), Quaternion.identity, levelContainer);

        List<GameObject> hearts = new List<GameObject>();
        var foundHearts = FindObjectsOfType<FullHeart>();
        foreach (var heart in foundHearts)
        {
            hearts.Add(heart.gameObject);
        }
        
        player.SetPlayersAttributesFromScene(this, hearts);
        score.text = "Score: " + player.score;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}