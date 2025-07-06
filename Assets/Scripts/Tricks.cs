using UnityEngine;
using UnityEngine.InputSystem;

public class Tricks : MonoBehaviour
{
    //Variables Autres
    [SerializeField] private InputActionReference TrickAction;
    [SerializeField] private Player script;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        TrickAction.action.Enable();
        TrickAction.action.performed += OnTrick;
    }

    private void OnDisable()
    {
        TrickAction.action.Disable();
        TrickAction.action.performed -= OnTrick;
    }

    private void OnTrick(InputAction.CallbackContext context)
    {
        if (script.GetGrounded())
        {
            //
        }
    }
}
