using UnityEngine;
using TMPro;

public class LabelFollowCamera : MonoBehaviour
{
    public TextMeshProUGUI textToDisplay;
    private GameObject objectToFollow;
    private Camera cameraToFollow;

    void Update()
    {
        //if the object is active
        if (gameObject.activeSelf) {
            //get the current main camera
            cameraToFollow = Camera.main;

            //get the gameobject
            objectToFollow = gameObject;

            // Get the position of the text and camera in world space
            Vector3 textPos = textToDisplay.transform.position;
            Vector3 cameraPos = cameraToFollow.transform.position;

            // Calculate the direction from the text to the camera
            Vector3 direction = cameraPos - textPos;
            direction.y = 0; // Ignore the y-component

            // Rotate the text to face the camera on the y-axis
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            textToDisplay.transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y + 180f, 0f);
        }
    }
}