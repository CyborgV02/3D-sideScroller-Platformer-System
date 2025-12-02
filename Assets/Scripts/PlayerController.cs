using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float speed=5.0f;
   public float jumpForce=5.0f;
   private Rigidbody rb ;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
      

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
    }

    void jump()
    {
        rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
    }

    void FixedUpdate()
    {
         float horizontalInput = Input.GetAxis("Horizontal");
       Vector3 movement = new Vector3(0,0,-horizontalInput);
       rb.MovePosition(rb.position+movement * speed*Time.fixedDeltaTime);
    }
}
