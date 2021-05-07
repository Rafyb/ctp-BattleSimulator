using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToMove : Node
{
    Soldat _soldat;
    public PositionToMove(Soldat s)
    {
        _soldat = s;
    }

    public override NodeState Evaluate()
    {
        List<Soldat> enemies = Game.Instance.GetEnemies(_soldat.team);

        float distSaved = 100f;
        _soldat.cible = null;

        foreach (Soldat enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = Vector2.Distance(enemy.position, _soldat.position);

                if (distance < distSaved)
                {
                    distSaved = distance;
                    _soldat.cible = enemy;
                }
                

            }
        }

        if(_soldat.minDist > 0f && distSaved < _soldat.minDist)
        {
            // reculer
            if(_soldat.position.x > _soldat.cible.position.x)
                _soldat.positionToMove = Vector2.Lerp(_soldat.cible.position - _soldat.position, _soldat.position, _soldat.minDist / 100);
            else
                _soldat.positionToMove = Vector2.Lerp(_soldat.position - _soldat.cible.position, _soldat.position, _soldat.minDist / 100);
        }
        else
        {
            // avancer
            _soldat.positionToMove = Vector2.Lerp(_soldat.cible.position, _soldat.position, _soldat.minDist/100);
        }

        if(_soldat.positionToMove.x > Game.Instance.minBorder.position.x && _soldat.positionToMove.y > Game.Instance.minBorder.position.y && _soldat.positionToMove.x < Game.Instance.maxBorder.position.x && _soldat.positionToMove.y < Game.Instance.maxBorder.position.y)
        {
            _nodeState = NodeState.SUCCESS;
        }
        else
        {
            _nodeState = NodeState.FAILURE;
        }


        return _nodeState;
    }
}
