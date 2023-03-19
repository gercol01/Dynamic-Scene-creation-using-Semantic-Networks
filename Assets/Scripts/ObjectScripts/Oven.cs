using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : ObjectDim //looking at the fridge doors
{
    //on, under
    protected float height = 2.1f;

    //infront, behind
    protected float width = 0.85f;

    //left, right
    protected float length = 0.85f;
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