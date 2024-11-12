using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evilguyhealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3f;

    [SerializeField] private float currentHealth;

    private float speed = 5;
    

    void Start()
    {
        currentHealth = maxHealth;

    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector2.left * speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nail")
        {
            currentHealth--;
        }
    }

}
