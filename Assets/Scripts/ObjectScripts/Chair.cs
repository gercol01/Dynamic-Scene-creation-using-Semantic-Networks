using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : ObjectDim //backrest is on the left
{
    //on, under
    protected float height = 1.05f;

    //infront, behind
    protected float width = 0.5f;

    //left, right
    protected float length = 0.5f;

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