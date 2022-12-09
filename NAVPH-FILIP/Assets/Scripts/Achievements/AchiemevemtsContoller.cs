using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchiemevemtsContoller : MonoBehaviour
{
    // this class controls achievement window

    [SerializeField] private List<Image> imageElements;
    [SerializeField] private TextMeshProUGUI scoreAchievementText;

    private void SetCompleted()
    {
        var index = 0;

        foreach (var element in imageElements)
        {
            if (Achievements.achievements.ElementAt(index).Value)
            {
                element.color = new Color32(255, 217, 102, 255);
            }

            index++;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        SetCompleted();
        scoreAchievementText.text = "Completed with score of " + Achievements.PointAchievementThreshold + ".";
    }
}