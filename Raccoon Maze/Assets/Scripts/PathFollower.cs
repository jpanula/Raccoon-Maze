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
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _movementTimer;

    private void Awake()
    {
        _targetNodeIndex = 0;
    }

    private void OnEnable()
    {
        _nodes = _path.GetNodes();
        TargetClosestNode();
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position == _targetPosition)
        {
            TargetNextNode();
        }

        _movementTimer += Time.deltaTime * _moveSpeed / Vector3.Distance(_startPosition, _targetPosition);
        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _movementTimer);
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

    private void TargetNextNode()
    {
        _movementTimer = 0;
        _startPosition = transform.position;
        _targetNodeIndex = (_targetNodeIndex + 1) % _nodes.Length;
        _targetPosition = _nodes[_targetNodeIndex].GetPosition();
    }

    private void TargetClosestNode()
    {
        _targetNodeIndex = _targetNodeIndex = Array.IndexOf(_nodes, GetClosestNode());
        _targetPosition = GetClosestNode().GetPosition();
    }

}
