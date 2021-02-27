using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float speed,stillTime,stillBottomTime, lowestPoint,originalY;
    public bool fall,rise, ready;
    protected Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {      
        rb = GetComponent<Rigidbody2D>();
        originalY = rb.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(fall)
        {
            rb.MovePosition(rb.position + new Vector2(0,-speed * Time.deltaTime));
            if(rb.position.y <= lowestPoint)
            {
                fall = false;
                StartCoroutine(WaitRise(stillBottomTime));
            }
        }
        else if (rise)
        {
            rb.MovePosition(rb.position + new Vector2(0, speed * Time.deltaTime));
            if (rb.position.y >= originalY)
            {
                rise = false;
                ready = true;
            }
        }
    }

    public void Activate(float lowest)
    {
        ready = false;
        lowestPoint = lowest;
        PlayNoise();
        StartCoroutine(WaitFall(stillTime));
    }

    IEnumerator WaitFall(float time)
    {
        yield return new WaitForSeconds(time);
        fall = true;
    }

    IEnumerator WaitRise(float time)
    {
        yield return new WaitForSeconds(time);
        rise = true;
    }

    public void PlayNoise()
    {
        this.GetComponent<AudioSource>().Play();
    }
}
