using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Vector2 = UnityEngine.Vector2;
using Unity.VisualScripting;




public class NewBehaviourScript : MonoBehaviour
{
    public GameManager gm;
    //THIS IS FOR THE LEVEL 2 BOSS!!
    [Header("Health")]
    public int health = 50;
    public int maxHealth = 50;
    public Slider healthbar;

    [Header("Player Stuff")]
    public NavMeshAgent agent;
    public PlayerMovement1 player;
    public float dismaz = 10;
    public float dismin;
    public Transform Player;
    public bool insightrange = false;
    public LayerMask PlayerLayer;
    public Vector2 Playerloco;

    [Header("Movement")]
    public Transform Boss1;
    public int speed = 5;
    public bool right = false;
    public Vector3 pos;
    public Vector3 localScale;
    public GameObject PlayerGame;
    private BoxCollider2D BossCollider;
    private int Active = 1;
    private float StunLength;
    public float StunLengthSet;


    [Header("Attack")]
    public GameObject attack1HB;
    private float attackTimer;
    public float attackTimerSet;
    public bool bossAtacking = false;
    public int attackNumber = 0;
    private float StunTimer;
    public float StunTimerSet;
    private int StunNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = attackTimerSet;
        StunTimer = StunTimerSet;
        attack1HB.SetActive(false);

        health = maxHealth;

        PlayerGame = GameObject.Find("blue_0");       

        localScale = transform.localScale;

        BossCollider = GameObject.Find("Boss").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame 
    void Update()
    {

        DetectPlayer();

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer <= 0 && bossAtacking == false)
        {
            attackTimer = 0;
        }

        if (attackTimer <= 0 && bossAtacking == false && attackNumber == 0)
        {
            attackNumber = Random.Range(1, 3);
        }

        if (StunTimer <= 0 && StunNumber == 0)
        {
            StunNumber = Random.Range(1, 3);
        }

        if (StunNumber == 3)
        {
            Stuneffect();
        }
        
        if (StunNumber == 1)
        {
            StunNumber = 0;
            StunTimer = StunTimerSet;
        }
        if (StunNumber == 2)
        {
            StunNumber = 0;
            StunTimer = StunTimerSet;
        }

        //rework this
        if (StunLength > 0)
        {
           StunLength -= Time.deltaTime;
        }
        else
        {
            attackTimer = attackTimerSet;
            Active = 1;
            StunTimer = StunTimerSet;

        }

        if (attackTimer <= 0 && bossAtacking == false)
        {
            if (attackNumber == 1)
            {
                attack1HB.SetActive(true);
                StartCoroutine("AttackLength");
                bossAtacking = true;
                attackNumber = 0;
            } else if (attackNumber == 2)
            {
                StartCoroutine("FailedAttackWait");
            }
        }

        if (Active == 1)
        {
            if (insightrange == true)
            {
                CheckFacing();
                if (right == true)
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                if (right == false)
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        pos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            BossCollider.isTrigger = true;
            StartCoroutine("BossCollidesWithPlayer");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nail")
        {
            health--;
            healthbar.value = health;
        }
    }

    IEnumerator AttackLength()
    {

        yield return new WaitForSeconds(2f);
        bossAtacking = false;
        attackTimer = 3;
        attack1HB.SetActive(false);
    }

    IEnumerator FailedAttackWait()
    {

        yield return new WaitForSeconds(1f);
        bossAtacking = false;       
        attackTimer = 3;
        attack1HB.SetActive(false);
        attackNumber = 0;
    }

    IEnumerator BossCollidesWithPlayer()
    {
        yield return new WaitForSeconds(2f);
        BossCollider.isTrigger = false;
    }

    public void DetectPlayer()
    {
            
        foreach(var collider in Physics2D.OverlapCircleAll(transform.position, dismaz, PlayerLayer))
        {
            Playerloco = collider.transform.position;
            insightrange = true;
        }
    }

    public void CheckFacing() 
    {
        if (Player.position.x < Boss1.position.x)
            right = false;

        if (Player.position.x > Boss1.position.x)
            right = true;

        if (((right) && (localScale.x > 0)) || ((!right) && (localScale.x < 0))) 
        {
           localScale.x *= -1;
        }
		transform.localScale = localScale;
    }
    void Stuneffect()
    {
        attackTimer = 1000000;
        bossAtacking = false;
        Active = 0;
        StunLength = StunLengthSet;
    }
}

