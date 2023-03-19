using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //to find the 'Test' instance
        Test test = FindObjectOfType<Test>();

        //get the list of objects that are exempt from collision detection, ex: table_1, table_2, chair_3 etc.
        string[] listOfObjects = test.GetListAsArray();

        //a flag to show if the object is exempt or not
        Boolean flagExempt = false;

        //check if the other object is a collision box
        if (other.gameObject.tag == "Enemy") {

            //get the bounds of the current object
            Bounds thisBounds = GetComponent<Collider>().bounds;

            //get the bounds of the other object
            Bounds otherBounds = other.bounds;

            //normalise the current object-----------------------

            //get the name of the PARENT object the script is attached to
            string objectName = transform.parent.gameObject.name;

            //check if the object the script is attached to is a 'temp', ex: table_1temp(Clone)
            bool isSubstringFound = objectName.Contains("temp");

            if (isSubstringFound)//the object is a temp, ex: table_1temp(Clone)
            {
                Debug.Log("temp");
                objectName = objectName.Replace("temp", ""); //table_1temp(Clone) -> table_1(Clone)
            }

            //remove "(Clone)" to compare, table_1(Clone) -> table_1
            objectName = objectName.Replace("(Clone)", "");

            //check if the object that the script is attached to is a member of the exempt list
            foreach (string NameOfExemptObject in listOfObjects)
            {
                if (NameOfExemptObject.Equals(objectName)){
                    //the currentObject should ignore collisions
                    flagExempt = true;
                }
            }

            if (flagExempt)
            {
                //do nothing
                Debug.Log("Exempt from collisions");

                //reset the flag
                flagExempt = false;
            }
            else {
                //check if the object it collides with is the same 
                if (thisBounds.Intersects(otherBounds))
                {
                    Debug.Log("Bounds intersecting");

                    //destroy the OTHER game object, that means temp or Copytemp objects
                    DestroyRecursive(other.gameObject);
                }
            }
        }
    }

    //method use to select the parent object of the object to be deleted
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