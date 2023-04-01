using System;
using System.Collections;
using UnityEngine;

//this script is attached to the temp object
public class CenteredLabel : MonoBehaviour
{
    //name----

    //Boolean flag to determine if the name of the gameobject is shown
    private bool nameAndCoordinatesflag = false;

    //Boolean flag to indicate if name and coordinates have been initialised or not
    private bool initialised = false;

    //Declare a GUIStyle object for the label
    public GUIStyle labelStyle2;

    //the name of the gameobject
    string nameObject;

    //the coordinates of the gameobject
    string coordinates;

    //combined name and coordinates of the gameobject
    string nameAndCoordinates;

    //Method is called everytime the object is clicked to enabled/disabled the name and coordinates
    private void OnMouseDown()
    {
        //Toggle the boolean flag for name and coordinates status
        nameAndCoordinatesflag = !nameAndCoordinatesflag;

        if (!initialised) {
            //the variables have now been initialised
            initialised = true;

            //get the name of the PARENT object the script is attached to
            nameObject = transform.parent.gameObject.name;

            //get the name of the gameobject without 'temp(Clone)'
            nameObject = nameObject.Replace("(Clone)", ""); //table_1temp(Clone) -> table_1(Clone)

            //to find the 'Test' instance
            Test test = FindObjectOfType<Test>();

            //get the coordinates of the object
            coordinates = test.getObjectCoordinates(nameObject);

            //combine the two strings
            nameAndCoordinates = String.Concat(nameObject, " ", coordinates);
        }
    }

    //OnGUI is called every frame
    void OnGUI()
    {
        if (nameAndCoordinatesflag)
        {
            // Calculate the position of the label in screen space
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
            float labelWidth = 100f;
            float labelHeight = 50f;

            Rect labelRect = new Rect(objectPosition.x - (labelWidth / 2f), Screen.height - objectPosition.y - (labelHeight / 2f), labelWidth, labelHeight);

            // Draw the label with the specified style and position
            labelStyle2.normal.textColor = Color.red;
            GUI.Label(labelRect, nameAndCoordinates, labelStyle2);

        }
    }
}