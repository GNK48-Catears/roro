using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject selectCategoryPanel;
    [SerializeField] private GameObject gamingPanel;
    [SerializeField] private GameObject exercisePanel;
    [SerializeField] private GameObject eatingPanel;
    [Header("Result Panel")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI scoreEarnedText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private Button closeButton;

    [Header("Category Buttons")]
    [SerializeField] private Button gamingButton;
    [SerializeField] private Button exerciseButton;
    [SerializeField] private Button eatingButton;
    [SerializeField] private Button confirmButton;

    private CategoryType currentCategory;

    [Header("Gaming Settings")]
    private TMP_Dropdown gamingTypeDropdown;
    private TMP_InputField gamingTimeInput;

    [Header("Exercise Settings")]
    private TMP_Dropdown exerciseTypeDropdown;
    private TMP_InputField exerciseTimeInput;

    [Header("Eating Settings")]
    private TMP_Dropdown mealTypeDropdown;
    private TMP_Dropdown foodTypeDropdown;

    void Awake()
    {
        Setup();
    }
    void OnEnable()
    {
        gamingButton.onClick.AddListener(() => OnCategoryButtonClicked(CategoryType.Gaming));
        exerciseButton.onClick.AddListener(() => OnCategoryButtonClicked(CategoryType.Exercise));
        eatingButton.onClick.AddListener(() => OnCategoryButtonClicked(CategoryType.Eating));
        confirmButton.onClick.AddListener(OnConfirmClicked);
        closeButton.onClick.AddListener(Reset);
        FunctionEventBus.UserScoreAdded += (score) => SetScoreEarned(score);
        FunctionEventBus.UserScoreUpdated += (score) => SetTotalScore(score);
    }

    void OnDisable()
    {
        gamingButton.onClick.RemoveAllListeners();
        exerciseButton.onClick.RemoveAllListeners();
        eatingButton.onClick.RemoveAllListeners();
        confirmButton.onClick.RemoveListener(OnConfirmClicked);
        closeButton.onClick.RemoveListener(Reset);
        FunctionEventBus.UserScoreAdded -= (score) => SetScoreEarned(score);
        FunctionEventBus.UserScoreUpdated -= (score) => SetTotalScore(score);
    }
    
    void OnCategoryButtonClicked(CategoryType category)
    {
        currentCategory = category;
        ShowPanel(category switch
        {
            CategoryType.Gaming => gamingPanel,
            CategoryType.Exercise => exercisePanel,
            CategoryType.Eating => eatingPanel,
            _ => selectCategoryPanel
        });
        confirmButton.gameObject.SetActive(true);
    }

    void OnConfirmClicked()
    {
        object recordData = null;
        switch (currentCategory)
        {
            case CategoryType.Gaming:
                recordData = new GamingData
                {
                    type = gamingTypeDropdown.options[gamingTypeDropdown.value].text,
                    time = int.TryParse(gamingTimeInput.text, out int gTime) ? gTime : 0
                };
                break;
            case CategoryType.Exercise:
                recordData = new ExerciseData
                {
                    type = (ExerciseType)exerciseTypeDropdown.value,
                    time = int.TryParse(exerciseTimeInput.text, out int eTime) ? eTime : 0
                };
                break;
            case CategoryType.Eating:
                recordData = new EatingData
                {
                    meal = (MealType)mealTypeDropdown.value,
                    food = (FoodType)foodTypeDropdown.value
                };
                break;
        }
        if (recordData != null)
        {
            UIEventBus.RecordSubmitted?.Invoke(recordData);
            ShowPanel(resultPanel);
        }
    }

    public void Setup()
    {
        // Gaming
        if (gamingPanel != null)
        {
            gamingTypeDropdown = gamingPanel.transform.GetChild(0).GetComponent<TMP_Dropdown>();
            gamingTimeInput = gamingPanel.transform.GetChild(1).GetComponent<TMP_InputField>();
        }
        // Exercise
        if (exercisePanel != null)
        {
            exerciseTypeDropdown = exercisePanel.transform.GetChild(0).GetComponent<TMP_Dropdown>();
            exerciseTimeInput = exercisePanel.transform.GetChild(1).GetComponent<TMP_InputField>();
        }
        // Eating
        if (eatingPanel != null)
        {
            mealTypeDropdown = eatingPanel.transform.GetChild(0).GetComponent<TMP_Dropdown>();
            foodTypeDropdown = eatingPanel.transform.GetChild(1).GetComponent<TMP_Dropdown>();
        }

        DropDownEnumBinder.BindEnumToDropdown<MealType>(mealTypeDropdown);
        DropDownEnumBinder.BindEnumToDropdown<FoodType>(foodTypeDropdown);
        DropDownEnumBinder.BindEnumToDropdown<ExerciseType>(exerciseTypeDropdown);
        Reset();
    }

    private void Reset()
    {
        gamingTypeDropdown.value = 0;
        gamingTimeInput.text = "";
        exerciseTypeDropdown.value = 0;
        exerciseTimeInput.text = "";
        mealTypeDropdown.value = 0;
        foodTypeDropdown.value = 0;
        ShowPanel(startPanel);
        confirmButton.gameObject.SetActive(false);
    }

    private void ShowPanel(GameObject panel)
    {
        startPanel.SetActive(false);
        selectCategoryPanel.SetActive(false);
        gamingPanel.SetActive(false);
        exercisePanel.SetActive(false);
        eatingPanel.SetActive(false);
        resultPanel.SetActive(false);

        panel.SetActive(true);

        if (panel == gamingPanel || panel == exercisePanel || panel == eatingPanel)
            confirmButton.gameObject.SetActive(true);
    }

    private void SetScoreEarned(float score)
    {
        if (scoreEarnedText != null)
            scoreEarnedText.text = $"Score Earned: {score}";
    }

    private void SetTotalScore(float score)
    {
        if (totalScoreText != null)
            totalScoreText.text = $"Total Score: {score}";
    }

}