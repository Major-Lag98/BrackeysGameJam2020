using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Struct for holding information about a frame of Rewind.
/// Holds Position and a timestamp. The timestamp will be used for lerping values
/// </summary>
public struct RewindFrame 
{
    public Vector2 Position;
    public float Timestamp;

    public Vector3 Rotation { get; }

    public RewindFrame(Vector2 position, float timestamp, Vector3 rotation)
    {
        Position = position;
        Timestamp = timestamp;
        Rotation = rotation;
    }
}
