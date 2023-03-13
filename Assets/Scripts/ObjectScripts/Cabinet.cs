using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : ObjectDim //looking at the drawers
{
    //on, under
    private readonly float height = 2.25f;

    //infront, behind
    private readonly float width = 1.25f;

    //left,right
    private readonly float length = 1.45f;

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