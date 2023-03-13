using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingNewScript : MonoBehaviour
{

    private Mesh mesh;

    void Start()
    {
        //// Get the Mesh component from the object
        //mesh = GetComponent<MeshFilter>().mesh;

        //// Create a new mesh collider
        //MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();

        //// Assign the mesh to the mesh collider
        //meshCollider.sharedMesh = mesh;

        //// Enable the mesh collider
        //meshCollider.enabled = true;
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision detected!");
    }
}
