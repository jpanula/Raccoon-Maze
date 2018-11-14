using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightNode : PathNode
{
    public override NodeType Type
    {
        get { return NodeType.Straight; }
    }
}
