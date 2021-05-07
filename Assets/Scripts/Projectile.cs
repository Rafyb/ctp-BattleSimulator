using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Soldat cible;
    public float speed;
    public int damage;


    // Update is called once per frame
    void Update()
    {
        Soldat toPoint = cible;
        if (toPoint == null) Destroy(gameObject);
        else
        {
            transform.position = Vector2.Lerp(transform.position, toPoint.position, speed * Time.deltaTime);

            Vector2 dir = toPoint.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            if (Vector2.Distance(transform.position, toPoint.position) < 0.05f)
            {
                toPoint.TakeHit(damage);
                Destroy(gameObject);
            }
        }
    }
}
