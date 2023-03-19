using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : ObjectDim //looking at the fridge doors
{
    //on, under
    protected float height = 0.027f;

    //infront, behind
    protected float width = 3.25f;

    //left, right
    protected float length = 2.25f;
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