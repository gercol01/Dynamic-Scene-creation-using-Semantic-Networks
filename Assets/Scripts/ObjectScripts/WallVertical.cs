using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallVertical : ObjectDim //looking at the shortest side
{
    //used for on, under
    protected float height = 4f;

    //used for infront, behind
    protected float width = 20f;

    //used for left,right
    protected float length = 0.25f;

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