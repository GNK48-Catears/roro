public interface IRewardSystem
{
    float GetGamingReward(GamingData gamingData);
    float GetEatingReward(EatingData eatingData);
    float GetExerciseReward(ExerciseData exerciseData);
}
