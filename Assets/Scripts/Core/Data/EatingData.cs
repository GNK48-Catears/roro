[System.Serializable]
public class EatingData
{
    public MealType meal;
    public FoodType food;
}

public enum MealType
{
    Breakfast,
    Lunch,
    Dinner
}

public enum FoodType
{
    Fuel,
    Build,
    Regulate
}