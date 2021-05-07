using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : Node
{
    Soldat _soldat;
    public MoveTo(Soldat s)
    {
        _soldat = s;
    }
    public override NodeState Evaluate()
    {
        _soldat.gameObject.transform.DOKill();

        if (Vector2.Distance(_soldat.position,_soldat.positionToMove) < 0.1f)
        {
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.RUNNING;
            _soldat.gameObject.transform.DOMove(_soldat.positionToMove, Vector2.Distance(_soldat.position,_soldat.cible.position) * _soldat.speed).OnComplete(() =>
            {
                _nodeState = NodeState.SUCCESS;
            });
        }


        return _nodeState;
    }
}
