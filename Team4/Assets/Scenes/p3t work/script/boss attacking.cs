using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossattacking : MonoBehaviour
{
    enum AttackType { multishot, sweap }
    [SerializeField] private AttackType attacktype;
    public GameObject stinger;
    public Transform stingpos;
    public float firerate;
    public float firerateNext;
    public float Multtime = 1;
    public bossmovement move;
    private int Mult = 0;
    private int Multileft = 0;
    private float timetillstop = 2;
    // Start is called before the first frame update
    void OnEnable()
    {
        timetillstop = 2;
        Multileft = 1;
        this.GetComponent<bossstings>().enabled = false;
        move.moveSpeed = 0f;
        move.magnitude = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
    if (timetillstop > 0)
        {
            timetillstop -= Time.deltaTime * Multtime;
        }
        else
        {
            multishot();
            timetillstop = 100000;
        }
    if (Mult > 0)
    {
            if (firerate > 0f)
            {
                firerate -= Time.deltaTime;
            }
            else
            {
            Multileft--;
            shoot();
            firerate = firerateNext;
            }
        }
    if (Multileft <= 0)
        {
            move.moveSpeed = move.moveSpeedredo;
            move.frequency = move.frequencyredo;
            move.magnitude = move.magnituderedo;
            move.timeuntilattack = move.timeuntilattackredo;
            Mult = 0;
            this.GetComponent<bossstings>().enabled = true;
        }
    }
    void multishot()
    {
     Mult = 1;
     Multileft = 3;
    }
    void shoot()
    {
        Instantiate(stinger, stingpos.position, Quaternion.identity);
    }
}
