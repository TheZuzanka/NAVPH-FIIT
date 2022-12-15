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

    private enum Mark
    {
        A,
        B,
        C,
        D,
        E
    }

    private void OnZuzkaFinished(bool isFinished)
    {
        Achievements.achievements["CompletedAsZuzka"] = isFinished;
    }

    private void OnFilipFinished(bool isFinished)
    {
        Achievements.achievements["CompletedAsFilip"] = isFinished;
    }

    private void OnScoreAchieved(bool isFinished)
    {
        Achievements.achievements["CompleteWithScore"] = isFinished;
    }

    public void SetScoreAchievement()
    {
        achievementsDelegate += OnScoreAchieved;
    }

    public void SetFinishedPlayerAchievement(int playerIndex)
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

    public void FinishWithMark(string mark)
    {
        switch ((Mark) Enum.Parse(typeof(Mark), mark.ToUpper()))
        {
            case Mark.A:
                achievementsDelegate += OnFinishedWithA;
                break;
            case Mark.B:
                achievementsDelegate += OnFinishedWithB;
                break;
            case Mark.C:
                achievementsDelegate += OnFinishedWithC;
                break;
            case Mark.D:
                achievementsDelegate += OnFinishedWithD;
                break;
            case Mark.E:
                achievementsDelegate += OnFinishedWithE;
                break;
        }
    }

    private void OnFinishedWithA(bool isFinished)
    {
        Achievements.achievements["CompletedWithA"] = isFinished;
    }
    
    private void OnFinishedWithB(bool isFinished)
    {
        Achievements.achievements["CompletedWithB"] = isFinished;
    }
    
    private void OnFinishedWithC(bool isFinished)
    {
        Achievements.achievements["CompletedWithC"] = isFinished;
    }
    
    private void OnFinishedWithD(bool isFinished)
    {
        Achievements.achievements["CompletedWithD"] = isFinished;
    }
    
    private void OnFinishedWithE(bool isFinished)
    {
        Achievements.achievements["CompletedWithE"] = isFinished;
    }
}