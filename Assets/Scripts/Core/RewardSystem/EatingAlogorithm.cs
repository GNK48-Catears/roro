/// <summary>
/// A really dumb algorithm, 
/// that only considers meal type
/// </summary>
public static class EatingAlogorithm
{
    /// <summary>
    /// Note: if it's Breakfast, worth 60 scores
    /// Otherwise, worth 20 scores
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static float CalculateScore(EatingData data)
    {
        if (data == null)
        {
            return 0f;
        }

        return data.meal == MealType.Breakfast ? 60f : 20f;
    }
}
