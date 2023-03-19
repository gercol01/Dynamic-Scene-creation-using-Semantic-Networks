using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sofa : ObjectDim //looking at the armrest with the backcushions facing to the left
{
    //on, under
    protected float height = 0.83f;

    //infront, behind
    protected float width = 3f;

    //left,right
    protected float length = 1.25f;

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