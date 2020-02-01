using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("Movement")]
    public int moveSpeed; 
    public bool isGrounded;
    Vector2 moveMe;
    public bool isFacingRight = true;

    
   
    public LayerMask platform;

   [Header ("Jump & Gravity")] 
    Vector2 grav;
    public float airTime;
    public float jumpHeight;
    public float rayDist;
    public bool isJumping;
    public float airAmount;
    public float fallSpeed;
    public float jumpMultiplier;
    public float fallSpdMultiplier;
    public float finalFallSpd;
    public float airSubtract;
    public float reverseFallSpd;
    public bool isFlipping = false;

   public PolarityShield pol;


    void Start()
    {
        Physics2D.queriesStartInColliders = false;
   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveControl();
        NormalGravity();
       
        
    }

    void MoveControl ()
    {
        //Horizontal Movement Control
        moveMe = transform.position;
        float xMove = Input.GetAxis("Horizontal");
        moveMe += new Vector2(moveSpeed * xMove * Time.deltaTime, 0);

        //Flips Player Sprite to direction of movement
        if (xMove < 0 && !isFacingRight)
        {
            Flip();
        }
        if (xMove > 0 && isFacingRight)
        {
            Flip();
        }

        //Jump Control
        RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up, rayDist);
        if (hit.collider == null)
        {
            isGrounded = false;
            
        }
        if (hit.collider != null)
        {
            isGrounded = true;
            airTime = airAmount;
        }

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            moveMe += new Vector2(0, jumpHeight * Time.deltaTime);
            
            isJumping = true;
        }

        
        

        if (Input.GetButton ("Jump") && airTime > 0 && isJumping == true)
        {
            moveMe += new Vector2(0, jumpHeight * jumpMultiplier * Time.deltaTime);
            
        }

        if (isGrounded == false || isJumping == true)
        {
            airTime -= airSubtract * Time.deltaTime;
        }
        if (Input.GetButtonUp ("Jump") && isJumping == true)
        {
            isJumping = false;
        }

        
        transform.position = moveMe;
    }

    void NormalGravity ()
    {
        grav = transform.position;
        if (isFlipping == false)
        {
            if (isGrounded == false && isJumping == false || airTime <= 0)
            {
                grav -= new Vector2(0, fallSpeed * Time.deltaTime);
                fallSpeed += fallSpdMultiplier * Time.deltaTime;

            }

            if (fallSpeed > finalFallSpd)
            {
                fallSpeed = finalFallSpd;
            }

            if (isGrounded == true)
            {
                fallSpeed = 5;
            }
            transform.position = grav;
        }
    }

    void Flip ()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
    
    void ReverseGravity ()
    {
        grav = transform.position;      
        
        grav += new Vector2(0, reverseFallSpd * Time.deltaTime);
       
        transform.position = grav;
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Polarity" && pol.shieldSwitch == true)
        {
           ReverseGravity();
           isFlipping = true;
        } 
        
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Polarity")
        {
            isFlipping = false;
            
        }
    }

}
