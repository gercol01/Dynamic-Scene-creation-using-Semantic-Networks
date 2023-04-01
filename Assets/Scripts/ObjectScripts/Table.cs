using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table: ObjectDim //looking at the shortest side
{
    //used for on, under
    protected float height = 1.5f;

    //used for infront, behind
    protected float width = 3f;

    //used for left,right
    protected float length = 2f;

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