using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    STARTING, PREPARING, BATTLE, LOSE, WIN
}
public class Game : MonoBehaviour
{
    public static Game Instance;
    void Awake()
    {
        Instance = this;
    }
    public Camera cam;

    public Transform maxBorder;
    public Transform minBorder;

    public UI ui;

    public GameObject[] prefabsTeam1;
    public GameObject[] prefabsTeam2;

    public GameObject[] prefabProjectile;


    public Level[] levels;
    int _levelIdx = 0;

    public GameState state = GameState.STARTING;

    War _war;

    // Start is called before the first frame update
    void Start()
    {
        StartLevel(_levelIdx);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize < 1.6f) cam.orthographicSize+=0.1f;
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && cam.orthographicSize > 0.5f) cam.orthographicSize-=0.1f;


        if (state == GameState.PREPARING)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector2 v = Camera.main.ScreenToWorldPoint(mousePosition);

                if(v.x < (maxBorder.position.x + minBorder.position.x) && v.x > minBorder.position.x && v.y > minBorder.position.y && v.y < maxBorder.position.y)
                {
                    Place(v);
                }
            }
        }
        if(state == GameState.BATTLE)
        {
            _war.UpdateIA();
        }
        if (state == GameState.LOSE || state == GameState.WIN)
        {
            if (_war != null)
            {
                Destroy(_war.gameObject);
                _war = null;

                if(state == GameState.LOSE)
                {
                    ui.OnLose();
                }
                else 
                {
                    ui.OnWin();
                }
            }
        }
    }

    public void Retry()
    {
        StartLevel(_levelIdx);
    }

    public void Next()
    {
        _levelIdx++;
        StartLevel(_levelIdx);
    }

    public void OnStart()
    {
        state = GameState.BATTLE;
    }

    public List<Soldat> GetEnemies(int team)
    {
        if(team == 0)
        {
            return _war.team2;
        }
        return _war.team1;

    }

    private void StartLevel(int idx)
    {
        // Set war
        GameObject warGo = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        warGo.AddComponent<War>();

        _war = warGo.GetComponent<War>();
        _war.team1 = new List<Soldat>();
        _war.team2 = new List<Soldat>();

        // Set ennemies
        foreach(var enemy in levels[idx].enemies)
        {
            GameObject go = Instantiate(enemy.soldat, enemy.position, Quaternion.identity);
            _war.team2.Add(go.GetComponent<Soldat>());
            go.transform.parent = _war.transform;
        }

        // Set UI
        ui.SetRestant(levels[idx].soldatMax);
        ui.SetUnitRestant(0, levels[idx].nbCAC);
        ui.SetUnitRestant(1, levels[idx].nbArcher);
        ui.SetUnitRestant(2, levels[idx].nbDistance);
        ui.OnPrep();

        // Set state
        state = GameState.PREPARING;
    }

    private void Place(Vector2 pos)
    {
        int nb = ui.GetRestant();
        if (nb <= 0) return;

        int idx = ui.GetSelected();
        if (idx == -1) return;

        int nbRest = ui.GetUnitRestant(idx);

        if (nbRest > 0)
        {
            GameObject go = Instantiate(prefabsTeam1[idx], pos, Quaternion.identity);
            _war.team1.Add(go.GetComponent<Soldat>());
            go.transform.parent = _war.transform;
        }
        else return;

        ui.SetRestant(nb - 1);
        ui.SetUnitRestant(idx, nbRest - 1);
    }

    public void Shoot(int idx,Soldat soldat)
    {
        GameObject go = Instantiate(prefabProjectile[idx], soldat.position, Quaternion.identity);
        go.GetComponent<Projectile>().damage = soldat.damage;
        go.GetComponent<Projectile>().cible = soldat.cible;
    }

}
