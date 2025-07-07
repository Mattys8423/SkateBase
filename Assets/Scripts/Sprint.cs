using UnityEngine;
using UnityEngine.InputSystem;

public class Sprint : MonoBehaviour
{
    [SerializeField] private InputActionReference SprintAction;
    [SerializeField] private float sprintForce = 5f;

    private Rigidbody rb;
    private bool canSprint = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        SprintAction.action.Enable();
        SprintAction.action.performed += OnSprint;
    }

    private void OnDisable()
    {
        SprintAction.action.Disable();
        SprintAction.action.performed -= OnSprint;
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (canSprint) 
        {
            Vector3 direction = transform.forward.normalized;
            rb.AddForce(direction * sprintForce, ForceMode.Impulse);
            StartCoroutine(SprintCooldown());
        }
    }

    private System.Collections.IEnumerator SprintCooldown()
    {
        canSprint = false;
        yield return new WaitForSeconds(1);
        canSprint = true;
    }
}
