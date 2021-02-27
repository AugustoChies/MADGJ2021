using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, jumpPower;
    protected Rigidbody2D body;
    protected int movement;
    protected bool jump;
    // Start is called before the first frame update
    void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = 0;
        if(Input.GetKey(KeyCode.A))
        {
            movement--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement++;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }
    }

    public Vector2 rayOrigin1,rayOrigin2,rayOrigin3;
    bool GroundCheck()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(body.position + rayOrigin1, Vector2.down, 0.1f);
        RaycastHit2D hit2 = Physics2D.Raycast(body.position + rayOrigin2, Vector2.down, 0.1f);
        RaycastHit2D hit3 = Physics2D.Raycast(body.position + rayOrigin3, Vector2.down, 0.1f);
        return (hit1.collider != null && hit1.collider.CompareTag("Ground"))
            || (hit2.collider != null && hit2.collider.CompareTag("Ground"))
            || (hit3.collider != null && hit3.collider.CompareTag("Ground"));       
    }

    void FixedUpdate()
    {        
        body.velocity = new Vector2(movement * speed * Time.deltaTime,body.velocity.y);
        if(jump)
        {
            if (GroundCheck())
            {
                body.velocity = new Vector2(body.velocity.x, 0);
                body.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            jump = false;
        }
    }
}
