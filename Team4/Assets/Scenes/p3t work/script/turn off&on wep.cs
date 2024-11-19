using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnoffonwep : MonoBehaviour
{
    public float timeToshow = 2f;
    private float timer = 0f;
    SpriteRenderer spriteSquare;
    BoxCollider2D attackAreaCollider;

    public bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteSquare = gameObject.GetComponent<SpriteRenderer>();
        attackAreaCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canAttack == true)
        {
           canAttack = false;
           spriteSquare.enabled = true;
           attackAreaCollider.enabled = true;
            StartCoroutine("HitCoolDown");
            
        }
        timer += Time.deltaTime;

        if (timer >= timeToshow)
        {
            timer = 0;
            spriteSquare.enabled = false;
            attackAreaCollider.enabled = false;

        }

    }

    IEnumerator HitCoolDown()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;

    }
}
