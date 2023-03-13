using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : ObjectDim //looking at the front legs
{
    //on, under
    private readonly float height = 1.05f;

    //infront, behind
    private readonly float width = 1f;

    //left, right
    private readonly float length = 1f;

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