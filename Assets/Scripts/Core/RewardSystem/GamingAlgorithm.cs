
/// <summary>
/// A really dumb algorithm, 
/// that takes in gaming time, times a multiplier (assume to be 1)
/// </summary>
public static class GamingAlgorithm
{
    private const float normalMultiplier = 1f;
    private const float reducedMultiplier = 0.5f;

    /// <summary>
    /// Note: when the time is over 1 hour, reduce the multiplier to 1
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static float CalculateScore(GamingData data)
    {
        if (data == null)
        {
            return 0f;
        }

        var minutes = data.time;
        var multiplier = minutes > 60 ? reducedMultiplier : normalMultiplier;
        return minutes * multiplier;
    }
}
