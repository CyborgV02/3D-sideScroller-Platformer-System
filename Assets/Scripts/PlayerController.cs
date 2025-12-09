using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float speed=5.0f;
   public float jumpForce=8.0f;
   private Rigidbody rb ;
   public bool isGrounded=false;
   public bool canDash=true;
   public bool isDashing=false;
   public float dashSpeed=7.0f;
   public float dashDuration=0.3f;
   public float dashCooldown=0.1f;

   public float jumpCount=1;
   public float maxJumps=2;
   public Transform groundCheck;
   public float groundCheckradius=0.2f;
   public LayerMask groundLayer;



    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(  Physics.CheckSphere(groundCheck.position,groundCheckradius,groundLayer))
        {
            isGrounded=true;
        }

        else
        {
            isGrounded=false;
        }

       
      

        if (Input.GetKeyDown(KeyCode.Space)&&jumpCount<maxJumps)
        {
            jump();
            jumpCount++;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)&&canDash&&isGrounded)
        {
           StartCoroutine(dash());
        }

        if (isGrounded == true)
        {
            jumpCount=1;
        }
    }

    void jump()
    {
        rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
    }

    void FixedUpdate()
    {
         float horizontalInput = Input.GetAxis("Horizontal");
       Vector3 movement = new Vector3(horizontalInput,0,0);
       rb.MovePosition(rb.position+movement * speed*Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("Game Over !");
        }
    }

    private IEnumerator dash()
    {
        isDashing=true;
        canDash=false;
        rb.useGravity=false;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(horizontalInput,0,0);
        rb.AddForce(movement * dashSpeed, ForceMode.VelocityChange);
        yield return new WaitForSeconds(dashDuration);
        rb.linearVelocity=Vector3.zero;
        isDashing=false;
        rb.useGravity=true;
        yield return new WaitForSeconds(dashCooldown);
        canDash=true;
    }

 

}
