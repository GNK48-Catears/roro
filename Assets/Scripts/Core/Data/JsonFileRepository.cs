using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// A simple JSON file-backed repository for type T.
/// Stores all records for a given type in a single JSON file.
/// </summary>
public class JsonFileRepository<T>
{
    private readonly string _filePath;
    private DataStore _cachedDataStore;

    [Serializable]
    private class Envelope
    {
        public string id;
        public long createdAtUnixMs;
        public T data;
    }

    // DataStore -> n*Envelope -> id, data, etc.
    [Serializable]
    private class DataStore
    {
        public List<Envelope> Records = new List<Envelope>();
    }

    public JsonFileRepository()
    {
        string typeKey = typeof(T).FullName;
        var repositoryPath = Path.Combine(Application.persistentDataPath, "repository");
        _filePath = Path.Combine(repositoryPath, $"{typeKey}.json");
        
        if (!Directory.Exists(repositoryPath))
        {
            Directory.CreateDirectory(repositoryPath);
        }
    }

    private DataStore GetDataStore()
    {
        if (_cachedDataStore != null)
        {
            return _cachedDataStore;
        }

        if (!File.Exists(_filePath))
        {
            _cachedDataStore = new DataStore();
            return _cachedDataStore;
        }

        string json = File.ReadAllText(_filePath);
        if (string.IsNullOrEmpty(json))
        {
            _cachedDataStore = new DataStore();
            return _cachedDataStore;
        }
        
        try
        {
            _cachedDataStore = JsonUtility.FromJson<DataStore>(json) ?? new DataStore();
        }
        catch
        {
            // If deserialization fails, create a new store to prevent data loss of new entries.
            // You might want to add more robust error handling here, like logging the error
            // or attempting to back up the corrupted file.
            _cachedDataStore = new DataStore();
        }
        return _cachedDataStore;
    }

    private void SaveDataStore()
    {
        if (_cachedDataStore == null) return;
        
        string json = JsonUtility.ToJson(_cachedDataStore, true);
        File.WriteAllText(_filePath, json);
    }

    public DataRecord<T> Add(T data)
    {
        var store = GetDataStore();
        // The original UuidGenerator.GenerateUuidFromClassName is deterministic and will not produce unique IDs.
        // I will use a simple Guid-based generator for now.
        string id = Guid.NewGuid().ToString();
        long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var env = new Envelope { id = id, createdAtUnixMs = now, data = data };

        store.Records.Add(env);
        SaveDataStore();

        return new DataRecord<T> { id = id, createdAtUnixMs = now, data = data };
    }

    public bool TryGet(string id, out DataRecord<T> record)
    {
        var envelope = GetDataStore().Records.Find(e => e.id == id);
        if (envelope != null)
        {
            record = new DataRecord<T> { id = envelope.id, createdAtUnixMs = envelope.createdAtUnixMs, data = envelope.data };
            return true;
        }

        record = null;
        return false;
    }

    public List<DataRecord<T>> GetAll()
    {
        var store = GetDataStore();
        var results = new List<DataRecord<T>>(store.Records.Count);
        foreach (var env in store.Records)
        {
            results.Add(new DataRecord<T> { id = env.id, createdAtUnixMs = env.createdAtUnixMs, data = env.data });
        }
        return results;
    }

    public bool Delete(string id)
    {
        var store = GetDataStore();
        int removedCount = store.Records.RemoveAll(e => e.id == id);

        if (removedCount > 0)
        {
            SaveDataStore();
            return true;
        }
        
        return false;
    }

    public void DeleteRepository()
    {
        _cachedDataStore = null;
        if (File.Exists(_filePath))
        {
            try
            {
                File.Delete(_filePath);
            }
            catch
            {
                // Fails silently
            }
        }
    }
}


