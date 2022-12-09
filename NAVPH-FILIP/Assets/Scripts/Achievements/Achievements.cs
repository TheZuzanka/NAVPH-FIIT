using System.Collections.Generic;

public static class Achievements
{
    // this class holds player's achievements

    public static bool CompletedAsZuzanka = false;
    public static bool CompletedAsFilip = false;

    public static Dictionary<string, bool> achievements = new()
        {{"CompletedAsZuzka", false}, {"CompletedAsFilip", false}};
}