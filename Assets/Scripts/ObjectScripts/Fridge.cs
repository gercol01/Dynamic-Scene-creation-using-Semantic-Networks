using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : ObjectDim //looking at the fridge doors
{
    //on, under
    private readonly float height = 4f;

    //infront, behind
    private readonly float width = 1.45f;

    //left, right
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