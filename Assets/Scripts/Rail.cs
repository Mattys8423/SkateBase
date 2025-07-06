using UnityEngine;
using System.Collections;

public class Rail : MonoBehaviour
{
    [SerializeField] private Transform railStartPoint;
    [SerializeField] private Transform railDirectionRef;
    [SerializeField] private float launchForce = 5f;
    [SerializeField] private float heightOffset = 0.5f;
    [SerializeField] private float Delay = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<Player>().GetPlayer() == false)
        {
            other.GetComponent<Player>().SetPlayer(true);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;

                Vector3 adjustedPosition = railStartPoint.position + Vector3.up * heightOffset;
                other.transform.position = adjustedPosition;
                other.transform.rotation = railDirectionRef.rotation;

                Vector3 launchDirection = railDirectionRef.forward;
                rb.linearVelocity = launchDirection * launchForce;

                StartCoroutine(ModifyAfterDelay(rb, other));
            }
        }
    }

    private IEnumerator ModifyAfterDelay(Rigidbody rb, Collider player)
    {
        yield return new WaitForSeconds(Delay);
        rb.useGravity = true;
        player.GetComponent<Player>().SetPlayer(false);
    }
}
