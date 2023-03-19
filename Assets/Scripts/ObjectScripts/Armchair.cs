using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armchair : ObjectDim //looking at the backside of the armchair
{
    //on, under
    protected float height = 0.935f;

    //infront, behind
    protected float width = 0.68f;

    //left, right
    protected float length = 0.87f;
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