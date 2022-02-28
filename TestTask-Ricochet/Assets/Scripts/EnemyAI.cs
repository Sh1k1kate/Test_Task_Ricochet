using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject MovePoint;
    public GameObject FlashPoint;
    public GameObject Gun;
    public GameObject Bullet;
    public float speed, startWaitTime, minX, maxX, minY, maxY, zAngle;

    private float waitTime,waittime;
    private float minShootWait = 2f;
    [SerializeField]
    private LayerMask collisionLayer;

    private Transform playerTransform;
    private Vector3 direction;
    private float angle;

    public Transform moveSpots;
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
   
    }
    private void Start()
    {
        waitTime = startWaitTime;
        moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
    void Update()
    {

        // gameObject.transform.Rotate(0.0f,0.0f,zAngle,Space.Self);
        Patrol();
        if (Physics2D.Raycast(FlashPoint.transform.position, FlashPoint.transform.up, 13f, collisionLayer))
        {
            if (Time.time > waittime)
            {
                waittime = Time.time + minShootWait;
                Shoot();
            }

        }
        Debug.DrawRay(FlashPoint.transform.position, FlashPoint.transform.up * 10f, Color.red);
    }

    private void Shoot()
    {
        GameObject b = Instantiate(Bullet, FlashPoint.transform.position, transform.rotation);
        Destroy(b, 3f);

    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void LookAround()
    {
        var rand = Random.Range(1f, 3f);
        while(Time.time > waitTime)
        {
            waitTime = Time.time + rand;
            gameObject.transform.Rotate(0.0f, 0.0f, zAngle, Space.Self);
        }
    }
}
