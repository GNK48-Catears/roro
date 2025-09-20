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
    /// Triggered when a record is submitted by the user.
    /// Parameter: object recordData (should be cast to the appropriate data type)
    /// </summary>
    public static Action<object> RecordSubmitted;

}
