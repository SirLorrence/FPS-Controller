using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float Runspeed;
    [SerializeField] float jumpForce;

    private float moveSpeed;
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
        // "normalized" makes sure that when moving diagonally you will have the same speed
    }
    //fixedUpdate() -- for physics steps
    private void FixedUpdate()
    {
        Move(Movement);
    }

    private void Move(Vector3 input) // gets the input of "Movement" with the normalized calculations
    {
        rb.MovePosition(transform.position + (input * moveSpeed * Time.deltaTime));
        Sprint();
        GroundCheck();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }
    }
    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = Runspeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }
    private void Jump()
    {
        rb.AddForce(Vector3.up* jumpForce, ForceMode.Impulse);
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
