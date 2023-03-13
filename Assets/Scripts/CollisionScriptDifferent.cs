using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScriptDifferent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bounds thisBounds = GetComponent<Collider>().bounds;
        Bounds otherBounds = other.bounds;

        if (thisBounds.Intersects(otherBounds) && other.gameObject.tag == "Enemy")
        {
            Debug.Log("Kill Table");
            Destroy(gameObject);
        }
    }
}
