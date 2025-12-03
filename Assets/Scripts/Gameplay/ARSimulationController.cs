using UnityEngine;

public class ARSimulationController : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float lookSpeed = 2f;

    float rotationX = 0f;
    float rotationY = 0f;

    void Update()
    {
        // Mouse look
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY, -80f, 80f);

        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);

        // WASD movement (simulating walking with phone)
        float v = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float h = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        transform.position += transform.forward * v;
        transform.position += transform.right * h;
    }
}