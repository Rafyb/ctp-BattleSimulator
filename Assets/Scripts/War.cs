using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War : MonoBehaviour
{
    public List<Soldat> team2;
    public List<Soldat> team1;


    public void UpdateIA()
    {


        bool teamDeath = true;
        foreach(Soldat s in team1)
        {
            if (s != null)
            {
                s.Evaluate();
                teamDeath = false;
            }
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
    }



}
