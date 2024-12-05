using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour {

    public CharacterController2D controller;
    public GameObject nailPos;
    public GameObject Nail;
    private GameObject Player;
    private Rigidbody2D PlayerRB;
    public GameObject endScreen;

    public int health = 10;
    public int maxHealth = 10;
    public bool canTakeDamage = true;

    public float runSpeed = 40f;
    public float knockbackForce = 1000f;

    float horizontalMove = 0f;
    bool jump = false;
    [SerializeField] public int jumps = 2;
    int jumpsMax = 2;

    public int healthBonus = 5;

    private bool endScreenShown = false;

    private void Start()
    {
        Player = GameObject.Find("blue_0");
        PlayerRB = GameObject.Find("blue_0").GetComponent<Rigidbody2D>();
        Player = GameObject.Find("blue_0 1");
        PlayerRB = GameObject.Find("blue_0 1").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        if (health > maxHealth)
        health = maxHealth;

        if (health <= 0)
        {
            endScreen.SetActive(true);
            endScreenShown = true;
        }
    
        controller = GetComponent<CharacterController2D>();

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        nailPos = GameObject.Find("NailPos");

        Nail = GameObject.Find("Nail");

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

        Nail.transform.position = nailPos.transform.position;
        Nail.transform.rotation = nailPos.transform.rotation;

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBasic" && canTakeDamage == true)
        {
            canTakeDamage = false;
            StartCoroutine("HitCoolDown");
            health--;
        }
        if (collision.gameObject.tag == "EnemyFlying" && canTakeDamage == true)
        {
            canTakeDamage = false;
            StartCoroutine("HitCoolDown");
            health--;
        }

        if (collision.gameObject.name == "BossAttack1" && canTakeDamage == true)
        {
            canTakeDamage = false;
            StartCoroutine("HitCoolDown");
            health--;
        }

        if (collision.gameObject.name == "health object")
        {
            if (endScreenShown == true)
            {
                 Destroy(collision.gameObject);
                 health = health + healthBonus;
                if (health > maxHealth)
                   health = maxHealth;
            }
           
        }

        if (collision.gameObject.name == "stinger prefab(Clone)" && canTakeDamage == true)
        {
            canTakeDamage = false;
            StartCoroutine("HitCoolDown");
            health--;
        }

        if (collision.gameObject.name == "b" && canTakeDamage == true)
        {
            canTakeDamage = false;
            StartCoroutine("HitCoolDown");
            health--;
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Boss1" && canTakeDamage == true)
        {
            canTakeDamage = false;
            StartCoroutine("HitCoolDown");
            health--;
        }
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }
}
