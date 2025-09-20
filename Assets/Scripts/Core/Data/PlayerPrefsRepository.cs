using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple PlayerPrefs-backed repository for type T.
/// Maintains an index list under a type-scoped key and stores each record as JSON under its own key.
/// </summary>
public class PlayerPrefsRepository<T>
{
    private readonly string _typeKey;

    [Serializable]
    private class Envelope
    {
        public string id;
        public long createdAtUnixMs;
        public T data;
    }

    [Serializable]
    private class Index
    {
        public List<string> ids = new List<string>();
    }

    public PlayerPrefsRepository()
    {
        _typeKey = typeof(T).FullName;
    }

    private string GetIndexKey()
    {
        return $"repo:{_typeKey}:index";
    }

    private string GetRecordKey(string id)
    {
        return $"repo:{_typeKey}:record:{id}";
    }

    private Index LoadIndex()
    {
        string json = PlayerPrefs.GetString(GetIndexKey(), string.Empty);
        if (string.IsNullOrEmpty(json)) return new Index();
        try { return JsonUtility.FromJson<Index>(json) ?? new Index(); }
        catch { return new Index(); }
    }

    private void SaveIndex(Index index)
    {
        string json = JsonUtility.ToJson(index);
        PlayerPrefs.SetString(GetIndexKey(), json);
    }

    public DataRecord<T> Add(T data)
    {
        string id = UuidGenerator.GenerateUuidFromClassName(typeof(T));
        long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var env = new Envelope { id = id, createdAtUnixMs = now, data = data };

        string envJson = JsonUtility.ToJson(env);
        PlayerPrefs.SetString(GetRecordKey(id), envJson);

        var index = LoadIndex();
        index.ids.Add(id);
        SaveIndex(index);

        return new DataRecord<T> { id = id, createdAtUnixMs = now, data = data };
    }

    public bool TryGet(string id, out DataRecord<T> record)
    {
        string json = PlayerPrefs.GetString(GetRecordKey(id), string.Empty);
        if (string.IsNullOrEmpty(json)) { record = null; return false; }
        try
        {
            var env = JsonUtility.FromJson<Envelope>(json);
            record = new DataRecord<T> { id = env.id, createdAtUnixMs = env.createdAtUnixMs, data = env.data };
            return true;
        }
        catch
        {
            record = null;
            return false;
        }
    }

    public List<DataRecord<T>> GetAll()
    {
        var index = LoadIndex();
        var results = new List<DataRecord<T>>(index.ids.Count);
        foreach (var id in index.ids)
        {
            if (TryGet(id, out var rec)) results.Add(rec);
        }
        return results;
    }

    public bool Delete(string id)
    {
        var index = LoadIndex();
        bool removed = index.ids.Remove(id);
        if (!removed) return false;
        SaveIndex(index);
        PlayerPrefs.DeleteKey(GetRecordKey(id));
        return true;
    }
}


