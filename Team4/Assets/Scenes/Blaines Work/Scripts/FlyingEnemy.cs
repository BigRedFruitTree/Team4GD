using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float frequency = 20f;
    [SerializeField] public float magnitude = 0.5f;

    public bool facingRight = true;
    
    public float maxHealth = 2f;

    public float currentHealth = 2;

    Vector3 pos, localScale;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        pos = transform.position;

        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;


        if (facingRight)
        {
           MoveRight();
        }

        if (!facingRight)
        {
            MoveLeft();
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

   

    void MoveRight()
    {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nail")
        {
            currentHealth--;

        }

        if (collision.gameObject.tag == "EnemyTurningPoint")
        {
            facingRight = !facingRight;

        }

    }
}