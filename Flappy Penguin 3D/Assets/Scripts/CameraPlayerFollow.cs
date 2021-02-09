using UnityEngine;


public class CameraPlayerFollow : MonoBehaviour
{
    public Transform player;

    public float smoothSpeed = 10f;
    public Vector3 offset;

    void FixedUpdate()
    {
        var presetPosition = player.position;
        presetPosition.y = 0;
        transform.position = Vector3.Lerp(transform.position, presetPosition + offset, smoothSpeed * Time.deltaTime);
    }
}
