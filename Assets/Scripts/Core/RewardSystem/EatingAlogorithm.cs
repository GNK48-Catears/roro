/// <summary>
/// A really dumb algorithm, 
/// that only considers meal type
/// </summary>
public static class EatingAlogorithm
{
    private const float _normalScore = 20f;
    private const float _breakfastScore = 60f;
    /// <summary>
    /// Note: if it's Breakfast, worth 60 scores
    /// Otherwise, worth 20 scores
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static float CalculateScore(EatingData data,
        float normalScore, 
        float breakfastScore)
    {
        if (data == null)
        {
            return 0f;
        }
        
        return data.meal == MealType.Breakfast ? breakfastScore : normalScore;
    }
}
