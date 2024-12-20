using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement1 : MonoBehaviour {

    public GameManager gm;
    public CharacterController2D controller;
    public bossmovement boss2Move;
    public Transform boss2Tr;
    public NewBehaviourScript boss1Move;
    public Transform boss1Ref;
    public GameObject nailPos;
    public GameObject Nail;
    public GameObject Player;
    public Transform PTransform;
    private Rigidbody2D PlayerRB;
    public GameObject Endgame;
    public bool endGameActive = false;
    public turnoffonwep playerAttackScript;

    public int health = 10;
    public int maxHealth = 10;
    public bool canTakeDamage = true;

    public float runSpeed = 40f;
    public float knockbackForce = 25f;

    public float horizontalMove = 0f;
    bool jump = false;
    [SerializeField] public int jumps = 2;
    int jumpsMax = 2;
    public bool jumping = false;

    public int healthBonus = 5;

    private SpriteRenderer playerSprite;

    public Animator animator;

    public bool idle = true;

    public GameObject playerIndicator;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hurt;
    public AudioClip jumpAudio;
    public AudioClip healthGain;

    private void Start()
    {
        Player = GameObject.Find("blue_0");
        PTransform = GameObject.Find("blue_0").GetComponent<Transform>();
        PlayerRB = GameObject.Find("blue_0").GetComponent<Rigidbody2D>();
        playerSprite = GameObject.Find("blue_0").GetComponent<SpriteRenderer>();
        audioSource = GameObject.Find("blue_0").GetComponent<AudioSource>();
        animator = GameObject.Find("blue_0").GetComponent<Animator>();
        playerAttackScript = GameObject.Find("Nail").GetComponent<turnoffonwep>();
    }

    // Update is called once per frame
    void Update () {

        if (health > maxHealth)
        health = maxHealth;


        if(SceneManager.GetActiveScene().buildIndex > 3)
        {
           if (boss2Move.amIDead == true)
           {
             Endgame.SetActive(true);
             endGameActive = true;
           } 
        }
        
        controller = GetComponent<CharacterController2D>();

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        nailPos = GameObject.Find("NailPos");

        Nail = GameObject.Find("Nail");

        if (Input.GetButtonDown("Jump") && jumps > 1 && gm.reset == false && gm.isPaused == false)
		{
            jumps--;
			jump = true;
            audioSource.PlayOneShot(jumpAudio);
            jumping = true;
		}	

        if(controller.m_Grounded)
        {
            jumps = jumpsMax;
            jumping = false;
            
        }

        if(controller.m_Grounded == false)
        jumping = true;

        if (jumps < 0)
        {
            jumps = 0;
            jumping = false;
        }

        Nail.transform.position = nailPos.transform.position;
        Nail.transform.rotation = nailPos.transform.rotation;

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

        if (gm.isPaused == false || gm.reset == false)
        {
            PlayerRB.constraints = RigidbodyConstraints2D.None;
            PlayerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (gm.isPaused == true || gm.reset == true)
        {
            PlayerRB.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if(horizontalMove == 0 && playerAttackScript.attacking == false) 
        {
            idle = true;
            animator.SetBool("idle?", true);
        }else
        {
            idle = false;
            animator.SetBool("idle?", false);
        }

        if(playerAttackScript.attacking == true) 
        {
             animator.SetBool("attacking?", true);
             animator.SetBool("walking?", false);
             idle = false;
        }else 
        {
            animator.SetBool("attacking?", false);
        }

        if(horizontalMove > 0 && idle == false && playerAttackScript.attacking == false || horizontalMove < 0 && idle == false && playerAttackScript.attacking == false) 
        {
            animator.SetBool("walking?", true);
        }else 
        {
            animator.SetBool("walking?", false);
        }
         
        if(jumping == true) 
        {
            animator.SetBool("jumping?", true);
            animator.SetBool("walking?", false);
            animator.SetBool("attacking?", false);
        }else 
        {
            animator.SetBool("jumping?", false);
        }
       
        if(gm.isPaused == true)
        {
            animator.SetBool("jumping?", false);
            animator.SetBool("walking?", false);
            animator.SetBool("attacking?", false);
            animator.SetBool("idle?", true);
            jumping = false;
            playerAttackScript.attacking = false;
            horizontalMove = 0;
            animator.SetBool("isPaused?", true);
        }
        else
        {
            animator.SetBool("isPaused?", false);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBasic" && canTakeDamage == true && gm.reset == false && gm.isPaused == false)
        {
            audioSource.PlayOneShot(hurt);
            canTakeDamage = false;
            StartCoroutine("OnHit");
            StartCoroutine("HitCoolDown");
            health--;
        }
        if (collision.gameObject.tag == "EnemyFlying" && canTakeDamage == true && gm.reset == false && gm.isPaused == false)
        {
            audioSource.PlayOneShot(hurt);
            canTakeDamage = false;
            StartCoroutine("OnHit");
            StartCoroutine("HitCoolDown");
            health--;
        }

        if (collision.gameObject.name == "BossAttack1" && canTakeDamage == true && gm.reset == false && gm.isPaused == false)
        {
            audioSource.PlayOneShot(hurt);
            canTakeDamage = false;
            StartCoroutine("OnHit");
            StartCoroutine("HitCoolDown");
            health--;
        }

        if (collision.gameObject.tag == "healthObject" && gm.reset == false && gm.isPaused == false)
        {


            Destroy(collision.gameObject);
            health = health + healthBonus;
            audioSource.PlayOneShot(healthGain);
            if (health > maxHealth)
                health = maxHealth;


        }

        if (collision.gameObject.name == "stinger prefab(Clone)" && canTakeDamage == true && gm.reset == false && gm.isPaused == false)
        {
            audioSource.PlayOneShot(hurt);
            canTakeDamage = false;
            StartCoroutine("OnHit");
            StartCoroutine("HitCoolDown");
            health--;
        }

        if (collision.gameObject.name == "b" && canTakeDamage == true && gm.reset == false && gm.isPaused == false)
        {
            audioSource.PlayOneShot(hurt);
            canTakeDamage = false;
            StartCoroutine("OnHit");
            StartCoroutine("HitCoolDown");
            health--;

            
            if(SceneManager.GetActiveScene().buildIndex > 3) 
            {
               if(PTransform.position.x < boss2Tr.position.x)
               {
                 Vector2 difference = (transform.position - collision.transform.position);
                 Vector2 force = difference * knockbackForce;
                 PlayerRB.AddForce(force, ForceMode2D.Force);
               }

               if(PTransform.position.x > boss2Tr.position.x)
               {
                 Vector2 difference = (transform.position - collision.transform.position).normalized;
                 Vector2 force = difference.normalized * knockbackForce;
                 PlayerRB.AddForce(force, ForceMode2D.Force);
               }
            }
        }

        if (collision.gameObject.tag == "TreeHollow" && gm.reset == false && gm.isPaused == false)
        {
            playerIndicator.SetActive(true);
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Boss1" && canTakeDamage == true && gm.reset == false && gm.isPaused == false)
        {
            audioSource.PlayOneShot(hurt);
            canTakeDamage = false;
            StartCoroutine("OnHit");
            StartCoroutine("HitCoolDown");
            health--;

            if(SceneManager.GetActiveScene().buildIndex > 1 && SceneManager.GetActiveScene().buildIndex < 3) 
            {
               if(PTransform.position.x < boss1Ref.position.x)
               {
                 Vector2 difference = (transform.position - collision.transform.position);
                 Vector2 force = difference * knockbackForce;
                 PlayerRB.AddForce(force, ForceMode2D.Force);
               }

               if(PTransform.position.x > boss1Ref.position.x)
               {
                 Vector2 difference = (transform.position - collision.transform.position).normalized;
                 Vector2 force = difference.normalized * knockbackForce;
                 PlayerRB.AddForce(force, ForceMode2D.Force);
               }
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.tag == "TreeHollow" && gm.reset == false && gm.isPaused == false)
        {
           playerIndicator.SetActive(false);
        }
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }

    IEnumerator OnHit()
    {
        playerSprite.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(1f);
        playerSprite.color = new Color(1f, 1f, 1f, 1f);
    }
}
