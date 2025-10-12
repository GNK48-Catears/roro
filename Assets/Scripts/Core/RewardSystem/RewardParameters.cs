using UnityEngine;

[CreateAssetMenu(fileName = "RewardParameters", menuName = "RoRo/RewardParameters")]
public class RewardParameters : ScriptableObject, IRewardParameters
{
    [Header("Eating")]
    [SerializeField, Min(0f)] private float eatingScore = 20f;
    [SerializeField, Min(0f)] private float breakfastScore = 60f;
    [Header("Exercise")]
    [SerializeField, Min(0f)] private float exerciseMultiplier = 2f;
    [SerializeField, Min(0f)] private float exerciseReducedMultiplier = 1f;
    [Header("Gaming")]
    [SerializeField, Min(0f)] private float gamingMultiplier = 0.5f;
    [SerializeField, Min(0f)] private float gamingReducedMultiplier = 0.25f;
    
    public float EatingModifer => eatingScore;
    public float BreakfastModifer => breakfastScore;
    public float ExerciseMultiplier => exerciseMultiplier;
    public float ExerciseReducedMultiplier => exerciseReducedMultiplier;
    public float GamingMultiplier => gamingMultiplier;
    public float GamingReducedMultiplier => gamingReducedMultiplier;
}
