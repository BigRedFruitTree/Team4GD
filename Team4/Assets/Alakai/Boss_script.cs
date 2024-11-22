using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    //THIS IS FOR THE LEVEL 2 BOSS!!
    public int health = 50;
    public int maxHealth = 50;
    public Slider healthbar;

    // Start is called before the first frame update
    void Start()
    {
        

        health = maxHealth;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nail")
        {
            health--;
            healthbar.value = health;
        }
    }
}
