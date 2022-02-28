using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    Vector3 prevVelocity;
    private Rigidbody2D rb;
    [SerializeField]
    private float force = 15.0f;
    public CircleCollider2D Bullet, Point;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
        Point = GameObject.FindGameObjectWithTag("Point").GetComponent<CircleCollider2D>();
        Physics2D.IgnoreCollision(Bullet,Point);
    }
    void Update()
    {
        prevVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameManager.enemyScr += 1;
            Destroy(gameObject);
            GameManager.Instance.RestartScene();
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.playerScr += 1;
            Destroy(gameObject);
            GameManager.Instance.RestartScene();
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            var speed = prevVelocity.magnitude;
            var direction = Vector3.Reflect(prevVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed, 0f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
