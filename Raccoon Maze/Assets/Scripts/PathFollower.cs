using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Path _path;
    private PathNode[] _nodes;
    [SerializeField] private float _moveSpeed;
    private int _targetNodeIndex;
    private PathNode _targetNode;
    private Mode _mode;
    private Vector3 _lastNodePosition;
    private PathNode _lastNode;

    private enum Mode
    {
        ClosestNode,
        NextNode
    }

    private void Awake()
    {
        _targetNodeIndex = 0;
    }

    private void OnEnable()
    {
        _mode = Mode.ClosestNode;
        _nodes = _path.GetNodes();
    }

    private void Update()
    {
        switch (_mode)
        {
            case Mode.ClosestNode:
                
                
                if (Vector3.Distance(transform.position, GetClosestNode().GetPosition()) < 0.1f)
                {
                    _targetNodeIndex = Array.IndexOf(_nodes, GetClosestNode());
                    ChangeMode(Mode.NextNode);
                }
                   break;
            
            case Mode.NextNode:
                
                   break;
            
            default:
                
                throw new ArgumentOutOfRangeException();
        }
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

    private void MoveTowardsTargetNode(float amount)
    {
        transform.position = Vector3.Lerp(_lastNodePosition, _nodes[_targetNodeIndex].GetPosition(), amount);
    }

    private void AcquireNextNode()
    {
        _lastNode = _targetNode;
        _targetNodeIndex++;
    }
    
    private void ChangeMode(Mode newMode)
    {
        _mode = newMode;
    }
}
