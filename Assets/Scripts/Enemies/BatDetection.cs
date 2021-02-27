using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDetection : MonoBehaviour
{
    public Bat myBat;
    public Transform lowest;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && myBat.ready)
        {
            myBat.Activate(lowest.position.y);
        }
    }
}
