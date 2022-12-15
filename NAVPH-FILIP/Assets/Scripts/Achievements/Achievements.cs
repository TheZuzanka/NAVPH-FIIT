using System.Collections.Generic;

public static class Achievements
{
    // this class holds player's achievements

    public static Dictionary<string, bool> achievements = new()
    {
        {"CompletedAsZuzka", false},
        {"CompletedAsFilip", false},
        {"CompleteWithScore", false},
        {"CompletedWithA", false},
        {"CompletedWithB", false},
        {"CompletedWithC", false},
        {"CompletedWithD", false},
        {"CompletedWithE", false},
    };
    
    public static int PointAchievementThreshold = 10;
}