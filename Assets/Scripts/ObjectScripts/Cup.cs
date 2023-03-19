using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : ObjectDim //the cup handle is on the left
{
    //on, under
    protected float height = 0.8f;

    //infront, behind
    protected float width = 0.4f;

    //left, right
    protected float length = 0.4f;

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