using UnityEngine;

public class FaceIndicator : MonoBehaviour
{
    //variable to control the size of the label
    [SerializeField] private float labelSize = 20f;

    //variable to control 
    [SerializeField] private GUIStyle labelStyle;

    //Boolean flag to determine if the name of the gameobject is shown
    private bool facesflag = false;

    private Camera mainCamera;
    private Transform cachedTransform;

    private void Start()
    {
        cachedTransform = transform;
    }

    //Method is called everytime the object is clicked to enabled/disabled the name and coordinates
    private void OnMouseDown()
    {
        //Toggle the boolean flag for name and coordinates status
        facesflag = !facesflag;
    }

    private void OnGUI()
    {
        if (facesflag) {
            mainCamera = Camera.main;

            // Convert the world position of the gameobject to screen space.
            Vector3 position = mainCamera.WorldToScreenPoint(transform.parent.position);
            position.y = Screen.height - position.y;

            // Get the world space direction vectors of each face of the cube.
            Vector3 right = transform.parent.right * transform.localScale.x / 2f;
            Vector3 left = -right;
            Vector3 up = transform.parent.up * transform.localScale.y / 2f;
            Vector3 down = -up;
            Vector3 forward = transform.parent.forward * transform.localScale.z / 2f;
            Vector3 back = -forward;

            

            // Convert the world positions of each face to screen space.
            Vector3 rightPosition = mainCamera.WorldToScreenPoint(transform.parent.position + right);
            rightPosition.y = Screen.height - rightPosition.y;
            Vector3 leftPosition = mainCamera.WorldToScreenPoint(transform.parent.position + left);
            leftPosition.y = Screen.height - leftPosition.y;
            Vector3 upPosition = mainCamera.WorldToScreenPoint(transform.parent.position + up);
            upPosition.y = Screen.height - upPosition.y;
            Vector3 downPosition = mainCamera.WorldToScreenPoint(transform.parent.position + down);
            downPosition.y = Screen.height - downPosition.y;
            Vector3 forwardPosition = mainCamera.WorldToScreenPoint(transform.parent.position + forward);
            forwardPosition.y = Screen.height - forwardPosition.y;
            Vector3 backPosition = mainCamera.WorldToScreenPoint(transform.parent.position + back);
            backPosition.y = Screen.height - backPosition.y;

            // Compute the directions of the labels based on the direction vectors of each face.
            Vector3 rightDirection = (rightPosition - position).normalized;
            Vector3 leftDirection = (leftPosition - position).normalized;
            Vector3 upDirection = (upPosition - position).normalized;
            Vector3 downDirection = (downPosition - position).normalized;
            Vector3 forwardDirection = (forwardPosition - position).normalized;
            Vector3 backDirection = (backPosition - position).normalized;

            // Compute the positions of the labels based on their directions.
            Vector2 rightLabelPosition = new Vector2(rightPosition.x, rightPosition.y) + new Vector2(-labelSize, -labelSize) + new Vector2(rightDirection.x, rightDirection.y) * labelSize;
            Vector2 leftLabelPosition = new Vector2(leftPosition.x, leftPosition.y) + new Vector2(-labelSize, -labelSize) + new Vector2(leftDirection.x, leftDirection.y) * labelSize;
            Vector2 upLabelPosition = new Vector2(upPosition.x, upPosition.y) + new Vector2(-labelSize, -labelSize) + new Vector2(upDirection.x, upDirection.y) * labelSize;
            Vector2 downLabelPosition = new Vector2(downPosition.x, downPosition.y) + new Vector2(-labelSize, -labelSize) + new Vector2(downDirection.x, downDirection.y) * labelSize;
            Vector2 forwardLabelPosition = new Vector2(forwardPosition.x, forwardPosition.y) + new Vector2(-labelSize, -labelSize) + new Vector2(forwardDirection.x, forwardDirection.y) * labelSize;
            Vector2 backLabelPosition = new Vector2(backPosition.x, backPosition.y) + new Vector2(-labelSize, -labelSize) + new Vector2(backDirection.x, backDirection.y) * labelSize;

            // Draw the labels.
            GUI.Label(new Rect(rightLabelPosition.x, rightLabelPosition.y, labelSize, labelSize), "Right", labelStyle);
            GUI.Label(new Rect(leftLabelPosition.x, leftLabelPosition.y, labelSize, labelSize), "Left", labelStyle);
            GUI.Label(new Rect(upLabelPosition.x, upLabelPosition.y, labelSize, labelSize), "Up", labelStyle);
            GUI.Label(new Rect(downLabelPosition.x, downLabelPosition.y, labelSize, labelSize), "Down", labelStyle);
            GUI.Label(new Rect(forwardLabelPosition.x, forwardLabelPosition.y, labelSize, labelSize), "Forward", labelStyle);
            GUI.Label(new Rect(backLabelPosition.x, backLabelPosition.y, labelSize, labelSize), "Back", labelStyle);
        }
    }
}