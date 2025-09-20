using UnityEngine;
using System;

public class FunctionEventBus
{
    public static Action<float> UserScoreUpdated;
    public static Action<float> UserScoreAdded;
}
