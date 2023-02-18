using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IANpc : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float vel_mov = 2f;
    [SerializeField] private float stopping_Distance = 2f;

    private Rigidbody2D rb2d;
    private SpriteRenderer sp;
    private int currentWaypoint = 0;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < stopping_Distance)
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
            sp.flipX = (direction.x < 0);
            rb2d.velocity = direction * vel_mov;
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
