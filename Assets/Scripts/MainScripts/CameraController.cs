using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 60f;   // Speed of camera movement
    public float lookSpeed = 100f;  // Speed of camera rotation

    private bool isMoving = false;  // Flag to track camera movement

    private void Update()
    {
        // Check if the shift button is being pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isMoving = !isMoving;
        }

        // Move and rotate the camera based on user input
        if (isMoving && gameObject.activeInHierarchy)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float upDown = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(new Vector3(horizontal, upDown, vertical) * moveSpeed * Time.deltaTime);

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            transform.Rotate(new Vector3(-mouseY, mouseX, 0) * lookSpeed * Time.deltaTime, Space.Self);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
    }
}