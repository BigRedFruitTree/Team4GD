using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public GameObject Dialouge;
    public GameObject Background;
    private PlayerMovement1 playerScript;
    private bool show = false;

    public float timer = 0f;
    public float timeToHide = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
        playerScript = GameObject.Find("blue_0").GetComponent<PlayerMovement1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (show == true && timer >= timeToHide)
        {
            timer = 0;
            Dialouge.SetActive(false);
            Background.SetActive(false);
            show = false;

        }

        if(show == true)
            timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player")) 
        {
            show = true;
            Dialouge.SetActive(true);
            Background.SetActive(true);
            

        }
        
    }
}
