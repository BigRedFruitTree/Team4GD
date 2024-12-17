using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattack : MonoBehaviour
{
    public GameManager gm;
    private GameObject attackArea;

    public bool attacking = false;
    public bool canAttack = true;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.F) && canAttack == true && gm.reset == false && gm.isPaused == false)
        {
            Attack();
            canAttack = false;
        }

        if (canAttack = true && attacking && gm.reset == false && gm.isPaused == false)
        {
            timer += Time.deltaTime;
        
            if(timer >= timeToAttack)
            {

               timer = 0;
               attackArea.SetActive(attacking);
               attacking = false;
            }
        }
    }
    private void Attack()
    {
        
        attacking = true;
        attackArea.SetActive(attacking);
    }

   
}
