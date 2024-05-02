using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public  GameObject player;
    public Vector3 offset;

    public float smoothSpeed = 0.125f;

    public bool start = false;
 public Vector3 rotationOffset ;
    private void Start()
    {
      
    }
    private void FixedUpdate()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Calculate the rotation offset
        // Modify the values as needed

        // Convert rotationOffset to Quaternion
        Quaternion rotationOffsetQuaternion = Quaternion.Euler(rotationOffset);

        // Apply the rotation offset
        transform.rotation = player.transform.rotation * rotationOffsetQuaternion;
    }

    // public void SetPlayer()
    // {
    //     player = GameObject.FindGameObjectWithTag("Player");
    //     player = player.transform.GetChild(0).gameObject;
    //     start = true;
    // }
}
