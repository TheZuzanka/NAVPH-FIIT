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
    [SerializeField] private Finish finish;
    [SerializeField] private AchievementsManager achievementsManager;
    [SerializeField] private PointCounter pointCounter;
    [SerializeField] private FinalScorePanel finalScorePanel;

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

        // this is an observer of shield system
        player.shieldDelegate += OnShieldActivated;

        // this is an observer of coffee system
        player.coffeeDelegate += OnCoffeeActivated;

        // this is an observer of finish system
        finish.isFinishedDelegate += OnFinished;

        achievementsManager.SetFinishedPlayerAchievement(Settings.SelectedPerson);

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

    private void OnFinished(bool isFinished)
    {
        if (isFinished)
        {
            achievementsManager.FinishWithMark(boss.GetFinalMark());
            achievementsManager.achievementsDelegate(true);

            OpenFinalScorePanel();
            Time.timeScale = 0.0f; // Stop time
        }
    }

    private void OpenFinalScorePanel()
    {
        FinalScorePanel finalScorePanelInstance = Instantiate(finalScorePanel);
        finalScorePanelInstance.DisplayScore(boss.GetFinalMark(), pointCounter.GetPoints());
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnShieldActivated(bool isActivated)
    {
        if (isActivated)
        {
            ShieldTimer timer = player.GetComponent<ShieldTimer>();
            timer.enabled = true;

            player.shieldImage.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            player.shieldImage.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnCoffeeActivated(bool isActivated)
    {
        if (isActivated)
        {
            CoffeeTimer timer = player.GetComponent<CoffeeTimer>();
            timer.enabled = true;

            Vector2 playerSpeed = player.GetSpeed();
            player.SetSpeed(GameLogicValues.CoffeeEffectMultiplier * playerSpeed.x,
                GameLogicValues.CoffeeEffectMultiplier * playerSpeed.y);
        }
        else
        {
            Vector2 playerSpeed = player.GetSpeed();
            player.SetSpeed(playerSpeed.x / GameLogicValues.CoffeeEffectMultiplier,
                playerSpeed.y / GameLogicValues.CoffeeEffectMultiplier);
        }
    }
}