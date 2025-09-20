/// <summary>
/// Dummy Algorithm that takes exercise time
/// and turn into score by multiplier (assume to be 2)
/// </summary>
public static class ExerciseAlgorithm
{
    // Note: when the time is over 1 hour, reduce the multiplier to 1
    private const float normalMultiplier = 2f;
    private const float reducedMultiplier = 1f;

    public static float CalculateScore(ExerciseData data)
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
