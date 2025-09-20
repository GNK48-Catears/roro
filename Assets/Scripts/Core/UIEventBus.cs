using UnityEngine;
using System;
public enum CategoryType
{
    Gaming,
    Exercise,
    Eating
}
public class UIEventBus : MonoBehaviour
{   
    /// <summary>
    /// Triggered when a category (e.g., "Gaming", "Exercise", "Eating") is selected.
    /// Parameter: CategoryType category
    /// </summary>
    public static Action<CategoryType> CategorySelected;

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
