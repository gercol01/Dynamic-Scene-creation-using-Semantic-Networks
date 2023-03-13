using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : ObjectDim //the cup handle is on the left
{
    //on, under
    private readonly float height = 0.44f;

    //infront, behind
    private readonly float width = 0.9f;

    //left, right
    private readonly float length = 0.9f;

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