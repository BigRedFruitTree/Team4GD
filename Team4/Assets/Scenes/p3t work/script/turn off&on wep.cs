using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnoffonwep : MonoBehaviour
{
    public float timeToshow = 0.8f;
    private float timer = 0f;
    SpriteRenderer spriteSquare;
    BoxCollider2D attackAreaCollider;
    public bool FacingRight = true;
    public CharacterController2D character;
    public GameManager gm;


    public bool canAttack = true;

    public AudioSource audioSource;
    public AudioClip attack;

    public Animator animator;

    public bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteSquare = gameObject.GetComponent<SpriteRenderer>();
        attackAreaCollider = gameObject.GetComponent<BoxCollider2D>();
        character = GameObject.Find("blue_0").GetComponent<CharacterController2D>();
        audioSource = GameObject.Find("blue_0").GetComponent<AudioSource>();
        animator = GameObject.Find("blue_0").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canAttack == true && gm.isPaused == false)
        {
           attacking = true;
           audioSource.PlayOneShot(attack);
           Flip();
           canAttack = false;
           spriteSquare.enabled = true;
           attackAreaCollider.enabled = true;
           StartCoroutine("HitCoolDown");
            
        }
        if(gm.isPaused == false)
        timer += Time.deltaTime;

        if (timer >= timeToshow && gm.isPaused == false)
        {
            timer = 0;
            spriteSquare.enabled = false;
            attackAreaCollider.enabled = false;

        }

    }

    private void Flip()
    {
        FacingRight = !FacingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= 1;
        transform.localScale = theScale;
    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
        attacking = false;

    }
}
