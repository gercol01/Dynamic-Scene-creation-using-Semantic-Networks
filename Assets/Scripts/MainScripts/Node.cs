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

    protected float[] fakeCoordinates = new float[3]; //Array which stores the coordinates of the bottom face of an object

    protected Boolean visited = false; //if the node has been visited or not
    protected String Preposition = "null"; //preposition of the node with its parent
    protected int Level; //number to show how many parents the node has

    //coordinates of the gameObject
    protected Nullable<float> x = null;
    protected Nullable<float> y = null;
    protected Nullable<float> z = null;

    protected float rotationX = 0; //initialised to 0
    protected float rotationY = 0; //initialised to 0
    protected float rotationZ = 0; //initialised to 0

    protected NodeObject Obj = new NodeObject(); //object 

    private ObjectList objectListScript;

    protected string spacing = "null";

    public Node(String value)//constructor for the node object
    {
        Value = value;
    }

    public void setValue(String value)
    {
        Value = value;
    }

    public override String ToString()
    {
        return Value;
    }

    public void addChild(Node child, String preposition)
    {
        Children.Add(child);
        Preposition = preposition;
    }

    //for accuracy
    public String AccuToStringWithLocationAndPreposition()
    {
        return ToString() + "*" + getCoordinates() + "*" + getPrepositionForString() + "*" + getRotations() + "*" + getSpacing(); //cube_9*0v1v0*on*0v90v90*near
    }

    //for aesthetics
    public String AestToStringWithLocationAndPreposition()
    {
        return ToString() + "*" + roundCoordinates() + "*" + getPrepositionForString() + "*" + getRotations() + "*" + getSpacing(); //cube_9*0v1v0*on*0v90v90*near
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

    public Node CopyDeep()
    {
        //creating a new node
        Node newNode = new Node(this.Value);

        //passing on all the attributes
        newNode.setObjectType(this.objectType);
        newNode.setSpacing(this.spacing);
        newNode.setPreposition(this.Preposition);
        newNode.setCoordinates(this.coordinates);
        newNode.setFakeCoordinates(this.fakeCoordinates);
        newNode.setRotationX(this.rotationX);
        newNode.setRotationY(this.rotationY);
        newNode.setRotationZ(this.rotationZ);

        //returning the node
        return newNode;
    }

    public void setCoordinates(float[] coordinates)
    {
        this.coordinates = coordinates;
        x = coordinates[0];
        y = coordinates[1];
        z = coordinates[2];
    }

    public float[] getFakeCoordinates() {
        return fakeCoordinates;
    }

    //set function for fake coordinates
    public void setFakeCoordinates(float[] FakeCoordinates)
    {
        this.fakeCoordinates = FakeCoordinates;
    }

    public float[] getCoordinatesFloat() {
        return this.coordinates;
    }

    public string getCoordinates()
    {
        return x + "v" + y + "v" + z;
    }

    //method used to round the coordinates
    public string roundCoordinates()
    {
        return (float)Math.Round((decimal)fakeCoordinates[0], 3) + "v" + (float)Math.Round((decimal)fakeCoordinates[1], 3) + "v" + (float)Math.Round((decimal)fakeCoordinates[2], 3);
    }


    public string getCoordinatesUI()
    {
        return x + "(x)," + y + "(y)," + z + "(z),";
    }

    public void setObjectType(String ObjectType)
    {
        this.objectType = ObjectType;

        //calling the method to set the enemy and true gameobjects for the node
        setObject(ObjectType);
    }

    public String getObjectType()
    {
        return objectType;
    }

    public void setObject(String obj)
    {
        GameObject listObject = GameObject.Find("ObjectListObject");
        objectListScript = listObject.GetComponent<ObjectList>();

        if (string.Equals(obj, "table", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.table);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyTable);
        }
        if (string.Equals(obj, "fridge", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.fridge);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyFridge);
        }
        else if (string.Equals(obj, "sofa", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.sofa);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemySofa);

        }
        else if (string.Equals(obj, "armchair", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.armchair);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyArmchair);
        }
        else if (string.Equals(obj, "bed", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.bed);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyBed);
        }
        else if (string.Equals(obj, "carpet", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.carpet);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyCarpet);
        }
        else if (string.Equals(obj, "chair", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.chair);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyChair);
        }
        else if (string.Equals(obj, "cup", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.cup);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyCup);
        }
        else if (string.Equals(obj, "lamp", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.lamp);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyLamp);
        }
        else if (string.Equals(obj, "nightstand", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.nightstand);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyNightstand);
        }
        else if (string.Equals(obj, "oven", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.oven);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyOven);
        }
        else if (string.Equals(obj, "vase", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.vase);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyVase);
        }
        else if (string.Equals(obj, "wallvertical", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.wallVertical);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyWallVertical);
        }
        else if (string.Equals(obj, "wallhorizontal", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.wallHorizontal);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyWallHorizontal);
        }
        else if (string.Equals(obj, "walls", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyWalls);
        }
        else if (string.Equals(obj, "wall1", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.wall1);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyWall1);
        }
        else if (string.Equals(obj, "wall2", StringComparison.CurrentCultureIgnoreCase))
        {
            //setting the true object
            Obj.setTrueObject(objectListScript.wall2);

            //setting the enemy object
            Obj.setEnemyObject(objectListScript.enemyWall2);
        }
    }

    public GameObject getObjectTrue()
    {
        return Obj.getTrueObject();
    }

    public GameObject getObjectEnemy()
    {
        return Obj.getEnemyObject();
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

    public override bool Equals(object obj)
    {
        return Value.Equals((obj as Node).Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public string getRotations() 
    {
        return rotationX + "v" + rotationY + "v" + rotationZ;
    }

    public float getRotationX()
    {
        return rotationX;
    }

    public void setRotationX(float Rotation)
    {
        rotationX = Rotation;
    }

    public float getRotationY() {
        return rotationY;
    }

    public void setRotationY(float Rotation) {
        rotationY = Rotation;
    }

    public float getRotationZ()
    {
        return rotationZ;
    }

    public void setRotationZ(float Rotation)
    {
        rotationZ = Rotation;
    }

    public void setSpacing(string Spacing) {
        this.spacing = Spacing;
    }

    public String getSpacing() {
        return this.spacing;
    }
}