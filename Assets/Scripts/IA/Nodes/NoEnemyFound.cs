using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEnemyFound : Node
{
    public Soldat soldat;

    public NoEnemyFound(Soldat s)
    {
        soldat = s;
    }
    public override NodeState Evaluate()
    {
        List<Soldat> enemies = Game.Instance.GetEnemies(soldat.team);

        _nodeState = NodeState.SUCCESS;
        foreach (Soldat enemy in enemies)
        {
            if (enemy != null)
            {
                _nodeState = NodeState.FAILURE;
                break;
                
            }
        }
        return _nodeState;
    }
}
