using System;

/// <summary>
/// Generic in-memory record wrapper returned by repositories.
/// Not directly serialized; storage uses an internal envelope type.
/// </summary>
public class DataRecord<T>
{
    public string id;
    public long createdAtUnixMs;
    public T data;
}


