using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float speed=5.0f;
   private Rigidbody rb ;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
       Vector3 movement = new Vector3(0,0,-horizontalInput);
       rb.MovePosition(transform.position +movement * speed*Time.deltaTime);
    }
}
