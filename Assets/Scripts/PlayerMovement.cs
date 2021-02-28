using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, jumpPower;
    private bool _isCrouching;
    protected Rigidbody2D body;
    protected int movement;
    protected bool jump;
    private bool _isFlipped = false;

    public int Movement => movement;
    public bool IsCrouching => _isCrouching;
    public GameObject particles;
    public Transform standigPos, crounchingPos;
    void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerAnimation.CurrentPlayerState != PlayerState.GettingUp)
        {
            if(GameController.instance._gameState == GameState.Gameplay)
            {
                UpdateMovement();
            }
        }      
    }

    public Vector2 rayOrigin1,rayOrigin2,rayOrigin3;

    public void UpdateMovement()
    {
        movement = 0;
        if (Input.GetKey(KeyCode.A) && !_isCrouching)
        {
            movement--;

            if(!_isFlipped)
            {
                transform.localScale = new Vector2(-1, 1);
                _isFlipped = true;
            }
        }
        if (Input.GetKey(KeyCode.D) && !_isCrouching)
        {
            movement++;

            if (_isFlipped)
            {
                transform.localScale = new Vector2(1, 1);
                _isFlipped = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.W) && !_isCrouching)
        {
            jump = true;
        }
        if (Input.GetKey(KeyCode.S) && CheckGround())
        {
            _isCrouching = true;
            particles.transform.position = crounchingPos.position;
        }
        if (Input.GetKeyUp(KeyCode.S) && CheckGround())
        {
            _isCrouching = false;
            particles.transform.position = standigPos.position;
        }
    }

    public bool CheckGround()
    {
        return GroundCheck.IsGrounded;

        //RaycastHit2D hit1 = Physics2D.Raycast(body.position + rayOrigin1, Vector2.down, 0.1f);
        //RaycastHit2D hit2 = Physics2D.Raycast(body.position + rayOrigin2, Vector2.down, 0.1f);
        //RaycastHit2D hit3 = Physics2D.Raycast(body.position + rayOrigin3, Vector2.down, 0.1f);
        //return (hit1.collider != null && hit1.collider.CompareTag("Ground"))
        //    || (hit2.collider != null && hit2.collider.CompareTag("Ground"))
        //    || (hit3.collider != null && hit3.collider.CompareTag("Ground"));      

        
    }

    void FixedUpdate()
    {
        if (GameController.instance._gameState == GameState.Gameplay)
        {
            body.velocity = new Vector2(movement * speed * Time.deltaTime, body.velocity.y);
            particles.SetActive(true);
        }
        else
        {
            body.velocity = new Vector2(0, body.velocity.y);
            particles.SetActive(false);
        }
        if(jump)
        {
            if (CheckGround())
            {
                body.velocity = new Vector2(body.velocity.x, 0);
                body.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            jump = false;
        }
    }
}
