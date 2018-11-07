using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathNode : MonoBehaviour
{
    public enum NodeType
    {
        Curved,
        Straight
    }

    public abstract NodeType Type { get; }

    public virtual Vector3 GetPosition()
    {
        return transform.position;
    }
}
