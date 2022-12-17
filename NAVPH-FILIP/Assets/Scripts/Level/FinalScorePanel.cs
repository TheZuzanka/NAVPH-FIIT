using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalScorePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI markText;
    [SerializeField] TextMeshProUGUI baseScoreText;
    [SerializeField] TextMeshProUGUI finalScoreText;

    public void Close()
    {
        Destroy(gameObject);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    // Display score on the panel
    public void DisplayScore(string mark, int points)
    {
        markText.SetText(mark);
        baseScoreText.SetText("Base Points: " + points);

        float finalPoints = points * GameLogicValues.MarkCoefficients[mark];
        finalScoreText.SetText("Final Points: " + finalPoints);
    }
}