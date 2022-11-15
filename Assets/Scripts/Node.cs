using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Node
{
    protected String Value; //value of the node, Example: Table, Mouse, Book, etc.

    protected List<Node> Children = new List<Node>();
    //objects that the node is related to, Example if the node is a table and their is a laptop on the table, the laptop will be a child of the table

    protected Boolean visited = false; //if the node has been visited or not
    protected String Preposition = ""; //preposition of the node with its parent
    protected int Level; //number to show how many parents the node has

    public GameObject Obj; //object 

    public Node(String value)//constructor for the node object
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

    public String ToStringPrep()
    {
        return Value + getPreposition();
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

    public string getPreposition() 
    {
        return "-" + Preposition;
    }

    public void setPreposition(string preposition) {
        Preposition = preposition;
    }

    public void setObject(GameObject obj)
    {
        Obj = obj;
    }

    public GameObject getObject() {
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
}