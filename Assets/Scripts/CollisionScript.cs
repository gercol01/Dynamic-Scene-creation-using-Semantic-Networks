using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //check if the other object is a collision box
        if (other.gameObject.tag == "Enemy") {

            Bounds thisBounds = GetComponent<Collider>().bounds;
            Bounds otherBounds = other.bounds;

            //name of the object the script is attached to
            string objectName = transform.parent.gameObject.name;

            //check if the object the script is attached to is a 'temp'
            bool isSubstringFound = objectName.Contains("temp");

            if (isSubstringFound)//the object is a temp
            {
                Debug.Log("temp");
                objectName = objectName.Replace("temp", "");
            }

            //remove "(Clone)" to compare
            objectName = objectName.Replace("(Clone)", "");

            string otherObjectName = other.transform.parent.gameObject.name;

            otherObjectName = otherObjectName.Replace("Copytemp(Clone)", "");

            //checking if it is the same object
            if (otherObjectName.Equals(objectName))
            {
                //do nothing
                Debug.Log("They are the same");
            }
            else
            {
                //check if the object is collides with is the same 
                if (thisBounds.Intersects(otherBounds))
                {
                    Debug.Log("Bounds intersecting");

                    //destroy the OTHER game object, that means temp or Copytemp objects
                    DestroyRecursive(other.gameObject);
                }
            }
        }
    }

    private void DestroyRecursive(GameObject obj)
    {
        // Destroy the object
        Destroy(obj);

        // Check if the object has a parent
        if (obj.transform.parent != null)
        {
            // Recursively destroy the parent object
            DestroyRecursive(obj.transform.parent.gameObject);
        }
    }
}