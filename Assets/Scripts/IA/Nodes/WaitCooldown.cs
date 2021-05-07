using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCooldown : Node
{
    float _time;
    Soldat _soldat;
    public WaitCooldown(Soldat s)
    {
        _soldat = s;
    }
    public override NodeState Evaluate()
    {
        _time += Time.deltaTime;

        if(_time >= _soldat.cooldown)
        {
            _time -= _soldat.cooldown;
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.RUNNING;
        }

        return _nodeState;
    }
}
