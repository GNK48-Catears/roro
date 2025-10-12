using UnityEngine;
using DG.Tweening;
using System;

public class PlanetAnim : MonoBehaviour
{   
    public static PlanetAnim Instance;
    public event Action OnPlusClickComplete;
    public event Action OnReportFinish;

    [Header("Planet 1")]
    [SerializeField] private Transform planet1;
    [SerializeField] private Vector3 planet1TargetPos;
    [Header("Planet 2")]
    [SerializeField] private Transform planet2;
    [SerializeField] private Vector3 planet2TargetPos;
    [Header("Planet 3")]
    [SerializeField] private Transform planet3;
    [SerializeField] private Vector3 planet3TargetPos;
    [SerializeField] private Vector3 planetTargetScale;
    [SerializeField] private float duration = 1.0f;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
    }

    public void FirstClick()
    {   
        var seq = DOTween.Sequence();
        seq.Append(planet1.DOLocalMove(planet1TargetPos, duration).SetEase(Ease.InOutSine));
        seq.Join(planet1.DOScale(planetTargetScale, duration).SetEase(Ease.InOutSine));
        seq.Join(planet2.DOLocalMove(planet2TargetPos, duration).SetEase(Ease.InOutSine));
        seq.Join(planet2.DOScale(planetTargetScale, duration).SetEase(Ease.InOutSine));
        seq.Join(planet3.DOLocalMove(planet3TargetPos, duration).SetEase(Ease.InOutSine));
        seq.Join(planet3.DOScale(planetTargetScale, duration).SetEase(Ease.InOutSine));

        seq.OnComplete(() => {OnPlusClickComplete?.Invoke();});
    }

    public void FinishReport()
    {
        var seq = DOTween.Sequence();
        seq.Append(planet1.DOLocalMove(Vector3.zero, duration).SetEase(Ease.InOutSine));
        seq.Join(planet1.DOScale(Vector3.one, duration).SetEase(Ease.InOutSine));
        seq.Join(planet2.DOLocalMove(Vector3.zero, duration).SetEase(Ease.InOutSine));
        seq.Join(planet2.DOScale(Vector3.one, duration).SetEase(Ease.InOutSine));
        seq.Join(planet3.DOLocalMove(Vector3.zero, duration).SetEase(Ease.InOutSine));
        seq.Join(planet3.DOScale(Vector3.one, duration).SetEase(Ease.InOutSine));

        seq.OnComplete(() => {OnReportFinish?.Invoke();});
    }
}
