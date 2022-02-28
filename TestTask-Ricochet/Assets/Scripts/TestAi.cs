using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestAi : MonoBehaviour
{
    public GameObject FlashPoint, MovePoint, Bullet;
    public float rotspd, speed, startWaitTime, minX, maxX, minY, maxY, zAngle, minReloadTime, distance;
    private float standTime, reloadTime;
    public LayerMask pointColl, PlayerColl;
    public Transform moveSpots;
    private Transform Player;
    public Collider2D Point;
    private bool canshoot = true;
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        standTime = startWaitTime;
        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), Point);
    }

    private void Update()
    {
        Actions();
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots.position) < 2f)
        {
            if (standTime <= 0)
            {
                moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                standTime = startWaitTime;  
            }
            else
            {
                standTime -= Time.deltaTime;
            }
        }
    }

    private void Actions()
    {
        float step = speed * Time.deltaTime;
        if (!Physics2D.Raycast(transform.position, transform.up, 20f, pointColl) && !Physics2D.Raycast(transform.position, FlashPoint.transform.up, 13f, PlayerColl))
        {
            transform.Rotate(0.0f, 0.0f, zAngle, Space.Self);
            transform.position = Vector2.MoveTowards(transform.position, MovePoint.transform.position, step*0.5f);     
        }
        else if (Physics2D.Raycast(transform.position, transform.up, 20f, pointColl))
        {
            Patrol();
        }

        if (Physics2D.Raycast(transform.position, FlashPoint.transform.up, 13f, PlayerColl))
        {
            moveSpots.position = Player.position;
            Shoot();
        }
    }
    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,FlashPoint.transform.up, distance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                canshoot = false;
            }
            else canshoot = true;
        }
        
        if (Time.time > reloadTime && canshoot == true)
        {
            reloadTime = Time.time + minReloadTime;
            GameObject b = Instantiate(Bullet, FlashPoint.transform.position, transform.rotation);
            Destroy(b, 3f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            transform.Rotate(0.0f, 0.0f, zAngle, Space.Self); 
        }
    }
}
