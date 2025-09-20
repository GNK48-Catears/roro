using UnityEngine;

public static class RewardSystem
{
    public static float GetGamingReward(GamingData gamingData)
    {
        return GamingAlgorithm.CalculateScore(gamingData);
    }

    public static float GetEatingReward(EatingData eatingData)
    {
        return EatingAlogorithm.CalculateScore(eatingData);
    }

    public static float GetExerciseReward(ExerciseData exerciseData)
    {
        return ExerciseAlgorithm.CalculateScore(exerciseData);
    }
}
