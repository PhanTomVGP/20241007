using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController charCon;

    private CameraController cam;
    private Vector3 moveAmount;
    public float jumpForce, gravityScale;
    private float yStore;
    public float rotateSpeed = 10f;
    public Animator anim;
    
    void Start()
    {
        cam = FindObjectOfType<CameraController>();
    }
    
    void Update()
    {
        yStore = moveAmount.y;
        
        //Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        

        //Camera moving vector length changes to 1.
        moveAmount = cam.transform.forward * Input.GetAxisRaw("Vertical") +
                     cam.transform.right * Input.GetAxisRaw("Horizontal");
        moveAmount.y = 0;
        moveAmount = moveAmount.normalized;

        if (moveAmount.magnitude > 0.1f)
        {
            if (moveAmount != Vector3.zero)
            {
                Quaternion newRot = Quaternion.LookRotation(moveAmount);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
            }
        }
        
        moveAmount.y = yStore;

        if (charCon.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveAmount.y = jumpForce;
            }
        }
        
        charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);

        float moveVel = new Vector3(moveAmount.x, 0f, moveAmount.z).magnitude * moveSpeed;
        anim.SetFloat("speed",moveVel);
        anim.SetBool("isGrounded",charCon.isGrounded);
        anim.SetFloat("yVel",moveAmount.y);
    }

    private void FixedUpdate()
    {
        //Gravity
        if (!charCon.isGrounded)
        {
            moveAmount.y += Physics.gravity.y * gravityScale * Time.fixedDeltaTime;
        }
        else
        {
            moveAmount.y = Physics.gravity.y * gravityScale * Time.fixedDeltaTime;
        }
    }
}
