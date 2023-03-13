using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelves : ObjectDim //looking at the shelves
{
    //on, under
    private readonly float height = 2.11f;

    //infront, behind
    private readonly float width = 1f;

    //left,right
    private readonly float length = 2f;

    public float getHeight()
    {
        return height;
    }

    public float getWidth()
    {
        return width;
    }

    public float getLength()
    {
        return length;
    }

}
