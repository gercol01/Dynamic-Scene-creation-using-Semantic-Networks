using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : ObjectDim //looking at the fridge doors
{
    //on, under
    protected float height = 0.31f;

    //infront, behind
    protected float width = 0.22f;

    //left, right
    protected float length = 0.22f;
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