using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table: ObjectDim //looking at the shortest side
{
    //on, under
    protected float height = 1.52f;

    //infront, behind
    protected float width = 3.25f;

    //left,right
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