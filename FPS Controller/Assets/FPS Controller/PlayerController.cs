using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float Runspeed;
    [SerializeField] float jumpForce;

    private bool isGrounded;
    private Rigidbody rb;
    private Vector3 Movement;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // update() -- for non-physics steps
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Movement = (x * transform.right + y * transform.forward).normalized;
        //normalized makes sure that when moving diagonally you will have the same speed
    }
    //fixedUpdate() -- for physics steps
    private void FixedUpdate()
    {
        Move();
        GroundCheck();
        Sprint();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }
    }

    private void Move()
    {
        Vector3 yFix = new Vector3(0, rb.velocity.y, 0); //this fixes the velocity of y to prevent if from falling slowly
        rb.velocity = Movement * moveSpeed * Time.deltaTime;
        rb.velocity += yFix;
    }
    private void Jump()
    {
        rb.velocity += new Vector3(0, jumpForce * Time.deltaTime, 0);
    }
    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 fix = new Vector3(0, rb.velocity.y, 0);
            rb.velocity = Movement * Runspeed * Time.deltaTime;
            rb.velocity += fix;
        }
        else
        {
            Move();
        }
    }

    private void GroundCheck()
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
