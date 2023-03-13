using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Node
{
    protected String Value; //value of the node, Example: Table, Mouse, Book, etc.

    protected String objectType;

    protected List<Node> Children = new List<Node>();
    //objects that the node is related to, Example if the node is a table and their is a laptop on the table, the laptop will be a child of the table

    protected float[] coordinates = new float[3]; //position array, instantiated with 3 null coordinates

    protected Boolean visited = false; //if the node has been visited or not
    protected String Preposition = "null"; //preposition of the node with its parent
    protected int Level; //number to show how many parents the node has

    protected Nullable<float> x = null;
    protected Nullable<float> y = null;
    protected Nullable<float> z = null;

    protected int rotation = 0; //initialised to 0

    protected ObjectDim Obj = new ObjectDim(); //object 

    public Node(String value)//constructor for the node object
    {
        Value = value;
    }

    public void setValue(String value)
    {
        Value = value;
    }

    public void addChild(Node child, String preposition)
    {
        Children.Add(child);
        Preposition = preposition;
    }

    public override String ToString()
    {
        return Value;
    }

    public String ToStringWithLocationAndPreposition()
    {
        return ToString() + "*" + getCoordinates() + "*" + getPrepositionForString() + "*" + getRotation(); //cube_9*0.1.0*on*90
    }

    public String ToStringPrep()
    {
        return Value + getPrepositionForString();
    }

    public List<Node> getChildren()
    {
        return Children;
    }

    public void setLevel(int level)
    {
        Level = level;
    }

    public int getLevel()
    {
        return Level;
    }

    public Node Copy()
    {
        return (Node)this.MemberwiseClone();
    }

    public void setCoordinates(float[] coordinates)
    {
        this.coordinates = coordinates;
        x = coordinates[0];
        y = coordinates[1];
        z = coordinates[2];
    }

    public string getCoordinates()
    {
        return x + "v" + y + "v" + z;
    }

    public void setObjectType(String ObjectType)
    {
        this.objectType = ObjectType;
    }

    public String getObjectType()
    {
        return objectType;
    }

    public float returnX()
    {
        return x ?? default(float);
    }

    public float returnY()
    {
        return y ?? default(float);
    }

    public float returnZ()
    {
        return z ?? default(float);
    }

    public Boolean getCoordinatesNotNull()
    {
        if (x == null)
        {
            return false;
        }
        if (y == null)
        {
            return false;
        }
        if (z == null)
        {
            return false;
        }

        //A node which has a relationship would return true
        return true;
    }

    public string getPreposition()
    {
        return Preposition;
    }

    public string getPrepositionForString()
    {
        return Preposition;
    }

    public void setPreposition(string preposition)
    {
        Preposition = preposition;
    }

    public void setObject(String obj)
    {
        if (string.Equals(obj, "table", StringComparison.CurrentCultureIgnoreCase)) {
            Obj = new Table();
        }
        else if (string.Equals(obj, "sofa", StringComparison.CurrentCultureIgnoreCase))
        {
            Obj = new Table();
        }
    }

    public ObjectDim getObject()
    {
        return Obj;
    }

    public override bool Equals(object obj)
    {
        return Value.Equals((obj as Node).Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public int getRotation() {
        return rotation;
    }

    public void setRotation(int Rotation) {
        rotation = Rotation;
    }

}