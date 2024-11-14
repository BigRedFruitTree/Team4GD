using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    public int health = 5;
    public int maxHealth = 5;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    [SerializeField] public int jumps = 2;
    int jumpsMax = 2;
	bool crouch = false;
	
	// Update is called once per frame
	void Update () {

        if (health < maxHealth)
            health = 0;

        if (health == 0)
            Destroy(gameObject);

        controller = GetComponent<CharacterController2D>();

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump") && jumps > 0)
		{
            jumps--;
			jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

        if(controller.m_Grounded)
        {
            jumps = jumpsMax;
        }

        if (jumps < 0)
        {
            jumps = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBasic")
        {
            health--;
        }
    }

    void FixedUpdate ()
	{
		// Move character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
