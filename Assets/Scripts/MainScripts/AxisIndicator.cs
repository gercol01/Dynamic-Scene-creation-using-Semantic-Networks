using UnityEngine;

public class AxisIndicator : MonoBehaviour
{
    [SerializeField] private float scale = 1.0f;

    private Camera mainCamera;
    private Transform cachedTransform;

    private void Start()
    {
        cachedTransform = transform;
    }

    private void OnGUI()
    {
        mainCamera = Camera.main;

        // Convert the world position of the gameobject to screen space.
        Vector3 position = mainCamera.WorldToScreenPoint(cachedTransform.position);
        position.y = Screen.height - position.y;

        // Convert the world positions of the X, Y, and Z axes to screen space.
        Vector3 xPosition = mainCamera.WorldToScreenPoint(cachedTransform.position + cachedTransform.right * scale);
        xPosition.y = Screen.height - xPosition.y;
        Vector3 yPosition = mainCamera.WorldToScreenPoint(cachedTransform.position + cachedTransform.up * scale);
        yPosition.y = Screen.height - yPosition.y;
        Vector3 zPosition = mainCamera.WorldToScreenPoint(cachedTransform.position + cachedTransform.forward * scale);
        zPosition.y = Screen.height - zPosition.y;

        // Compute the directions of the X, Y, and Z axes in screen space.
        Vector3 xDirection = (xPosition - position).normalized;
        Vector3 yDirection = (yPosition - position).normalized;
        Vector3 zDirection = (zPosition - position).normalized;

        // Compute the positions of the X, Y, and Z axis labels based on their directions.
        float labelSize = 20;
        Vector2 xLabelPosition = new Vector2(xPosition.x, xPosition.y) + new Vector2(-labelSize, -labelSize) + new Vector2(xDirection.x, xDirection.y) * labelSize;
        Vector2 yLabelPosition = new Vector2(yPosition.x, yPosition.y) + new Vector2(-labelSize, -labelSize) + new Vector2(yDirection.x, yDirection.y) * labelSize;
        Vector2 zLabelPosition = new Vector2(zPosition.x, zPosition.y) + new Vector2(-labelSize, -labelSize) + new Vector2(zDirection.x, zDirection.y) * labelSize;

        // Draw the X, Y, and Z axis labels on the screen.
        GUI.color = Color.blue;
        GUI.Label(new Rect(xLabelPosition.x, xLabelPosition.y, labelSize * 2, labelSize * 2), "Z");
        GUI.color = Color.red;
        GUI.Label(new Rect(yLabelPosition.x, yLabelPosition.y, labelSize * 2, labelSize * 2), "X");
        GUI.color = Color.green;
        GUI.Label(new Rect(zLabelPosition.x, zLabelPosition.y, labelSize * 2, labelSize * 2), "Y");
    }
}
