using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float smoothSpeed = 5f;

    void FixedUpdate()
    {
        if (target != null)
        {

            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, -10f);


            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}