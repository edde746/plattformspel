using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float walkingSpeed = 1;
    bool walkingLeft = true;
    SpriteRenderer renderer;
    Rigidbody2D rigidbody;
    BoxCollider2D collider;
    Vector3 startPosition;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (Physics2D.Raycast(transform.position - new Vector3(collider.size.x/1.8f,0), Vector2.down, collider.size.y) ||
            Physics2D.Raycast(transform.position + new Vector3(collider.size.x / 1.8f, 0), Vector2.down, collider.size.y)) // Make sure we're on ground to walk
            rigidbody.velocity = new Vector2(walkingSpeed, 0) * (walkingLeft ? -1 : 1);
    }

    void ChangeDirection()
    {
        walkingLeft = !walkingLeft;
        renderer.flipX = !walkingLeft;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SwitchDirection"))
        {
            ChangeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.Raycast(transform.position - new Vector3(0, collider.size.y / 1.5f), walkingLeft ? Vector3.left : Vector3.right, collider.size.x / 1.5f))
            ChangeDirection();
    }

    IEnumerator KillSnail()
    {
        Destroy(collider);
        rigidbody.freezeRotation = false;
        rigidbody.MoveRotation(rigidbody.rotation + 30.0f);
        rigidbody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public void Kill()
    {
        StartCoroutine(KillSnail());
    }
}
