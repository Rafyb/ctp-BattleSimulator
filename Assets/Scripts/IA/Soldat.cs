using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public float speed;
    public float cooldown;

    [HideInInspector] public Soldat cible;
    [HideInInspector] public Vector2 positionToMove;
    private Node _brain;

    // Start is called before the first frame update
    void Start()
    {
        InitBrain();
    }

    private void Update()
    {
        position = new Vector2(transform.position.x, transform.position.y);



    }

    private void OnDrawGizmos()
    {
        var increment = 10;
        Vector3 pos = new Vector3(position.x, position.y, 0f);
        
        for (int angle = 0; angle < 360; angle = angle + increment)
        {
            Handles.DrawWireDisc(pos, new Vector3(0, 0, 1), maxDist);
            Handles.DrawWireDisc(pos, new Vector3(0, 0, 1), minDist);
        }
    }

    void InitBrain()
    {
        List<Node> firstStep = new List<Node>();
        {
            Node noEnemy = new NoEnemyFound(this);
            firstStep.Add(noEnemy);

            List<Node> attaquerNodes = new List<Node>();
            {
                Node range = new IsInRange(this);
                attaquerNodes.Add(range);
                Node cooldown = new WaitCooldown(this);
                attaquerNodes.Add(cooldown);
                Node attack = new Attack(this);
                attaquerNodes.Add(attack);
            }
            Sequence attaquerSquence = new Sequence(attaquerNodes);
            firstStep.Add(attaquerSquence);

            List<Node> deplacerNodes = new List<Node>();
            {
                Node position = new PositionToMove(this);
                deplacerNodes.Add(position);
                Node moveTo = new MoveTo(this);
                deplacerNodes.Add(moveTo);
            }
            Sequence deplacementSequence = new Sequence(deplacerNodes);
            firstStep.Add(deplacementSequence);
        }
        _brain = new Selector(firstStep);
    }

    public void TakeHit(int dmg)
    {
        GetComponent<SpriteRenderer>().DOColor(Color.red, 0.5f).OnComplete(() =>
        {
            health -= dmg;
            GetComponent<SpriteRenderer>().DOColor(Color.white, 0.5f);

        }); ;
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
