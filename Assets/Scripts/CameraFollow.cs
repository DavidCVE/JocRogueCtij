using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Public ca sa apara in Inspector
    public float smoothSpeed = 5f;

    void FixedUpdate()
    {
        if (target != null)
        {
            // Pozitia dorita (X si Y player, Z ramane -10)
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, -10f);

            // Miscarea lina
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}