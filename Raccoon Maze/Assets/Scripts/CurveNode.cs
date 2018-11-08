using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveNode : PathNode
{
    public override NodeType Type
    {
        get { return NodeType.Curved; }
    }
}
