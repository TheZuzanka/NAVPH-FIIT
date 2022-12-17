using TMPro;
using UnityEngine;

public class DynamicScoreAchievement : MonoBehaviour, IDynamicDescription
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    public void SetDynamicDescription()
    {
        title.SetText("Completed with nice score (" 
                      + Achievements.PointAchievementThreshold + ")");
        description.SetText("You completed the game with score of " 
                            + Achievements.PointAchievementThreshold + ".");
    }
}