using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Bounds intersecting");
    }
}