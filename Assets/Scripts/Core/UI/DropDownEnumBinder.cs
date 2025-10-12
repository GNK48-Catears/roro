using TMPro;
using UnityEngine;
using System;

public static class DropDownEnumBinder
{
    public static void BindEnumToDropdown<T>(TMP_Dropdown dropdown) where T : Enum
    {
        dropdown.ClearOptions();
        var names = Enum.GetNames(typeof(T));
        dropdown.AddOptions(new System.Collections.Generic.List<string>(names));
    }
}