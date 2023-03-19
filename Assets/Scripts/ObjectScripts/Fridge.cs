using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : ObjectDim //looking at the fridge doors
{
    //on, under
    protected float height = 4f;

    //infront, behind
    protected float width = 1f;

    //left, right
    protected float length = 1f;
    public override float getHeight()
    {
        return height;
    }

    public override float getWidth()
    {
        return width;
    }

    public override float getLength()
    {
        return length;
    }

}