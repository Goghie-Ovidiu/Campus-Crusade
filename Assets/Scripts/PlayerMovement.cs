using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D coll;//collider pentru player
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;//verificare pentru a nu sarii in aer

    private enum MovementState { idle, running, jumping, falling} //animatie

    [SerializeField] private AudioSource jumpSound;

    private float dirX;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;

    // Start is called before the first frame update
    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        sprite= GetComponent<SpriteRenderer>();
        anim= GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    } 

    // Update is called once per frame
    private void Update()
    {
        //miscare stanga dreapta
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity=new Vector2 (dirX * moveSpeed,rb.velocity.y);


        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }

        UpdateAnimationState();
       
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {//animatie run
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else//animatie idle
            state = MovementState.idle;

        if(rb.velocity.y> .1f)
            state=MovementState.jumping;
        else if(rb.velocity.y < -.1f)
            state=MovementState.falling;


        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f,jumpableGround);
    }
}
