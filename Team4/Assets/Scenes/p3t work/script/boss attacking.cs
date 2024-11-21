using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossattacking : MonoBehaviour
{
    enum AttackType { multishot, sweap }
    [SerializeField] private AttackType attacktype;
    public GameObject stinger;
    public Transform stingpos;
    private float bullettime;

    public bossmovement move;
    private int Mult = 0;
    private int Multileft = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<bossstings>().enabled = false;
        move.moveSpeed = 0f;
        move.magnitude = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
    if (move.frequency > 0)
        {
            move.frequency -= Time.deltaTime;
        }
        else
        {
            move.frequency = 0;
            Mult = 1;
            Multileft = 3;
        }
    if (Mult > 0)
        {
            
        }
    if (Multileft > 0)
        {

        }
    }
}
