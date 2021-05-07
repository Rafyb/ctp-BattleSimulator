using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War : MonoBehaviour
{
    public List<Soldat> team2;
    public List<Soldat> team1;

    public float timer = 60f;


    public void UpdateIA()
    {
        timer -= Time.deltaTime;
        Game.Instance.ui.SetTimer((int)Mathf.Ceil(timer));

        bool teamDeath = true;
        foreach(Soldat s in team1)
        {
            if (s != null)
            {
                s.Evaluate();
                teamDeath = false;
            }
        }
        if (teamDeath)
        {
            Game.Instance.state = GameState.LOSE;
            return;
        }

        teamDeath = true;
        foreach (Soldat s in team2)
        {
            if (s != null)
            {
                s.Evaluate();
                teamDeath = false;
            }
        }
        if (teamDeath)
        {
            Game.Instance.state = GameState.WIN;
            return;
        }

        if(timer <= 0f) Game.Instance.state = GameState.LOSE;
    }



}
