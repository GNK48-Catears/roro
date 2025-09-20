[System.Serializable]
public class ExerciseData
{
    public ExerciseType type;
    /// <summary>
    /// Assume the basic unit to be "minute"
    /// </summary>
    public int time;

}

public enum ExerciseType
{
    Cardio,
    Strength,
    FNM
}
