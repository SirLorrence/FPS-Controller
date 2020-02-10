using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float JumpSpeed;
    public bool isGrounded;

    public LayerMask groundCheck;

    Rigidbody rb;
    Vector3 Movement;
    CapsuleCollider capsuleCollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    // update() -- for non-physics steps
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Movement = (x * transform.right + z * transform.forward).normalized;
        //normalized makes sure that when moving diagonally you will have the same speed
    }
    //fixedUpdate() -- for physics steps
    private void FixedUpdate()
    {
        Move();
        GroundCheck();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }
        Debug.Log(isGrounded);

    }

    void Move()
    {
        Vector3 yFix = new Vector3(0, rb.velocity.y, 0); //this fixes the velocity of y to prevent if from falling slowly
        rb.velocity = Movement * walkSpeed * Time.deltaTime;
        rb.velocity += yFix;
    }
    void Jump()
    {
        rb.velocity += new Vector3(0, JumpSpeed * Time.deltaTime, 0);
    }

    void GroundCheck()
    {
        
        Vector3 RayLength = new Vector3(0, -2, 0); // the length and direction of the ray
        float Hitdistance = 1.5f; // the length of the ray which can detect/hit stuff

        if (Physics.Raycast(transform.position, RayLength, Hitdistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.DrawRay(transform.position, RayLength, Color.red);
    }
}
