using System;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    // this class covers the logic for gaining achievements in the game
    public delegate void AchievementsDelegate(bool isFinished);

    public AchievementsDelegate achievementsDelegate;

    private enum Player
    {
        Filip,
        Zuzka
    }

    private void OnZuzkaFinished(bool isFinished)
    {
        if (isFinished)
        {
            Achievements.achievements["CompletedAsZuzka"] = isFinished;
        }
    }

    private void OnFilipFinished(bool isFinished)
    {
        if (isFinished)
        {
            Achievements.achievements["CompletedAsFilip"] = isFinished;
        }
    }

    public void SetFinishedAchievement(int playerIndex)
    {
        if (playerIndex == (int) Player.Filip)
        {
            achievementsDelegate += OnFilipFinished;
        }
        else if (playerIndex == (int) Player.Zuzka)
        {
            achievementsDelegate += OnZuzkaFinished;
        }
    }
}