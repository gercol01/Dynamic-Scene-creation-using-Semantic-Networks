using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Linq; //for the array
using UnityEngine.UI;

public class GraphDataStructure : MonoBehaviour
{
    //Placeholder field
    public Text placeholder;

    //Input field
    public InputField commandBox;

    //Display canvas
    public Text outputCommands;

    //list of nodes
    public List<Node> nodeList;

    //List of GameObjects currently in the scene
    public List<GameObject> objList;

    public GameObject cube; //cube object
    public GameObject sphere; //cube object

    //public Transform spawnPoint;
    public GameObject objToSpawn;
    //public Quaternion rotation;


    //creating the datastructure that will store the nodes
    IDictionary<Node, Node[]> Tree = new Dictionary<Node, Node[]>();

    //String format of the Semantic Tree
    string TreeString = "[ProgramUnity, Chair-on|Book-under|Laptop-right_of]";

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 temp2 = getSize(cube);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function used to add commands
    public void addCommand()
    {
        //runner object to use methods
        GraphDataStructure graphObject = new GraphDataStructure();

        //array used to split the sentence into words
        string[] words = { };

        //flag to identify the type of command
        int flag = -1;

        //check that the input is not empty
        if (commandBox.text != "")
        {
            //the sentence is split into words
            words = commandBox.text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //if the command is Add, flag to 0
            if (String.Equals(words[0], "Add", StringComparison.OrdinalIgnoreCase))
            {
                flag = 0;
            }

            //if the command is Create, flag to 1
            else if (String.Equals(words[0], "Create", StringComparison.OrdinalIgnoreCase))
            {
                flag = 1;
            }

            //if the command is none of the above, flag to 0
            else 
            {
                flag = -1;
            }

            //display the command in the output commands box
            outputCommands.text = outputCommands.text + commandBox.text + "\n";

            //only called for the first node
            if (words.Length == 2 && flag == 0)//if there are only two nodes, that means that it is the start node, ex: Add Tree
            {
                string parent = words[1];//getting the name of the object

                Node parentNode = new Node(parent);
                //parentNode.setPreposition("startNode");

                //nodeList.Add(parentNode);//adding the Node to the List

                graphObject.addChildren(Tree, parentNode, null); //adding the first node to the tree - it has no children
            }
            else if (words.Length == 4 && flag == 1)//if there are 4 nodes, that means that it is a relationship ex: Create Book under Tree
            {
                string parent = words[3];//the parent node

                Node parentNode = new Node(parent);


                string child = words[1];//the child node
                string preposition = words[2];//the preposition

                Node childNode = new Node(child);
                childNode.setPreposition(preposition);

                //nodeList.Add(childNode);//adding the Node to the List

                graphObject.addChildren(Tree, parentNode, childNode);
            }
            else//if the command is not a valid input
            {
                placeholder.text = "Incorrect input!";
                placeholder.color = Color.red;
            }

            //reset the box after adding the command
            commandBox.text = "";
            placeholder.text = "Enter commands...";
            placeholder.color = Color.black;
        }
        else//if the input is empty
        {
            placeholder.text = "Incorrect input!";
            placeholder.color = Color.red;
        }



        ////runner object to use methods
        //GraphDataStructure graphObject = new GraphDataStructure();

        ////Testing-----------------------------------------
        ////First Node is the 'table' - start point
        //Node table = new Node("Table");
        //table.setPreposition("startNode");
        //table.setObject(cube);

        //Node chair = new Node("Chair");
        //chair.setPreposition("on");
        //table.setObject(sphere);

        //Node book = new Node("Book");
        //book.setPreposition("under");

        //Node laptop = new Node("Laptop");
        //laptop.setPreposition("right_of");

        //Node box = new Node("Box");
        //box.setPreposition("left_of");

        //Node cat = new Node("Cat");
        //cat.setPreposition("left_of");

        //Node mouse = new Node("Mouse");
        //mouse.setPreposition("behind");

        //Node stapler = new Node("Stapler");
        //stapler.setPreposition("infront");

        //Node monitor = new Node("Monitor");
        //monitor.setPreposition("on");

        //Node man = new Node("Man");
        //man.setPreposition("under");

        //Node cheese = new Node("Cheese");
        //cheese.setPreposition("on");



        //graphObject.addChildren(Tree, table, chair);
        //graphObject.addChildren(Tree, table, book);
        //graphObject.addChildren(Tree, table, laptop);
        //graphObject.addChildren(Tree, chair, box);
        //graphObject.addChildren(Tree, book, cat);
        //graphObject.addChildren(Tree, book, mouse);
        //graphObject.addChildren(Tree, book, stapler);
        //graphObject.addChildren(Tree, laptop, monitor);
        //graphObject.addChildren(Tree, box, man);
        //graphObject.addChildren(Tree, mouse, cheese);
    }

    //Function used to add commands
    public void ResetScene()
    {
        //Destroy(table.getObject());

        //Node chair = new Node("Chair");
        //chair.setPreposition("on");
        //table.setObject(sphere);
    }

