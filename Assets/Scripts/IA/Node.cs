using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
	RUNNING, SUCCESS, FAILURE,
}

public abstract class Node
{
	protected NodeState _nodeState;
	
	public NodeState GetState()
    {
		return _nodeState;
    }

	public abstract NodeState Evaluate();
}


