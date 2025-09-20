using UnityEngine;

public class UIEventBus : MonoBehaviour
{   
    /// <summary>
    /// Triggered when a category (e.g., "Gaming", "Exercise", "Eating") is selected.
    /// Parameter: string categoryName
    /// </summary>
    public static Action<string> CategorySelected;

    /// <summary>
    /// Triggered when a record is submitted by the user.
    /// Parameter: object recordData (should be cast to the appropriate data type)
    /// </summary>
    public static Action<object> RecordSubmitted;

    /// <summary>
    /// Triggered to show the success modal after a record is submitted.
    /// Parameter: int scoreGained
    /// </summary>
    public static Action<int> ShowRecordSuccessModal;
}
