using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Tricks : MonoBehaviour
{
    //Variables Autres
    [SerializeField] private InputActionReference TrickAction;
    [SerializeField] private Player script;
    [SerializeField] private Score script2;
    [SerializeField] private SliderCooldown script3;

    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
        if (script.GetPlayer() == false)
        {
            if (script.GetGrounded())
            {
                animator.Play("KickFlip");
                script2.AddScore(30);
                StartCoroutine(ModifyAfterDelay());
            }
            else
            {
                animator.Play("JumpTrick");
                script2.AddScore(60);
                StartCoroutine(ModifyAfterDelay());
            }
        }
    }

    private IEnumerator ModifyAfterDelay()
    {
        script3.SetSliderToMax();
        script.SetPlayer(true);
        script.SetPlayerState(true);
        yield return new WaitForSeconds(.8f);
        script.SetPlayerState(false);
        yield return new WaitForSeconds(1.2f);
        script.SetPlayer(false);
    }
}
