/// <summary>
/// A Read-Only interface for Reward Parameters
/// </summary>
public interface IRewardParameters
{
    float EatingModifer { get; }
    float BreakfastModifer { get; }
    float ExerciseMultiplier { get; }
    float ExerciseReducedMultiplier { get; }
    float GamingMultiplier { get; }
    float GamingReducedMultiplier { get; }
}
