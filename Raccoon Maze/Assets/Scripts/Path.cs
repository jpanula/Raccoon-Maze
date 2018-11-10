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
            if (nodes[(i + 1) % nodes.Length].Type == NodeType.Straight)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(nodes[i].GetPosition(), nodes[(i + 1) % nodes.Length].GetPosition());
            }

            else if (nodes[(i + 1) % nodes.Length].Type == NodeType.Curved)
            {
                Gizmos.color = Color.magenta;
                //Gizmos.DrawWireSphere(Vector3.Lerp(nodes[i].GetPosition(), nodes[(i + 1) % nodes.Length].GetPosition(), 0.5f), Vector3.Distance(nodes[i].GetPosition(), nodes[(i + 1) % nodes.Length].GetPosition()) / 2);
                Gizmos.DrawLine(nodes[i].GetPosition(), nodes[(i + 1) % nodes.Length].GetPosition());
            }
        }
    }
}
