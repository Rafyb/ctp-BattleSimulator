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
        _soldat.cible.TakeHit(_soldat.damage);
        return NodeState.SUCCESS;
    }
}
