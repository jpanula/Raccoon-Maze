using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Path : MonoBehaviour
{

    public PathNode[] GetNodes()
    {
        return GetComponentsInChildren<PathNode>();
    }

    private void OnDrawGizmos()
    {
        PathNode[] nodes = GetNodes();
        for (int i = 0; i < nodes.Length; i++)
        {
            Gizmos.DrawLine(nodes[i].GetPosition(), nodes[(i + 1) % nodes.Length].GetPosition());
        }
    }
}
