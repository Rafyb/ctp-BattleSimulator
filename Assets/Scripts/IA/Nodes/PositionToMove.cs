using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToMove : Node
{
    Soldat soldat;
    public PositionToMove(Soldat s)
    {
        soldat = s;
    }

    public override NodeState Evaluate()
    {
        List<Soldat> enemies = Game.Instance.GetEnemies(soldat.team);

        float distSaved = 100f;
        soldat.cible = null;

        foreach (Soldat enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = Vector2.Distance(enemy.position, soldat.position);

                if (distance < distSaved)
                {
                    distSaved = distance;
                    soldat.cible = enemy;
                }
                

            }
        }

        if(soldat.minDist > 0f && distSaved < soldat.minDist)
        {
            // reculer
        }
        else
        {
            // avancer
            soldat.positionToMove = Vector2.Lerp(soldat.cible.position, soldat.position, soldat.minDist/100);
        }

        if(soldat.positionToMove.x > Game.Instance.minBorder.position.x && soldat.positionToMove.y > Game.Instance.minBorder.position.y && soldat.positionToMove.x < Game.Instance.maxBorder.position.x && soldat.positionToMove.y < Game.Instance.maxBorder.position.y)
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
