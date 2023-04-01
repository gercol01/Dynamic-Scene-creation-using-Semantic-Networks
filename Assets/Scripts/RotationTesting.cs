using UnityEngine;

public class RotationTesting : MonoBehaviour
{
    //reference to the original cube that we want to create our new cube in front of
    public GameObject cube1;

    //reference to the actual instance of the second cube that we create in front of the original cube.
    public GameObject cube2;

    void Start()
    {
        //Parent cube1----------------

        // Get the renderer component of the gameObject
        Renderer rendParent = cube1.GetComponent<Renderer>();

        // Get the bounds of the mesh
        Bounds boundsParent = rendParent.bounds;

        // Get the width of the bounds (size along the x-axis)
        float widthParent = boundsParent.size.x;

        //Child cube2----------------

        // Get the renderer component of the gameObject
        Renderer rendChild = cube2.GetComponent<Renderer>();

        // Get the bounds of the mesh
        Bounds boundsChild = rendChild.bounds;

        // Get the width of the bounds (size along the x-axis)
        float widthChild = boundsChild.size.x;

        float rotation = cube1.transform.rotation.eulerAngles.y;

        // Create cube_2 prefab 2 units infront of the original cube
        Instantiate(cube2, cube1.transform.position + (((4/2) + (10/2)) * cube1.transform.forward), Quaternion.Euler(0f, rotation, 0f));
    }
}