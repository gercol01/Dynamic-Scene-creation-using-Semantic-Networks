using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : ObjectDim //looking at the backside of the bed
{
    //on, under
    protected float height = 1f;

    //infront, behind
    protected float width = 2f;

    //left, right
    protected float length = 1.5f;
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