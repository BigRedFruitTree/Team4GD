using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossattacking : MonoBehaviour
{
    enum AttackType { multishot, BIGSHOT }
    [SerializeField] private AttackType attacktype;
    // starting: 1 = BIGSHOT 2 = multishot
    public int attack = 1;
    public GameObject stinger;
    public Transform stingpos;
    private float firerate;
    public float firerateBIGNext;
    public float fireratemultiNext;
    public float Multtime;
    public bossmovement move;
    private int Mult = 0;
    private int Multileft = 0;
    public int Multileft1;
    public int Multileft2;
    private float timetillstop;
    public float timetillstopNext;
    // OnEnable is called before the first frame Everytime
    void OnEnable()
    {
        if (attack == 1)
        {
            attacktype = AttackType.BIGSHOT;
            attack = 2;
            firerate = firerateBIGNext;
        }
        else
        {
            attacktype = AttackType.multishot;
            attack = 1;
            firerate = fireratemultiNext;
        }
        timetillstop = timetillstopNext;
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
             if (attacktype == AttackType.BIGSHOT)
             {
                  firerate = firerateBIGNext;
             }
                if (attacktype == AttackType.multishot)
                {
                    firerate = fireratemultiNext;
                }
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
        if (attacktype == AttackType.BIGSHOT)
        {
            Multileft = Multileft1;
        }
        if (attacktype == AttackType.multishot)
        {
            Multileft = Multileft2;
        }
    }
    void shoot()
    {
        Instantiate(stinger, stingpos.position, Quaternion.identity);
    }
}
