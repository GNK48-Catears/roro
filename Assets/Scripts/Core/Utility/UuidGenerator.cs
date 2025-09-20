using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;


public static class UuidGenerator
{
    /// <summary>
    /// It takes in any random class, give it a Uuid by the class name and current time
    /// </summary>
    /// <param name="type">The class Type to base the UUID on.</param>
    public static string GenerateUuidFromClassName(Type type)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        string input = $"{type.FullName}|{DateTime.UtcNow.Ticks}";
        using (var md5 = MD5.Create())
        {
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            var guid = new Guid(hash);
            return guid.ToString();
        }
    }
}
