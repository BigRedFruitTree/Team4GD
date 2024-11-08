using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float timeToshow = 2f;
    private float timer = 0f;
    SpriteRenderer spriteSquare;
    // Start is called before the first frame update
    void Start()
    {
        spriteSquare = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            spriteSquare.enabled = true;
        }
        timer += Time.deltaTime;

        if (timer >= timeToshow)
        {
            timer = 0;
            spriteSquare.enabled = false;
           
        }

    }
}
