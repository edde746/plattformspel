using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var snail = collision.gameObject.GetComponent<SnailScript>();
            if (snail)
                snail.Kill();
            Destroy(gameObject.GetComponent<CircleCollider2D>());
        }
    }
}
