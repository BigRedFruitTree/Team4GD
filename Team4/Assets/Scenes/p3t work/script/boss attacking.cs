using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossattacking : MonoBehaviour
{
    public bossmovement move;
    public float hitdelay = 2;
    // Start is called before the first frame update
    void Start()
    {
        move.moveSpeed = 0f;
        move.magnitude = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitdelay > 0)
        {
            hitdelay -= Time.deltaTime;
        }
        else
        {
            move.frequency = 0f;
            hitdelay = 2;
        }
    }
}
