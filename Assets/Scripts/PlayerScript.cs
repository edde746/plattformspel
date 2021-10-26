using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce;
    public float movementSpeed;
    Rigidbody2D rigidBody;
    BoxCollider2D collider;
    Vector3 startPosition;
    public GameObject fireball;
    public AudioSource fireballSound;

    public int lives = 3;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
    }

    private bool IsOnGround()
    {
        var offset = new Vector3(collider.size.x / 2, 0);
        var distance = collider.size.y / 1.95f;
        return Physics2D.Raycast(transform.position - offset, Vector2.down, distance, 1 << 3) ||
            Physics2D.Raycast(transform.position + offset, Vector2.down, distance, 1 << 3);
    }

    public bool walkingRight = true;
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(new Vector2(movementSpeed - rigidBody.velocity.x, 0), ForceMode2D.Impulse);
            walkingRight = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(new Vector2((-movementSpeed) - rigidBody.velocity.x, 0), ForceMode2D.Impulse);
            walkingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
            rigidBody.AddForce(new Vector2(0, jumpForce));

        if (Input.GetKeyDown(KeyCode.F))
        {
            var p = Instantiate(fireball, transform.position + new Vector3(walkingRight ? collider.size.x / 2 : -collider.size.x / 2, 0), Quaternion.identity);
            var pRb = p.GetComponent<Rigidbody2D>();
            pRb.AddForce(new Vector2(walkingRight ? 2 : -2, 0), ForceMode2D.Impulse);
            fireballSound.Play();
        }
    }

    public void DoDie()
    {
        lives -= 1;
        if (lives <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = startPosition;
        }
    }

    public int GetLives()
    {
        return lives;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Finished level!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DoDie();
        }
    }
}
