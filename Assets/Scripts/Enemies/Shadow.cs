using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Transform downDetector;
    public float speed;
    protected Rigidbody2D rb;
    public bool stopped;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stopped) return;

        rb.MovePosition(rb.position + Vector2.right * speed * Time.deltaTime);
        CheckRays();
    }

    void CheckRays()
    {       
        RaycastHit2D hitD = Physics2D.Raycast(downDetector.position, Vector2.down, 0.4f);
        
        if(hitD.collider != null && hitD.collider.CompareTag("Killer"))
        {
            Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Killer"))
        {
            Stop();
        }
    }

    void Stop()
    {
        stopped = true;
        Destroy(this.gameObject); // change later maybe
    }
}
