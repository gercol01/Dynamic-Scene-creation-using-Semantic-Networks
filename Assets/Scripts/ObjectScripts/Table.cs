using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table: ObjectDim //looking at the shortest side
{
    //used for on, under
    protected float height = 1.52f;

    //used for infront, behind
    protected float width = 2f;

    //used for left,right
    protected float length = 1.1f;

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