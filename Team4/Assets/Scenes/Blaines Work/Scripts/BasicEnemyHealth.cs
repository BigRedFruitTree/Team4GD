using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : MonoBehaviour
{
    public float maxHealth = 2f;

    public float currentHealth = 2;

    public float speed = 5;
    private bool FacingRight = false;

    public SpriteRenderer enemySprite;

    public GameManager gm;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip deathSound;
    private bool isPlayingAudio = false;


    void Start()
    {
        currentHealth = maxHealth;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine("Death");
        }

        if (currentHealth <= 0 && isPlayingAudio == false)
        {
           audioSource.PlayOneShot(deathSound);
           isPlayingAudio = true;
        }

        if (FacingRight && currentHealth > 0 && gm.isPaused == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (!FacingRight && currentHealth > 0 && gm.isPaused == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Nail" && gm.isPaused == false)
        {
            currentHealth--;
            StartCoroutine("OnHit");
            
        }

        if (collision.gameObject.tag == "EnemyTurningPoint")
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Wall")
        {
            Flip();
        }
        
    }

    private void Flip()
    {

        FacingRight = !FacingRight;


        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator OnHit()
    {
        enemySprite.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        enemySprite.color = new Color(1f, 1f, 1f, 1f);
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        
    }
}
