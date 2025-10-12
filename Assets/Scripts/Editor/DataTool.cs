using UnityEditor;
using UnityEngine;

public class DataTool
{
    [MenuItem("Tools/RoRo/Delete All Data")]
    public static void DeleteAllDataRepository()
    {
        ResetScore();
        DeleteEatingDataRepository();
        DeleteExerciseDataRepository();
        DeleteGamingDataRepository();
        Debug.Log("All data repositories deleted.");
    }

    [MenuItem("Tools/RoRo/Reset Score")]
    public static void ResetScore()
    {
        PersistentDataModel.ResetScore();
        Debug.Log("User score has been reset.");
    }

    [MenuItem("Tools/RoRo/Delete Eating Data")]
    public static void DeleteEatingDataRepository()
    {
        var repository = new JsonFileRepository<EatingData>();
        repository.DeleteRepository();
        Debug.Log("EatingData repository deleted.");
    }

    [MenuItem("Tools/RoRo/Delete Exercise Data")]
    public static void DeleteExerciseDataRepository()
    {
        var repository = new JsonFileRepository<ExerciseData>();
        repository.DeleteRepository();
        Debug.Log("ExerciseData repository deleted.");
    }

    [MenuItem("Tools/RoRo/Delete Gaming Data")]
    public static void DeleteGamingDataRepository()
    {
        var repository = new JsonFileRepository<GamingData>();
        repository.DeleteRepository();
        Debug.Log("GamingData repository deleted.");
    }


}
