using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpickup : MonoBehaviour
{
    PlayerMovement1 playerhealth;

    public int healthBonus = 5;

    private void Awake()
    {
        playerhealth = GameObject.Find("Player").GetComponent<PlayerMovement1>();
    }
   

    void OnTriggerEnter2D(Collider2D col)
    {
        if (playerhealth.health < playerhealth.maxHealth)
        {
            Destroy(gameObject);
            playerhealth.health = playerhealth.health + healthBonus;
        }
    }
}
