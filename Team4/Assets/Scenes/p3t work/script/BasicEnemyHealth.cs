using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : MonoBehaviour
{
    public float maxHealth = 2f;

    public float currentHealth = 2;

    private float speed = 5;
    private bool FacingRight = false;


    void Start()
    {
        currentHealth = maxHealth;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (FacingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (!FacingRight)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nail")
        {
            currentHealth--;
            Debug.Log("hit");
        }

        if (collision.gameObject.tag == "EnemyTurningPoint")
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Wall")
        {
            Flip();
        }
        
    }

    private void Flip()
    {

        FacingRight = !FacingRight;


        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
