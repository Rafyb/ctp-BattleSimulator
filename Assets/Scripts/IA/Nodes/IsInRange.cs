using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInRange : Node
{
    Soldat _soldat;

    public IsInRange(Soldat s)
    {
        _soldat = s;
    }


    public override NodeState Evaluate()
    {
        List<Soldat> enemies = Game.Instance.GetEnemies(_soldat.team);

        float distSaved = 100f ;
        _soldat.cible = null;
    
        foreach (Soldat enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = Vector2.Distance(enemy.position, _soldat.position);
                if (distance > _soldat.minDist && distance < _soldat.maxDist)
                {
                    if(distance < distSaved)
                    {
                        distSaved = distance;
                        _soldat.cible = enemy;
                        _soldat.gameObject.transform.DOKill();
                    }
                }

            }
        }

        if(_soldat.cible == null) _nodeState = NodeState.FAILURE;
        else _nodeState = NodeState.SUCCESS;

        return _nodeState;
    }
}
