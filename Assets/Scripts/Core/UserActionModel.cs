using UnityEngine;
using Zenject;

public class UserActionModel : MonoBehaviour
{
    [Inject] private JsonFileRepository<GamingData> _gamingRepo;
    [Inject] private JsonFileRepository<EatingData> _eatingRepo;
    [Inject] private JsonFileRepository<ExerciseData> _exerciseRepo;
    [Inject] private IRewardSystem _rewardSystem;
    [SerializeField] private bool deleteDataOnLaunchMode = false;

    void Awake()
    {
        /*
        _gamingRepo = new JsonFileRepository<GamingData>();
        _eatingRepo = new JsonFileRepository<EatingData>();
        _exerciseRepo = new JsonFileRepository<ExerciseData>();
        */
        if (deleteDataOnLaunchMode)
        {
            _eatingRepo.DeleteRepository();
            _exerciseRepo.DeleteRepository();
            _gamingRepo.DeleteRepository();
            PersistentDataModel.ResetScore();
        }
    }

    public void AddGamingData(string ntype = "Unknown", int ntime = 0)
    {
        GamingData data = new GamingData { type = ntype, time = ntime };
        _gamingRepo.Add(data);
        float currentScore = PersistentDataModel.ReadScore();
        float addScore = _rewardSystem.GetGamingReward(data);
        PersistentDataModel.SaveNewScore(currentScore + addScore);
        FunctionEventBus.UserScoreAdded?.Invoke(addScore);
    }

    public void AddGamingData(GamingData data)
    {
        _gamingRepo.Add(data);
        float currentScore = PersistentDataModel.ReadScore();
        float addScore = _rewardSystem.GetGamingReward(data);
        PersistentDataModel.SaveNewScore(currentScore + addScore);
        FunctionEventBus.UserScoreAdded?.Invoke(addScore);
    }

    public void AddEatingData(MealType nMeal = MealType.Dinner, FoodType nFood = FoodType.Fuel)
    {
        EatingData data = new EatingData { meal = nMeal, food = nFood };
        _eatingRepo.Add(data);
        float currentScore = PersistentDataModel.ReadScore();
        float addScore = _rewardSystem.GetEatingReward(data);
        PersistentDataModel.SaveNewScore(currentScore + addScore);
        FunctionEventBus.UserScoreAdded?.Invoke(addScore);
    }
    public void AddEatingData(EatingData data)
    {
        _eatingRepo.Add(data);
        float currentScore = PersistentDataModel.ReadScore();
        float addScore = _rewardSystem.GetEatingReward(data);
        PersistentDataModel.SaveNewScore(currentScore + addScore);
        FunctionEventBus.UserScoreAdded?.Invoke(addScore);
    }

    public void AddExerciseData(ExerciseType nType = ExerciseType.FNM, int nTime = 0)
    {
        ExerciseData data = new ExerciseData { type = ExerciseType.Cardio, time = 0 };
        _exerciseRepo.Add(data);
        float currentScore = PersistentDataModel.ReadScore();
        float addScore = _rewardSystem.GetExerciseReward(data);
        PersistentDataModel.SaveNewScore(currentScore + addScore);
        FunctionEventBus.UserScoreAdded?.Invoke(addScore);
    }

    public void AddExerciseData(ExerciseData data)
    {
        _exerciseRepo.Add(data);
        float currentScore = PersistentDataModel.ReadScore();
        float addScore = _rewardSystem.GetExerciseReward(data);
        PersistentDataModel.SaveNewScore(currentScore + addScore);
        FunctionEventBus.UserScoreAdded?.Invoke(addScore);
    }
}
