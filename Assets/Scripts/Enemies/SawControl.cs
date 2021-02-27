using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawControl : MonoBehaviour
{
    public Transform saw;
    public Transform leftLimit, rightLimit;
    public float currentSpot, moveTime;
    public bool right;
    // Start is called before the first frame update
    void Start()
    {
        saw.transform.position = Vector3.Lerp(leftLimit.position,rightLimit.position,currentSpot);
    }

    // Update is called once per frame
    void Update()
    {
        if(right)
        {
            currentSpot += Time.deltaTime / moveTime;
        }
        else
        {
            currentSpot -= Time.deltaTime / moveTime;
        }
        saw.transform.position = Vector3.Lerp(leftLimit.position, rightLimit.position, currentSpot);
        if(currentSpot >= 1)
        {
            right = false;
        }
        if (currentSpot <= 0)
        {
            right = true;
        }
    }
}
