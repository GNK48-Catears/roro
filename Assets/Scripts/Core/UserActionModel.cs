using UnityEngine;

public class UserActionModel : MonoBehaviour
{
    private PlayerPrefsRepository<GamingData> _gamingRepo;
    private PlayerPrefsRepository<EatingData> _eatingRepo;
    private PlayerPrefsRepository<ExerciseData> _exerciseRepo;

    void Awake()
    {
        _gamingRepo = new PlayerPrefsRepository<GamingData>();
        _eatingRepo = new PlayerPrefsRepository<EatingData>();
        _exerciseRepo = new PlayerPrefsRepository<ExerciseData>();
    }

    public void AddGamingData(string ntype = "Unknown", int ntime = 0)
    {
        GamingData data = new GamingData { type = ntype, time = ntime };
        _gamingRepo.Add(data);
        float currentScore = PersistentDataModel.ReadScore();
        float addScore = RewardSystem.GetGamingReward(data);
        PersistentDataModel.SaveNewScore(currentScore + addScore);
        FunctionEventBus.UserScoreAdded?.Invoke(addScore);
    }

    public void AddEatingData(MealType nMeal = MealType.Dinner, FoodType nFood = FoodType.Fuel)
    {
        EatingData data = new EatingData { meal = nMeal, food = nFood };
        _eatingRepo.Add(data);
        float currentScore = PersistentDataModel.ReadScore();
        float addScore = RewardSystem.GetEatingReward(data);
        PersistentDataModel.SaveNewScore(currentScore + addScore);
        FunctionEventBus.UserScoreAdded?.Invoke(addScore);
    }

    public void AddExerciseData(ExerciseType nType = ExerciseType.FNM, int nTime = 0)
    {
        ExerciseData data = new ExerciseData { type = ExerciseType.Cardio, time = 0 };
        _exerciseRepo.Add(data);
        float currentScore = PersistentDataModel.ReadScore();
        float addScore = RewardSystem.GetExerciseReward(data);
        PersistentDataModel.SaveNewScore(currentScore + addScore);
        FunctionEventBus.UserScoreAdded?.Invoke(addScore);
    }
}
