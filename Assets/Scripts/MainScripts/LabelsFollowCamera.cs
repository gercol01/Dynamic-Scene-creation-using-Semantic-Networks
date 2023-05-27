using UnityEngine;
using TMPro;

public class LabelsFollowCamera : MonoBehaviour
{
    //labels
    private TextMeshProUGUI textToDisplayFront;
    private TextMeshProUGUI textToDisplayBehind;
    private TextMeshProUGUI textToDisplayOn;
    private TextMeshProUGUI textToDisplayUnder;
    private TextMeshProUGUI textToDisplayLeft;
    private TextMeshProUGUI textToDisplayRight;

    private GameObject objectToFollow;
    private Camera cameraToFollow;

    private void Start()
    {
        //find the Canvas gameObjects and the children
        GameObject onGameObjectParent = gameObject.transform.Find("On").gameObject;
        GameObject onGameObjectChild  = onGameObjectParent.transform.GetChild(0).gameObject;

        GameObject underGameObjectParent = gameObject.transform.Find("Under").gameObject;
        GameObject underGameObjectChild = underGameObjectParent.transform.GetChild(0).gameObject;

        GameObject leftGameObjectParent = gameObject.transform.Find("Left").gameObject;
        GameObject leftGameObjectChild = leftGameObjectParent.transform.GetChild(0).gameObject;

        GameObject rightGameObjectParent = gameObject.transform.Find("Right").gameObject;
        GameObject rightGameObjectChild = rightGameObjectParent.transform.GetChild(0).gameObject;

        GameObject frontGameObjectParent = gameObject.transform.Find("Front").gameObject;
        GameObject frontGameObjectChild = frontGameObjectParent.transform.GetChild(0).gameObject;

        GameObject behindGameObjectParent = gameObject.transform.Find("Behind").gameObject;
        GameObject behindGameObjectChild = behindGameObjectParent.transform.GetChild(0).gameObject;

        textToDisplayFront = frontGameObjectChild.GetComponent<TextMeshProUGUI>();
        textToDisplayBehind = behindGameObjectChild.GetComponent<TextMeshProUGUI>();
        textToDisplayOn = onGameObjectChild.GetComponent<TextMeshProUGUI>();
        textToDisplayUnder = underGameObjectChild.GetComponent<TextMeshProUGUI>();
        textToDisplayLeft = leftGameObjectChild.GetComponent<TextMeshProUGUI>();
        textToDisplayRight = rightGameObjectChild.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //if the object is active
        if (gameObject.activeSelf)
        {
            //get the current main camera
            cameraToFollow = Camera.main;

            //get the gameobject
            objectToFollow = gameObject;

            // Get the position of the text and camera in world space
            Vector3 textPosFront = textToDisplayFront.transform.position;
            Vector3 textPosBehind = textToDisplayBehind.transform.position;
            Vector3 textPosLeft = textToDisplayLeft.transform.position;
            Vector3 textPosRight = textToDisplayRight.transform.position; 
            Vector3 textPosOn = textToDisplayOn.transform.position;
            Vector3 textPosUnder = textToDisplayUnder.transform.position;

            Vector3 cameraPos = cameraToFollow.transform.position;

            // Calculate the direction from the text to the camera
            Vector3 directionFront = cameraPos - textPosFront;
            Vector3 directionBehind = cameraPos - textPosBehind;
            Vector3 directionOn = cameraPos - textPosOn;
            Vector3 directionUnder = cameraPos - textPosUnder;
            Vector3 directionLeft = cameraPos - textPosLeft;
            Vector3 directionRight = cameraPos - textPosRight;

            directionFront.y = 0; // Ignore the y-component
            directionBehind.y = 0; // Ignore the y-component
            directionOn.y = 0; // Ignore the y-component
            directionUnder.y = 0; // Ignore the y-component
            directionLeft.y = 0; // Ignore the y-component
            directionRight.y = 0; // Ignore the y-component

            // Rotate the text to face the camera on the y-axis
            Quaternion lookRotationFront = Quaternion.LookRotation(directionFront);
            Quaternion lookRotationBehind = Quaternion.LookRotation(directionBehind);
            Quaternion lookRotationLeft = Quaternion.LookRotation(directionLeft);
            Quaternion lookRotationRight = Quaternion.LookRotation(directionRight);
            Quaternion lookRotationOn = Quaternion.LookRotation(directionOn);
            Quaternion lookRotationUnder = Quaternion.LookRotation(directionUnder);

            textToDisplayFront.transform.rotation = Quaternion.Euler(0f, lookRotationFront.eulerAngles.y + 180f, 0f);
            textToDisplayBehind.transform.rotation = Quaternion.Euler(0f, lookRotationFront.eulerAngles.y + 180f, 0f);
            textToDisplayLeft.transform.rotation = Quaternion.Euler(0f, lookRotationFront.eulerAngles.y + 180f, 0f);
            textToDisplayRight.transform.rotation = Quaternion.Euler(0f, lookRotationFront.eulerAngles.y + 180f, 0f);
            textToDisplayOn.transform.rotation = Quaternion.Euler(0f, lookRotationFront.eulerAngles.y + 180f, 0f);
            textToDisplayUnder.transform.rotation = Quaternion.Euler(0f, lookRotationFront.eulerAngles.y + 180f, 0f);
        }
    }
}