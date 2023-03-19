using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightstand : ObjectDim //looking at the fridge doors
{
    //on, under
    protected float height = 1.122f;

    //infront, behind
    protected float width = 0.73f;

    //left, right
    protected float length = 0.73f;
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