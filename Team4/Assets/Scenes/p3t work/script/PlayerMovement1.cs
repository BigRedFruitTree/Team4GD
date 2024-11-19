using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour {

    public CharacterController2D controller;

    public int health = 10;
    public int maxHealth = 10;
    public bool canTakeDamage = true;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    [SerializeField] public int jumps = 2;
    int jumpsMax = 2;

    
	// Update is called once per frame
	void Update () {

        if (health > maxHealth)
        health = maxHealth;

        if (health <= 0)
            Destroy(gameObject);

        controller = GetComponent<CharacterController2D>();

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump") && jumps > 0)
		{
            jumps--;
			jump = true;
		}	

        if(controller.m_Grounded)
        {
            jumps = jumpsMax;
        }

        if (jumps < 0)
        {
            jumps = 0;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBasic" && canTakeDamage == true)
        {
            canTakeDamage = false;
            StartCoroutine("HitCoolDown");
            
            
        }
    }



    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(2);
        canTakeDamage = true;
        
    }

   
}
