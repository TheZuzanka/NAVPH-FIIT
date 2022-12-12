using System;
using TMPro;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private int points;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] private AchievementsManager achievementsManager;

    public delegate void PointCounterDelegate(int score);

    public PointCounterDelegate pointCounterDelegate;

    // Increase player's score and display it
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        pointCounterDelegate(points);

        scoreText.SetText("Score: {0}", points);
    }

    private void Start()
    {
        pointCounterDelegate += OnPointsChange;

        points = 0;
        pointCounterDelegate(points);
    }

    private void OnPointsChange(int score)
    {
        if (score >= Achievements.PointAchievementThreshold)
        {
            achievementsManager.SetScoreAchievement();
        }
    }

    public int GetPoints()
    {
        return points;
    }
}