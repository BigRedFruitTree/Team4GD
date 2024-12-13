using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float frequency = 20f;
    [SerializeField] public float magnitude = 0.5f;

    public GameManager gm;

    public bool facingRight = true;
    
    public float maxHealth = 2f;

    public float currentHealth = 2;

    public SpriteRenderer enemySprite;

    Vector3 pos, localScale;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip deathSound;
    private bool isPlayingAudio = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        pos = transform.position;

        localScale = transform.localScale;

        enemySprite = GameObject.Find("EnemyFlying").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }
        transform.localScale = localScale;


        if (facingRight && currentHealth > 0)
        {
           MoveRight();
        }

        if (!facingRight && currentHealth > 0)
        {
            MoveLeft();
        }

        if (currentHealth <= 0)
        {
            StartCoroutine("Death");
        }

        if (currentHealth <= 0 && isPlayingAudio == false)
        {
           audioSource.PlayOneShot(deathSound);
           isPlayingAudio = true;
        }

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
        if (collision.gameObject.name == "Nail" && gm.isPaused == false)
        {
            currentHealth--;
            StartCoroutine("OnHit");

        }

        if (collision.gameObject.tag == "EnemyTurningPoint" && gm.isPaused == false)
        {
            facingRight = !facingRight;

        }

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
