using Zenject;

public class RewardSystem : IRewardSystem
{
    private IRewardParameters _rewardParameters;
    
    public RewardSystem(IRewardParameters rewardParameters)
    {
        _rewardParameters = rewardParameters;
    }
    
    public float GetGamingReward(GamingData gamingData)
    {
        return GamingAlgorithm.CalculateScore(gamingData, 
            _rewardParameters.GamingMultiplier, _rewardParameters.GamingReducedMultiplier);
    }

    public float GetEatingReward(EatingData eatingData)
    {
        return EatingAlogorithm.CalculateScore(eatingData, 
            _rewardParameters.EatingModifer, _rewardParameters.BreakfastModifer);
    }

    public float GetExerciseReward(ExerciseData exerciseData)
    {
        return ExerciseAlgorithm.CalculateScore(exerciseData, 
            _rewardParameters.ExerciseMultiplier, _rewardParameters.ExerciseReducedMultiplier);
    }
}
