using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base class for pathfinding nodes
/// </summary>

/// Different node types
public enum NodeType
{
    Curved,
    Straight
}

public abstract class PathNode : MonoBehaviour
{
    

    public abstract NodeType Type { get; }

    public virtual Vector3 GetPosition()
    {
        return transform.position;
    }
}
