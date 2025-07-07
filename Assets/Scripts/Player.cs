using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    //Variables Autres
    [SerializeField] private InputActionReference MoveAction;
    [SerializeField] private InputActionReference JumpAction;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ObstacleLayer;

    //Variables Parametres
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 0.5f;
    private bool PlayerGrounded = true;
    private bool PlayerOnObstacle = false;
    private bool PlayerTrick = false;
    private bool PlayerOccupied = false;
    
    private Animator animator;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        MoveAction.action.Enable();
        JumpAction.action.Enable();

        MoveAction.action.performed += OnMove;
        MoveAction.action.canceled += OnStop;
        JumpAction.action.performed += OnJump;
    }

    private void OnDisable()
    {
        MoveAction.action.Disable();
        JumpAction.action.Disable();

        MoveAction.action.performed -= OnMove;
        MoveAction.action.canceled -= OnStop;
        JumpAction.action.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (PlayerGrounded && !PlayerOccupied || PlayerOnObstacle && !PlayerOccupied)
        {
            animator.Play("Jump");
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        animator.SetBool("IsMove", true);
    }

    private void OnStop(InputAction.CallbackContext context)
    {
        animator.SetBool("IsMove", false);
    }

    private bool IsGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f; // pour éviter de partir trop bas
        bool grounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer);

        // Debug visuel
        Debug.DrawRay(origin, Vector3.down * groundCheckDistance, grounded ? Color.green : Color.red);

        return grounded;
    }

    private bool IsOnObstacle()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f; // pour éviter de partir trop bas
        bool grounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, ObstacleLayer);

        // Debug visuel
        Debug.DrawRay(origin, Vector3.down * groundCheckDistance, grounded ? Color.green : Color.red);

        return grounded;
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            PlayerGrounded = true;
        }
        else
        {
            PlayerGrounded = false;
        }

        if (IsOnObstacle())
        {
            PlayerOnObstacle = true;
        }
        else
        {
            PlayerOnObstacle = false;
        }

        Vector2 input = MoveAction.action.ReadValue<Vector2>();

        float forwardInput = input.y;
        float turnInput = input.x;

        if (PlayerGrounded || PlayerOnObstacle)
        {
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion rotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * rotation);

            Vector3 force = transform.forward * forwardInput * acceleration;
            rb.AddForce(force, ForceMode.Force);

            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            if (flatVel.magnitude > maxSpeed)
            {
                flatVel = flatVel.normalized * maxSpeed;
                rb.linearVelocity = new Vector3(flatVel.x, rb.linearVelocity.y, flatVel.z);
            }
        }
    }

    /*------------------------*/

    public bool GetGrounded(){return PlayerGrounded;}
    public bool GetObstacle() { return PlayerOnObstacle; }
    public bool GetPlayer(){return PlayerTrick;}
    public bool GetPlayerState() { return PlayerOccupied; }

    public void SetPlayer(bool value){PlayerTrick = value;}
    public void SetPlayerState(bool value) { PlayerOccupied = value; }

}
