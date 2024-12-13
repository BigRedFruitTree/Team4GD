using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossstings : MonoBehaviour
{
    public GameManager gm;
    public GameObject stinger;
    public Transform stingpos;
    private float bullettime;
    public float tillnext = 2f;

    // OnEnable is called before the first frame Everytime
    void OnEnable()
    {
        this.GetComponent<bossattacking>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        bullettime += Time.deltaTime;

        if(bullettime > tillnext)
        {
            bullettime = 0;
            shoot();
        }
        
    }
    void shoot()
    {
        Instantiate(stinger, stingpos.position, Quaternion.identity);
    }
}
