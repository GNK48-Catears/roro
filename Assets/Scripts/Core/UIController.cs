using UnityEngine;
using UnityEngine.UI;
using System;

public class RecordInputPanelController : MonoBehaviour
{   
    [Header("Panels")]
    public GameObject gamingPanel;
    public GameObject exercisePanel;
    public GameObject eatingPanel;

    [Header("Category Buttons")]
    public Button gamingButton;
    public Button exerciseButton;
    public Button eatingButton;
    public Button confirmButton;

    private CategoryType currentCategory;

    [Header("Gaming Settings")]
    public Dropdown gamingTypeDropdown;
    public InputField gamingTimeInput;

    [Header("Exercise Settings")]
    public Dropdown exerciseTypeDropdown;
    public InputField exerciseTimeInput;

    [Header("Eating Settings")]
    public Dropdown mealTypeDropdown;
    public Dropdown foodTypeDropdown;

    void OnEnable()
    {
        gamingButton.onClick.AddListener(() => OnCategoryButtonClicked(CategoryType.Gaming));
        exerciseButton.onClick.AddListener(() => OnCategoryButtonClicked(CategoryType.Exercise));
        eatingButton.onClick.AddListener(() => OnCategoryButtonClicked(CategoryType.Eating));
        UIEventBus.CategorySelected += OnCategorySelected;
        confirmButton.onClick.AddListener(OnConfirmClicked);
    }

    void OnDisable()
    {
        gamingButton.onClick.RemoveAllListeners();
        exerciseButton.onClick.RemoveAllListeners();
        eatingButton.onClick.RemoveAllListeners();
        UIEventBus.CategorySelected -= OnCategorySelected;
        confirmButton.onClick.RemoveListener(OnConfirmClicked);
    }

    void OnCategorySelected(CategoryType category)
    {
        currentCategory = category;
        gamingPanel.SetActive(category == CategoryType.Gaming);
        exercisePanel.SetActive(category == CategoryType.Exercise);
        eatingPanel.SetActive(category == CategoryType.Eating);
    }

    void OnCategoryButtonClicked(CategoryType category)
    {
        UIEventBus.CategorySelected?.Invoke(category);
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
    }
}
}