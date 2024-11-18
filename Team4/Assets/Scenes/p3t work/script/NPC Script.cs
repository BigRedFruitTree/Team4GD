using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public GameObject Dialouge;
    private PlayerMovement1 playerScript;

    // Start is called before the first frame update
    void Start()
    {
        
        playerScript = GameObject.Find("blue_0").GetComponent<PlayerMovement1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player")) 
        {
            Dialouge.SetActive(true);
            
        }
            
    }
}
