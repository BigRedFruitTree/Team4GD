using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;




public class NewBehaviourScript : MonoBehaviour
{
    public GameManager gm;
    //THIS IS FOR THE LEVEL 2 BOSS!!
    public int health = 50;
    public int maxHealth = 50;
    public Slider healthbar;
    public NavMeshAgent agent;
    public PlayerMovement1 player;
    public float dismaz;
    public float dismin;
    public Transform Player;
    public bool insightrange;
    public LayerMask PlayerLayer;
    public Vector2 Playerloco;

    public GameObject attack1HB;
    public float attackTimer = 10;
    public bool bossAtacking = false;
    public int attackNumber = 0;
    

    public void DetectPlayer()
    {
        insightrange = false;
            
        foreach(var collider in Physics2D.OverlapCircleAll(transform.position, dismaz, PlayerLayer))
        {
            Playerloco = collider.transform.position;
            insightrange = true;


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        attack1HB.SetActive(false);

        health = maxHealth;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
    
}

