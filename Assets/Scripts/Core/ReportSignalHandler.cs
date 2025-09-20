using UnityEngine;

/// <summary>
/// Handles UI Signals. Controls Model to do the Report actions
/// </summary>
public class ReportSignalHandler : MonoBehaviour
{
    [SerializeField] private UserActionModel actionModel;
    private void OnEnable()
    {
        UIEventBus.RecordSubmitted += HandleNewActionRecorded;
    }

    private void OnDisable()
    {
        UIEventBus.RecordSubmitted -= HandleNewActionRecorded;
    }

    void HandleNewActionRecorded(object data)
    {
        if (data == null)
        {
            Debug.LogWarning("RecordSubmitted received null data.");
            return;
        }

        if (actionModel == null)
        {
            Debug.LogError("UserActionModel reference is not set on ReportSignalHandler.");
            return;
        }

        if (data is GamingData gamingData)
        {
            actionModel.AddGamingData(gamingData);
            return;
        }

        if (data is EatingData eatingData)
        {
            actionModel.AddEatingData(eatingData);
            return;
        }

        if (data is ExerciseData exerciseData)
        {
            actionModel.AddExerciseData(exerciseData);
            return;
        }

        Debug.LogWarning($"Unsupported record data type: {data.GetType().Name}");
    }
}