    public void Test() {
        //table.setPreposition("startNode");
        //table.setObject(cube);

        ////Spawn the object
        //Instantiate(table.getObject());
    }

    //Function used to create the Scene
    public void CreateScene() 
    {
        //runner object to use methods
        GraphDataStructure graphObject = new GraphDataStructure();

        //Testing-----------------------------------------
        //First Node is the 'table' - start point
        Node table = new Node("Gerard");
        table.setPreposition("startNode");

        Node chair = new Node("Chair");
        chair.setPreposition("on");

        Node book = new Node("Book");
        book.setPreposition("under");

        Node laptop = new Node("Laptop");
        laptop.setPreposition("right_of");

        Node box = new Node("Box");
        box.setPreposition("left_of");

        Node cat = new Node("Cat");
        cat.setPreposition("left_of");

        Node mouse = new Node("Mouse");
        mouse.setPreposition("behind");

        Node stapler = new Node("Stapler");
        stapler.setPreposition("infront");

        Node monitor = new Node("Monitor");
        monitor.setPreposition("on");

        Node man = new Node("Man");
        man.setPreposition("under");

        Node cheese = new Node("Cheese");
        cheese.setPreposition("on");


        graphObject.addChildren(Tree, table, null);
        graphObject.addChildren(Tree, table, chair);
        graphObject.addChildren(Tree, table, book);
        graphObject.addChildren(Tree, table, laptop);
        graphObject.addChildren(Tree, chair, box);
        graphObject.addChildren(Tree, book, cat);
        graphObject.addChildren(Tree, book, mouse);
        graphObject.addChildren(Tree, book, stapler);
        graphObject.addChildren(Tree, laptop, monitor);
        graphObject.addChildren(Tree, box, man);
        graphObject.addChildren(Tree, mouse, cheese);

        //populating the TreeString
        TreeString = TreeToString(Tree);

        //Spawn our object
        //Instantiate(objToSpawn);

        //Connect to MSAGL
        //Process.Start("notepad.exe");

        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = "MSAGL2.exe";
        psi.Arguments = TreeString;
        psi.WorkingDirectory = @"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release"; // or directory where you put the winapp 
        Process.Start(psi);



        //Process psi = new Process();
        //psi.StartInfo.FileName = "MSAGL2.exe";
        //psi.StartInfo.WorkingDirectory = @"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release"; // or directory where you put the winapp
        //psi.StartInfo.Arguments = "test";
        //psi.Start();

        ////Connect to MSAGL
        //ProcessStartInfo psi = new ProcessStartInfo();
        //psi.FileName = "MSAGL2.exe";
        //psi.Arguments = TreeString; //or just a filename with data
        //psi.WorkingDirectory = @"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release"; //or directory where you put the winapp 
        //Process.Start(psi);
    }

    //Function used to create the Scene
    public void ViewSemanticNetwork()
    {


        //Connect to MSAGL
        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = "MSAGL2.exe";
        psi.Arguments = TreeString; //or just a filename with data
        psi.WorkingDirectory = @"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release"; //or directory where you put the winapp 
        Process.Start(psi);
    }

    //method used to add nodes
    public void addChildren(IDictionary<Node, Node[]> tree, Node parent, Node child)
    {
        Node[] temp = new Node[] { }; //utility array

        //Checking if there already exists a relationship
        if (tree.ContainsKey(parent))
        {
            //get the current children
            temp = tree[parent];

            //add the new child
            temp = temp.Append(child).ToArray();

            //overwrite the current children
            tree[parent] = temp;

            //create new record with null children
            tree[child] = new Node[] { };
        }
        else
        {//if it is the first time adding a relationship
            tree[parent] = new Node[] { };
        }
    }

    //method used to turn a tree into a string
    public string TreeToString(IDictionary<Node, Node[]> tree)
    {
        //the new Tree which has a format of string,string
        IDictionary<string, string> Tree = new Dictionary<string, string>();

        string ValueArrayToString; //variable used to convert the values array into one continuous string
        string KeyToString; //variable used to convert Node key to string

        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            ValueArrayToString = string.Join("|", Array.ConvertAll(entry.Value, item => item.ToStringPrep())); //turning the value array into a string divided by '|'
            KeyToString = entry.Key.ToString(); //turning the Node to a string
            Tree.Add(KeyToString, ValueArrayToString); //adding a record but in string format
        }

        //Method to convert the Semantic Tree into a String
        string TreeString = string.Join(Environment.NewLine, Tree);

        return TreeString;
    }

    public Vector3 getSize(GameObject ObjectUnity) {
        return ObjectUnity.GetComponent<Collider>().bounds.size;
    }

    public double getWidth(GameObject ObjectUnity) {
        Vector3 size = getSize(ObjectUnity);
        return size.x;
    }

    public double getLength(GameObject ObjectUnity)
    {
        Vector3 size = getSize(ObjectUnity);
        return size.y;
    }

    public double getDepth(GameObject ObjectUnity)
    {
        Vector3 size = getSize(ObjectUnity);
        return size.z;
    }
}