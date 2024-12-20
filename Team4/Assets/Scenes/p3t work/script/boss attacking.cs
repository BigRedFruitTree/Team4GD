using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossattacking : MonoBehaviour
{
    enum AttackType { multishot, BIGSHOT }
    [SerializeField] private AttackType attacktype;
    public GameManager gm;
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
        if (attack == 1 && gm.isPaused == false)
        {
            attacktype = AttackType.BIGSHOT;
            attack = 2;
            firerate = firerateBIGNext;
        }
        else
        {
            if(gm.isPaused == false)
            {
               attacktype = AttackType.multishot;
              attack = 1;
              firerate = fireratemultiNext;
            }
           
        }
        if(gm.isPaused == false)
        {
          timetillstop = timetillstopNext;
          Multileft = 1;
          this.GetComponent<bossstings>().enabled = false;
          move.moveSpeed = 0f;
          move.magnitude = 0.2f;
        }
        
    }
    void OnDisable()
    {
        if(gm.isPaused == false)
        stinger.transform.localScale = new Vector3(1f, 1.25f, 1.25f);
    }

    // Update is called once per frame
    void Update()
    {
        if (timetillstop > 0 && gm.isPaused == false)
        {
            timetillstop -= Time.deltaTime * Multtime;
        }
        else
        {
            if(gm.isPaused == false)
            {
              multishot();
              timetillstop = 100000;
            }
            
        }
       if (Mult > 0 && gm.isPaused == false)
       {
            if (firerate > 0f && gm.isPaused == false)
            {
                firerate -= Time.deltaTime;
            }
            else
            {
                if(gm.isPaused == false)
                {
                 Multileft--;
                 shoot();
                }
              
               if (attacktype == AttackType.BIGSHOT && gm.isPaused == false)
               {
                  firerate = firerateBIGNext;
               }
                if (attacktype == AttackType.multishot && gm.isPaused == false)
                {
                    firerate = fireratemultiNext;
                }
            }
       }
        if (Multileft <= 0 && gm.isPaused == false)
        {
            move.moveSpeed = move.moveSpeedredo;
            move.frequency = move.frequencyredo;
            move.magnitude = move.magnituderedo;
            stinger.transform.localScale = new Vector3(1f, 1.25f, 1.25f);
            move.timeuntilattack = move.timeuntilattackredo;
            Mult = 0;
            this.GetComponent<bossstings>().enabled = true;
        }
    }
    void multishot()
    {
        Mult = 1;
        if (attacktype == AttackType.BIGSHOT && gm.isPaused == false)
        {
            Multileft = Multileft1;
            stinger.transform.localScale = new Vector3(1.5f, 2f, 2f);
        }
        if (attacktype == AttackType.multishot && gm.isPaused == false)
        {
            Multileft = Multileft2;
            stinger.transform.localScale = new Vector3(0.6f, 1.25f, 1.25f);
        }
    }
    void shoot()
    {
        if(gm.isPaused == false)
        Instantiate(stinger, stingpos.position, Quaternion.identity);
    }
}
