using UnityEngine;
using UnityEngine.InputSystem;

public class Reset : MonoBehaviour
{
    //Variables Autres
    [SerializeField] private InputActionReference ResetAction;

    private Rigidbody rb;

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
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

        Vector3 position = transform.position;
        position.y += .5f;
        transform.position = position;
    }
}
