//using UnityEngine;
//using TMPro;

//public class TestingHoverCameraText : MonoBehaviour
//{
//    public Transform target;
//    public TextMeshProUGUI textMesh;
//    public Camera mainCamera;

//    void Update()
//    {
//        // Set the position of the text mesh to be above the target game object
//        textMesh.transform.position = target.position + Vector3.up * 2f;

//        // Rotate the text mesh to face the camera
//        textMesh.transform.rotation = Quaternion.LookRotation(textMesh.transform.position - mainCamera.transform.position);

//        // Make sure the text mesh is always facing up
//        textMesh.transform.Rotate(0, 180, 0);
//    }
//}