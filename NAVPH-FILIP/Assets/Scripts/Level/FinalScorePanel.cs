using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalScorePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI markText;
    [SerializeField] TextMeshProUGUI baseScoreText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] float[] markCoefficients;
   
    // Close the panel
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
        baseScoreText.SetText("Base Points: "+points.ToString());


        Dictionary<string,float> markCoefficientsDict = new Dictionary<string, float>
        {
            { "E", markCoefficients[0] },
            { "D", markCoefficients[1] },
            { "C", markCoefficients[2] },
            { "B", markCoefficients[3] },
            { "A", markCoefficients[4] }
        };

        float final_points = points * markCoefficientsDict[mark];
        finalScoreText.SetText("Final Points: " + final_points.ToString());
    }
}
