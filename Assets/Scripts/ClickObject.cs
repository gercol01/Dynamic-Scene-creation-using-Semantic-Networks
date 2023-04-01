using UnityEngine;
using TMPro;

public class ClickObject : MonoBehaviour
{
    // Declare a boolean flag
    private bool flag = false;

    //Camera variables
    public GameObject objectToFollow;
    public TextMeshProUGUI textToDisplay;
    public Camera cameraToFollow;

    // This method is called when the object is clicked
    private void OnMouseDown()
    {
        // Toggle the boolean flag
        flag = !flag;

        // Print the current value of the flag to the console
        Debug.Log("Flag value: " + flag);
    }

    void Update()
    {
        if (flag)
        {
            textToDisplay.gameObject.SetActive(true);

            //Object text--------

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
        else
        {
            textToDisplay.gameObject.SetActive(false);
        }
    }
}
