using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Path _path;
    private PathNode[] _nodes;
    private float _moveSpeed;
    private int _currentNode;

    private void Start()
    {
        _nodes = _path.GetNodes();
        _currentNode = 0;
    }

    private void Update()
    {
        
    }

    private PathNode GetClosestNode()
    {
        PathNode closestNode = _nodes[0];
        float distance = Vector3.Distance(transform.position, _nodes[0].GetPosition());
        float newDistance = 0;
        foreach (var node in _nodes)
        {
            newDistance = Vector3.Distance(transform.position, node.GetPosition());
            
            if (newDistance < distance)
            {
                closestNode = node;
                distance = newDistance;
            }
        }

        return closestNode;
    }

    private void MoveTowardsNextNode()
    {
        
    }
}
