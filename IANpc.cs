using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IANpc : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stopping_Distance = 2f;

    private Rigidbody2D rb2d;
    private int currentWaypoint = 0;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < stoppingDistance)
        {
            currentWaypoint++;

            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
        else
        {
            Vector2 direction = (waypoints[currentWaypoint].position - transform.position).normalized;
            rb2d.velocity = direction * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PJprincipal")
        {
            rb2d.velocity = Vector2.zero;
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PJprincipal")
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
        }
    }
}
