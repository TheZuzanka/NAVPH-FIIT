using System.Collections.Generic;

public static class GameLogicValues
{
    public static float CoffeeEffectMultiplier = 1.1f;
    public static int CoffeeTimerValue = 1000;
    public static int ShieldTimerValue = 1000;
    public static float MarkExistingTime = 5f;
    public static float FxExistingTime = 2f;
    
    public static readonly Dictionary<string, float> MarkCoefficients = new()
    {
        { "E", 1f },
        { "D", 1.5f },
        { "C", 2f },
        { "B", 2.5f },
        { "A", 3f }
    };
}