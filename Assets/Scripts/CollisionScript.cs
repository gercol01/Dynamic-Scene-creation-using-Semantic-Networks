using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bounds thisBounds = GetComponent<Collider>().bounds;
        Bounds otherBounds = other.bounds;

        if (thisBounds.Intersects(otherBounds) && other.gameObject.tag == "Enemy")
        {
            Debug.Log("Bounds intersecting");
            Destroy(other.gameObject);
        }
    }
}
