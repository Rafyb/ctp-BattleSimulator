using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    Soldat _soldat;

    public Attack(Soldat s)
    {
        _soldat = s;
    }
    public override NodeState Evaluate()
    {
        if(_soldat.type == TypeSoldat.EPEE) _soldat.cible.TakeHit(_soldat.damage);
        if (_soldat.type == TypeSoldat.ARCHER) Game.Instance.Shoot(0, _soldat);
        if (_soldat.type == TypeSoldat.DISTANCE) Game.Instance.Shoot(1, _soldat);
        return NodeState.SUCCESS;
    }
}
