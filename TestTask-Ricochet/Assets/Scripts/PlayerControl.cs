using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject MovePoint;
    public GameObject FlashPoint;
    public GameObject Bullet;
    [SerializeField]
    private float speed = 1.0f;
    public float zAngle = 0.5f;
    public Collider2D Point;

    private void Start()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), Point);
    }
    void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
         if (Input.GetKey("left"))
         {
             gameObject.transform.Rotate(0.0f, 0.0f, zAngle, Space.Self);
         }
         if (Input.GetKey("right"))
         {
             gameObject.transform.Rotate(0.0f, 0.0f, -zAngle, Space.Self);
         }
         if (Input.GetKey("up"))
         {
             gameObject.transform.position = Vector2.MoveTowards(transform.position, MovePoint.transform.position, step);
         }
         if (Input.GetKey("down"))
         {
             gameObject.transform.position = Vector2.MoveTowards(transform.position, MovePoint.transform.position, -step);
         }
    }

    private void Shoot() 
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject b = Instantiate(Bullet, FlashPoint.transform.position, transform.rotation);
            Destroy(b,3f);
        }
    }
}
