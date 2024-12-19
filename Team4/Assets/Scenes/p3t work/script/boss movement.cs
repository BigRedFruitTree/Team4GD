﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossmovement : MonoBehaviour {

	[SerializeField]
	public float moveSpeed;
    public float moveSpeedredo;

    [SerializeField]
	public float frequency;
    public float frequencyredo;

    [SerializeField]
	public float magnitude;
    public float magnituderedo;
    public float farmove;

    public float timeuntilattack;
    public float timeuntilattackredo;

    bool facingRight = true;

	Vector3 pos, localScale;

    [SerializeField] public int health = 10;
    public int maxHealth;
    public Slider healthbar;
    public bool canTakeDamage = true;

    private SpriteRenderer bossSprite;
    public GameObject healthObject;
    private Quaternion posRO;
    public Vector3 bossPos;
    public bool amIDead = false;
    public Rigidbody2D rb;
    

    public GameManager gm;

    [Header("Audio")]
    public AudioSource bossAudioSource;
    public AudioClip deathSound;
    private bool isPlayingAudio = false;
    // Use this for initialization
    void Start ()
    {
        health = maxHealth;
        pos = transform.position;
        

		localScale = transform.localScale;
        bossSprite = GameObject.Find("b").GetComponent<SpriteRenderer>();
        rb = GameObject.Find("b").GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        bossPos = transform.position;
		
		CheckWhereToFace ();

		if (facingRight)
			MoveRight ();
		else
			MoveLeft ();
        if (timeuntilattack > 0 && gm.isPaused == false)
        {
            timeuntilattack -= Time.deltaTime;
        }
        else
        {
            if(gm.isPaused == false)
            {
              this.GetComponent<bossattacking>().enabled = true;
              timeuntilattack = 0;
            }
            
        }

        if (health <= 0 && isPlayingAudio == false && gm.isPaused == false)
        {
           bossAudioSource.PlayOneShot(deathSound);
           isPlayingAudio = true;
        }

        if (health <= 0)
        {
            StartCoroutine("DeathOfBoss");
        }
        if(gm.isPaused == true)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

	void CheckWhereToFace()
	{
		if (pos.x < -farmove && gm.isPaused == false)
			facingRight = true;
		
		else if (pos.x > farmove && gm.isPaused == false)
			facingRight = false;

		if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
			localScale.x *= -1;

		transform.localScale = localScale;

	}

	void MoveRight()
	{
        if(gm.isPaused == false)
        {
          pos += transform.right * Time.deltaTime * moveSpeed;
		  transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        }
		
	}

	void MoveLeft()
	{
        if(gm.isPaused == false)
        {
          pos -= transform.right * Time.deltaTime * moveSpeed;
		  transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        }
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nail" && canTakeDamage == true && gm.isPaused == false)
        {
            canTakeDamage = false;
            StartCoroutine("OnHit");
            health--;
            healthbar.value = health;
            StartCoroutine("HitCoolDown");
            if (health == 5)
            {
                Instantiate(healthObject, bossPos, posRO);
            }
        }
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }

    IEnumerator OnHit()
    {
        bossSprite.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        bossSprite.color = new Color(1f, 1f, 1f, 1f);
    }

     IEnumerator DeathOfBoss()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        amIDead = true;
    }
}