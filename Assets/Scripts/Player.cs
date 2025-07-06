using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    //Variables Autres
    [SerializeField] private InputActionReference MoveAction;
    [SerializeField] private InputActionReference JumpAction;
    [SerializeField] private LayerMask groundLayer;

    //Variables Parametres
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float deadzone = 0.1f;
    [SerializeField] private float JumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 0.5f;
    private bool PlayerGrounded = true;
    private bool PlayerTrick = false;
    
    private Animator animator;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        MoveAction.action.canceled += OnStop;
    }

    private void OnEnable()
    {
        MoveAction.action.Enable();
        JumpAction.action.Enable();

        MoveAction.action.performed += OnMove;
        JumpAction.action.performed += OnJump;
    }

    private void OnDisable()
    {
        MoveAction.action.Disable();
        JumpAction.action.Disable();

        MoveAction.action.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (PlayerGrounded)
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

        Vector2 input = MoveAction.action.ReadValue<Vector2>();

        float forwardInput = input.y;
        float turnInput = input.x;

        if (PlayerGrounded)
        {
            // 1. Rotation avant l'accélération
            float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion rotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * rotation);

            // 2. Appliquer accélération vers l'avant
            Vector3 force = transform.forward * forwardInput * acceleration;
            rb.AddForce(force, ForceMode.Force);

            // 3. Clamp vitesse max (sur XZ uniquement)
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            if (flatVel.magnitude > maxSpeed)
            {
                flatVel = flatVel.normalized * maxSpeed;
                rb.linearVelocity = new Vector3(flatVel.x, rb.linearVelocity.y, flatVel.z);
            }
        }
    }

    private void Update()
    {
        Debug.Log($"Grounded : {IsGrounded()}");
    }

    public bool GetGrounded(){return PlayerGrounded;}
    public bool GetPlayer(){return PlayerGrounded;}
    public void SetPlayer(bool value){PlayerTrick = value;}
}
