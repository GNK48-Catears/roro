using UnityEngine;

/// <summary>
/// A sealed class for persistent data read/write in the app
/// </summary>
public static class PersistentDataModel
{
    // TODO: we need a proper data storing/accessing method
    public static void SaveNewData(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public static string ReadData(string keyAsQuery)
    {
        return PlayerPrefs.GetString(keyAsQuery);
    }
}
