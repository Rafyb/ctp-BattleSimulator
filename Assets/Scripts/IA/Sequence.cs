using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    protected List<Node> _nodes = new List<Node>();

    public Sequence(List<Node> nodes)
    {
        this._nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        bool isRunning = false;
        foreach (var node in _nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    return _nodeState;
                case NodeState.SUCCESS:
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.FAILURE;
                    return _nodeState;
                default:
                    break;
            }
        }
        _nodeState =  NodeState.SUCCESS;
        return _nodeState;
    }
}
