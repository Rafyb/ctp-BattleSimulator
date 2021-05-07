using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    [Serializable] public struct Enemy
    {
        public GameObject soldat;
        public Vector2 position;
    }

    [SerializeField]  public Enemy[] enemies;

    public int soldatMax;
    public int nbCAC;
    public int nbArcher;
    public int nbDistance;

}
