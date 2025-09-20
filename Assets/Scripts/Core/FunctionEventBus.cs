using UnityEngine;
using System;

public class FunctionEventBus
{
    /// <summary>
    /// float: current score
    /// </summary>
    public static Action<float> UserScoreUpdated;
    /// <summary>
    /// float: the amount of score added in this single action
    /// </summary>
    public static Action<float> UserScoreAdded;
}
