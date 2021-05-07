using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeSoldat
{
    EPEE, ARCHER, DISTANCE
}

public class Soldat : MonoBehaviour
{

    public TypeSoldat type;
    public int team;
    public Vector2 position;

    public int health;
    public int damage;
    public float minDist;
    public float maxDist;

    private Node _brain;

    // Start is called before the first frame update
    void Start()
    {
        InitBrain();
    }

    void InitBrain()
    {
        List<Node> firstStep = new List<Node>();



        _brain = new Selector(firstStep);
    }

    public void Evaluate()
    {
        _brain.Evaluate();

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
