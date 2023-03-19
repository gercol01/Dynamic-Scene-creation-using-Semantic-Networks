using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabinet : ObjectDim //looking at the drawers
{
    //on, under
    protected float height = 2.25f;

    //infront, behind
    protected float width = 1.25f;

    //left,right
    protected float length = 1.45f;

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