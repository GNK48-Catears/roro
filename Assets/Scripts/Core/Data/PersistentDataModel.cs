using System;
using UnityEngine;

/// <summary>
/// A sealed class for persistent data read/write in the app
/// </summary>
public static class PersistentDataModel
{
    public static readonly string SCORE_KEY = "user:score";
    // TODO: we need a proper data storing/accessing method
    public static void SaveNewScore(float score)
    {
        PlayerPrefs.SetFloat(SCORE_KEY, score);
        FunctionEventBus.UserScoreUpdated?.Invoke(score);
    }

    public static float ReadScore()
    {
        return PlayerPrefs.GetFloat(SCORE_KEY);
    }

}
