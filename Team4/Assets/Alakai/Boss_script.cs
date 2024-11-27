using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Vector2 = UnityEngine.Vector2;




public class NewBehaviourScript : MonoBehaviour
{
    public GameManager gm;
    //THIS IS FOR THE LEVEL 2 BOSS!!
    public int health = 50;
    public int maxHealth = 50;
    public Slider healthbar;
    public NavMeshAgent agent;
    public PlayerMovement1 player;
    public float dismaz = 10;
    public float dismin;
    public Transform Player;
    public bool insightrange = false;
    public LayerMask PlayerLayer;
    public Vector2 Playerloco;
    public Transform Boss1;
    public Transform Middle;
    public int speed = 5;
    public bool right = false;
    public Vector3 pos;
    public Vector3 localScale;
    
    
    [Header("Attack")]
    public GameObject attack1HB;
    public float attackTimer = 10;
    public bool bossAtacking = false;
    public int attackNumber = 0;
    

    
    // Start is called before the first frame update
    void Start()
    {
        attack1HB.SetActive(false);

        health = maxHealth;
        
       
        localScale = transform.localScale;
    }

    // Update is called once per frame SKibidi
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

        if (insightrange == true)
        {
           CheckFacing();
           if(right == true) 
           {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
           } else 
           {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
           }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        pos = transform.position;
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
        attackTimer = 10;
        attack1HB.SetActive(false);
    }


    IEnumerator FailedAttackWait()
    {

        yield return new WaitForSeconds(1f);
        bossAtacking = false;
        attackTimer = 10;
        attack1HB.SetActive(false);
        attackNumber = 0;
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
        if (Player.position.x < Middle.position.x) 
        {
              right = false; 
        } else if (Player.position.x > Middle.position.x)
        {
			 right = true;
        }
        if (((right) && (localScale.x > 0)) || ((!right) && (localScale.x < 0))) 
        {
          localScale.x *= -1;
        }
		transform.localScale = localScale;
    }

}

