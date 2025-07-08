using UnityEngine;
using UnityEngine.InputSystem;

public class Reset : MonoBehaviour
{
    //Variables Autres
    [SerializeField] private InputActionReference ResetAction;
    [SerializeField] private Score script;

    private Rigidbody rb;
    private bool UpsideDown = false;
    private float boolActivatedTime = -1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        ResetAction.action.Enable();
        ResetAction.action.performed += OnReset;
    }

    private void OnDisable()
    {
        ResetAction.action.Disable();
        ResetAction.action.performed -= OnReset;
    }

    private void OnReset(InputAction.CallbackContext context)
    {
        ResetPlayer();
    }

    private void Update()
    {
        if (UpsideDown && boolActivatedTime < 0f)
        {
            boolActivatedTime = Time.time;
        }
        if (UpsideDown && Time.time - boolActivatedTime >= 1.5f)
        {
            ResetPlayer();
        }
        if (!UpsideDown)
        {
            boolActivatedTime = -1f;
        }
    }

    private void LateUpdate()
    {
        if (transform.up.y < 0)
        {
            UpsideDown = true;
        }
        else
        {
            UpsideDown = false;
        }
    }

    private void ResetPlayer()
    {
        UpsideDown = !UpsideDown;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        Vector3 position = transform.position;
        position.y += .5f;
        transform.position = position;

        script.RemoveScore(50);
    }
}
