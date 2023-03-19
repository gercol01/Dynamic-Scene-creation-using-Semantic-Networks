using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vase : ObjectDim //the cup handle is on the left
{
    //on, under
    protected float height = 1.19f;

    //infront, behind
    protected float width = 0.61f;

    //left, right
    protected float length = 0.61f;

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