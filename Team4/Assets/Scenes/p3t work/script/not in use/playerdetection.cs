using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerdetection : MonoBehaviour
{

    [field: SerializeField]

    public bool Playerinarea { get; private set; }
    public Transform Player { get; private set; }

    [SerializeField]
    private string detectionTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            Playerinarea = true;
            Player = collision.gameObject.transform;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            Playerinarea = false;
            Player = null;
        }
    }
}
