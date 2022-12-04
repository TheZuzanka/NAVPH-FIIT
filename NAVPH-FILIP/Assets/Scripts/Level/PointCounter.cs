using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private int points = 0;
    [SerializeField] TextMeshProUGUI scoreText;

    // Increase player's score and display it
    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        scoreText.SetText("Score: {0}", points);
    }

    public int GetPoints()
    {
        return points;
    }
}
