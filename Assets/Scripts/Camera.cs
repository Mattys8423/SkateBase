using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform target;        // Le joueur
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10); // Position relative
    [SerializeField] private float smoothSpeed = 5f;  // Vitesse de lissage

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
    }
}
