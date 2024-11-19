using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerattack : MonoBehaviour
{
    
    private GameObject attackArea;

    public bool attacking = true;
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
        
        if(Input.GetKeyDown(KeyCode.F) && canAttack == true)
        {
            canAttack = false;
            Attack();
            
        }

        if (canAttack = true && attacking)
        {
            timer += Time.deltaTime;
        
            if(timer >= timeToAttack)
            {

               timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }
    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
    }

   
}
