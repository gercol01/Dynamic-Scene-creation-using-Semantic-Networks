using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button addButton; //add button object
    public Button createButton; //create button object
    public Button viewButton; //view button object
    public Button resetButton; //view button object
    public Button minimiseButton; //minimise button object
    public Button maximiseButton; //maximise button object
    public Button saveSceneButton; //save scene object
    public Button loadSceneButton; //load scene button

    GameObject Menu;
    GameObject maxButton;
    GameObject minButton;

    public GameObject cube; //cube object
    public GameObject sphere; //cube object
    public GameObject cylinder; //cube object

    public GameObject enemy;


    public Node temporaryNode;//used in the update method

    //Placeholder field
    public Text placeholder;

    //Canvas UI
    //public Canvas MainUI;

    //Input field
    public InputField commandBox;

    //Display canvas
    public Text outputCommands;

    //String format of the Semantic Tree
    string TreeString;

    //String format for saving of scene
    public string TreeStringSave;

    //String format for loading of scene
    public string TreeStringLoad;

    //creating the datastructure that will store the nodes
    public IDictionary<Node, Node[]> Tree = new Dictionary<Node, Node[]>();

    public IDictionary<Node, Node[]> TreeSet
    {
        get
        {
            return Tree;
        }
        set
        {
            Tree = value;
        }
    }

    public float creationDelay = 2.0f; // delay between object creation
    private bool canCreate = false; // flag to check if the object can be created
    private bool found = false;// flag used to check if the object has been destroyed or not

    // Start is called before the first frame update
    void Start()
    {
        Menu = GameObject.Find("Canvas");
        minButton = GameObject.Find("MinimiseButton");
        maxButton = GameObject.Find("MaximiseButton");
        maxButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //checking if the object can be created
        if (canCreate)
        {
            //get name of the parent
            string name = temporaryNode.ToString() + "temp";

            //get x coordinate
            int x = temporaryNode.returnX();
            int y = temporaryNode.returnY();
            int z = temporaryNode.returnZ();

            //create the cube that will not be destroyed
            Vector3 coordinates = new Vector3(x, y, z);

            //check if the object is already created
            if (GameObject.Find(name + "(Clone)") != null)
            {
                //check what type of object it is
                string objectType1 = temporaryNode.getObjectType();

                //if the object type is a cube
                if (string.Equals(objectType1, "Cube", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the cube the name
                    enemy.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemy, coordinates, enemy.transform.rotation);
                }

                ////update the position
                //GameObject obj = GameObject.Find(name + "(Clone)");
                //obj.transform.position = coordinates;
            }
            else
            {
                //check what type of object it is
                string objectType1 = temporaryNode.getObjectType();

                //if the object type is a cube
                if (string.Equals(objectType1, "Cube", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the cube the name
                    enemy.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemy, coordinates, enemy.transform.rotation);
                }
            }
            //resetting the flag
            canCreate = false;
        }
    }

    void OnEnable()
    {
        //Register Button Events
        addButton.onClick.AddListener(() => addCommand());
        createButton.onClick.AddListener(() => createScene());
        minimiseButton.onClick.AddListener(() => minimiseUI());
        maximiseButton.onClick.AddListener(() => maximiseUI());
        viewButton.onClick.AddListener(() => ViewSemanticNetwork());
        saveSceneButton.onClick.AddListener(() => saveScene());
        loadSceneButton.onClick.AddListener(() => loadScene());
    }

    public void addCommand() {
        //calling the method
        StartCoroutine(addCommand2());
    }

    public IEnumerator addCommand2()
    {
        //current nodes
        Node[] currentNodes = Tree.Keys.ToArray();

        //runner object to use methods
        Test graphObject = new Test();

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

            //if the command is Delete, flag to 2
            else if (String.Equals(words[0], "Delete", StringComparison.OrdinalIgnoreCase))
            {
                flag = 2;
            }

            //if the command is Move, flag to 3
            else if (String.Equals(words[0], "Move", StringComparison.OrdinalIgnoreCase))
            {
                flag = 3;
            }

            //if the command is Change, flag to 4
            else if (String.Equals(words[0], "Change", StringComparison.OrdinalIgnoreCase))
            {
                flag = 4;
            }

            //if the command is none of the above, flag to -1
            else
            {
                flag = -1;
            }

            //CHECK - it outputting invalid commands
            //display the command in the output commands box
            outputCommands.text = outputCommands.text + commandBox.text + "\n";

            //only called for the start nodes
            if (words.Length == 4 && flag == 0)//if the command is an Add command, that means that it is a start node, ex: Add Tree_1 at 2,0,0
            {
                //getting the name of the object
                string parent = words[1];

                //getting the object and removing the number from it, Tree_1 -> Tree, 1
                string[] objectTypeWithNo = words[1].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //getting the string of the coordinates, ex: 2,0,0
                string coordinates = words[3];

                //contains the coordinates 
                string[] coordinatesXYZ = { };

                //split the string into x, y and z coordnates
                coordinatesXYZ = words[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //creating the parent node
                Node parentNode = new Node(parent);

                //setting the object type 
                parentNode.setObjectType(objectTypeWithNo[0]);

                //setting the coordinates
                parentNode.setCoordinates(coordinatesXYZ.Select(int.Parse).ToArray());

                //check if there already exists an object with the same name
                Boolean resultCheckName = graphObject.checkExist(TreeSet, parentNode);

                if (resultCheckName == false) //object is unique
                {
                    //check if it collides with any other object
                    StartCoroutine(checkSceneCollidersParent(parentNode));

                    yield return new WaitForSeconds(creationDelay);

                    if (found == true)
                    {
                        found = false;

                        Boolean resultAddChildren = graphObject.addChildren(TreeSet, parentNode, null); //adding the first node to the tree - it has no children

                        if (resultAddChildren == false)
                        {
                            placeholder.text = "Could not add node!";
                            placeholder.color = Color.red;
                        }
                    }
                    else
                    {
                        placeholder.text = "Could not add node!";
                        placeholder.color = Color.red;
                    }
                }
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }


            }
            else if (words.Length == 4 && flag == 1)//if the command is a Create command and consists of 4 words, that means that it is a single relationship, ex: Create Book_1 under Tree_1
            {
                //the parent node
                string parent = words[3];

                //creating the parent node
                Node parentNode = new Node(parent);

                //the child node
                string child = words[1];

                //creating the child node
                Node childNode = new Node(child);

                //the preposition
                string preposition = words[2];

                string[] objectTypeWithNo = words[1].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                childNode.setPreposition(preposition);
                childNode.setObjectType(objectTypeWithNo[0]);

                //check if the parent itself exists
                Boolean resultCheckNameParent = graphObject.checkExist(TreeSet, parentNode);

                if (resultCheckNameParent == true)
                {//parent exists

                    //check if there is the same preposition for the same parent node
                    Boolean resultCheckPreposition = graphObject.checkPreposition(TreeSet, parentNode, childNode);

                    if (resultCheckPreposition == true)
                    {//there is no other child with the same preposition

                        //check if there already exists a child with the same name
                        Boolean resultCheckNameChild = graphObject.checkExist(TreeSet, childNode);

                        if (resultCheckNameChild == false) //object is unique
                        {
                            //create a copy of the current Tree
                            IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                            //add the new records
                            graphObject.addChildren(tempTree, parentNode, childNode);

                            //calculate the new coordinates
                            graphObject.setCoordinates(tempTree);

                            //pass the correct parentNode with coordinates
                            foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                            { 
                                //get name of the parent
                                string name1 = entry.Key.ToString();

                                //checking if the key has been found
                                if (entry.Key.ToString().Equals(childNode.ToString()))
                                {
                                    //check if it collides with any other object
                                    StartCoroutine(checkSceneCollidersParent(entry.Key));

                                    yield return new WaitForSeconds(creationDelay);

                                    if (found == true)
                                    {
                                        found = false;//reset the found flag

                                        //if correct make current tree equal to the new tree
                                        TreeSet = tempTree;
                                    }
                                    else
                                    {
                                        placeholder.text = "Could not add node!";
                                        placeholder.color = Color.red;
                                    }
                                }
                            }
                        }
                        else
                        {
                            placeholder.text = "Could not add node!";
                            placeholder.color = Color.red;
                        }
                    }
                    else
                    {
                        placeholder.text = "Could not add node!";
                        placeholder.color = Color.red;
                    }
                }
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

            }
            else if (words.Length == 6 && flag == 1)//if the command is a Create command and consists of 6 words, that means that it is a double relationship, ex: Create Book_1 under Tree_1 and Man_1
            {
                //the first parent node
                string parent1 = words[3];

                //creating the first parent node
                Node parentNode1 = new Node(parent1);

                //the second parent node
                string parent2 = words[5];

                //creating the second parent node
                Node parentNode2 = new Node(parent2);

                //the child node
                string child = words[1];

                //creating the child node
                Node childNode = new Node(child);

                string preposition = words[2];//the preposition
                string[] objectTypeWithNo = words[1].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                childNode.setPreposition(preposition);
                childNode.setObjectType(objectTypeWithNo[0]);

                //nodeList.Add(childNode);//adding the Node to the List

                //since it is a double relationship, we add two records
                graphObject.addChildren(Tree, parentNode1, childNode);
                graphObject.addChildren(Tree, parentNode2, childNode);
            }
            else if (words.Length == 2 && flag == 2)//if the command is a Delete command and consists of 2 words, ex: Delete cube_1
            {
                //the first parent node
                string delete = words[1];

                //creating the first parent node
                Node delNode = new Node(delete);

                //check that the object to be deleted exists
                Boolean checkResult = checkExist(TreeSet, delNode);

                if (checkResult)
                {
                    deleteNode(Tree, delNode);

                    //letting the frame pass
                    yield return new WaitForSeconds(creationDelay);
                }
                else {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }
            }
            else if (words.Length == 4 && flag == 3)//if the command is a Move command and consists of 4 words, ex: Move cube_1 to 2,0,0, Move cube_1 on cube_3
            {
                Boolean collisions = false;

                //the parent node
                string parent = words[1];

                //creating the parent node
                Node parentNode = new Node(parent);

                //getting the string of the coordinates, ex: 2,0,0
                string coordinates = words[3];

                //contains the coordinates 
                string[] coordinatesXYZ = { };

                //split the string into x, y and z coordinates
                coordinatesXYZ = coordinates.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the coordinates
                parentNode.setCoordinates(coordinatesXYZ.Select(int.Parse).ToArray());

                //create a temporary tree and recalculate the coordinates
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //add the new records
                changeTree(tempTree, parentNode);

                //calculate the new coordinates
                setCoordinates(tempTree);

                //check that the object to be moved exists
                Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                if (resultCheckExist)//if the parent object exists
                {
                    //List to store all the nodes involved
                    List<Node> nodesToMove = new List<Node>();

                    //add the parentNode as the start
                    foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                    {
                        //first thing to do is spawn the parent
                        if (entry.Key.ToString().Equals(parentNode.ToString()))
                        {
                            Node newNodeCopy = entry.Key.Copy();
                            nodesToMove.Add(newNodeCopy);
                        }
                    }

                    //get all the objects involved through iteration

                    //start with the children of the parentNode
                    Node[] temp = tempTree[parentNode];

                    //getting the first nodes from the array
                    foreach (Node tempNode in temp) {
                        Node newNodeCopy = tempNode.Copy();
                        nodesToMove.Add(newNodeCopy);
                    }

                    //list to compare
                    List<Node> nodesToMoveCopy = new List<Node>(nodesToMove);

                    Boolean repeat = true;

                    while (repeat) {
                        foreach (Node tNode in nodesToMove)
                        {
                            //get the children and add them to the list
                            Node[] temporary = tempTree[tNode];

                            //check if it has children
                            foreach (Node tempNode in temporary)
                            {
                                //check if the nodes are not already in the list
                                Boolean checkNode = nodesToMove.Contains(tempNode);

                                if (checkNode == false)
                                {
                                    Node newNodeCopy = tempNode.Copy();
                                    nodesToMoveCopy.Add(newNodeCopy);//add the new children (if any)
                                }
                                
                            }
                        }

                        if (nodesToMove.Count == nodesToMoveCopy.Count) {
                            repeat = false;
                        }
                        else
                        {
                            //update the list with the new Nodes
                            nodesToMove = new List<Node>(nodesToMoveCopy);
                            repeat = true;
                        }
                    }

                    //check that the new coordinates are not the same as the old coordinates
                    GameObject obj = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //current position of the object
                    Vector3 currentPosition = obj.transform.position;

                    int x = parentNode.returnX();
                    int y = parentNode.returnY();
                    int z = parentNode.returnZ();


                    if (currentPosition != new Vector3(x, y, z))
                    { //if they are not equal
                      ////check that the object to be moved exists
                      //Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                        if (resultCheckExist)//if the object exists
                        {
                            Boolean create = true;

                            //loop through the nodes to be moved

                            foreach (Node node in nodesToMove)
                            {
                                //change name as it is a temporary duplicate
                                node.setValue(node.ToString() + "Copy");

                                //check that the nodes can be moved
                                StartCoroutine(checkSceneCollidersParent(node));

                                yield return new WaitForSeconds(creationDelay);//handing control to Update()

                                if (found == true)//the node can be moved
                                {
                                    found = false;//reset the found flag

                                    create = true;//boolean to see if there are collisions in all children
                                }
                                else
                                {
                                    //the objects will not be moved as there is a collision
                                    create = false;

                                    placeholder.text = "Could not add node!";
                                    placeholder.color = Color.red;

                                    break;
                                }
                            }

                            //if there are no collisions at all
                            if (create == true)
                            {
                                //the objects can be moved

                                //delete the copies of the objects
                                foreach (Node node in nodesToMove) {
                                    GameObject objToDelete = GameObject.Find(node.ToString() + "temp(Clone)");

                                    //destroying the object
                                    Destroy(objToDelete);

                                    yield return new WaitForSeconds(creationDelay);

                                    //find the object
                                    String originalObject = node.ToString().Replace("Copy", "");

                                    GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                    objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                    yield return new WaitForSeconds(creationDelay);

                                    //find the created
                                    if (GameObject.Find(originalObject + "(Clone)")) {
                                        GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                        objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                        yield return new WaitForSeconds(creationDelay);
                                    }
                                }

                                //update the Tree
                                TreeSet = tempTree;
                            }
                            else
                            {
                                placeholder.text = "Could not add node!";
                                placeholder.color = Color.red;
                            }


                            //if (found == true)//the parent can be moved
                            //{
                            //    found = false;//reset the found flag

                            //    Boolean create = true;//boolean to see if there are collisions in all children

                            //    //create a copy of the Tree
                            //    IDictionary<Node, Node[]> tree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                            //    //get the children of the parent node
                            //    Node[] temp = tree[parentNode];

                            //    //copy of the temp array, used to address the parent Node
                            //    Node[] myCopy = new Node[temp.Length];
                            //    Array.Copy(temp, myCopy, temp.Length);

                            //    //recalculate the coordinates of the children in relation to the parent node
                            //    foreach (Node node in temp)
                            //    {
                            //        Node tempNode = node;
                            //        calculatePrepositionCoordinates(parentNode, tempNode);

                            //        //check the collisions of the children
                            //        StartCoroutine(checkSceneCollidersParent(tempNode));

                            //        yield return new WaitForSeconds(creationDelay); //handing control to Update()

                            //        if (found == false) //if there is a collision, the rest of the children are ignored
                            //        {
                            //            create = false;
                            //            break;
                            //        }
                            //    }

                            //    //if there are no collisions at all
                            //    if (create == true)
                            //    {
                            //        //update the coordinates of the parent node in the tree
                            //        tree[temp] = parentNode;

                            //        //update the coordinates of the child node in the tree


                            //        //update the Tree
                            //        TreeSet = tree;
                            //    }
                            //    else
                            //    {
                            //        placeholder.text = "Could not add node!";
                            //        placeholder.color = Color.red;
                            //    }
                            //}
                            //else
                            //{
                            //    placeholder.text = "Could not add node!";
                            //    placeholder.color = Color.red;
                            //}
                        }
                        else
                        {
                            placeholder.text = "Could not add node!";
                            placeholder.color = Color.red;
                        }
                    }
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }
            }
            else if (words.Length == 4 && flag == 4)//if the command is a Change command and consists of 4 words, ex: Change cube_1 to red
            {
                string objectToChange = words[1];

                string color = words[3];

                // Next, create a new Color variable
                Color newColor;

                if (GameObject.Find(objectToChange + "(Clone)")) {
                    // First, get a reference to the GameObject you want to change the color of
                    GameObject myObject = GameObject.Find(objectToChange + "(Clone)");

                    // Next, get a reference to the object's Renderer component
                    Renderer myRenderer = myObject.GetComponent<Renderer>();

                    // Then, use the ColorUtility class to convert the string to a Color value
                    if (ColorUtility.TryParseHtmlString(color, out newColor))
                    {
                        // If the string was successfully parsed, you can now use the newColor value to set the color of your GameObject
                        myRenderer.material.color = newColor;
                    }
                    else
                    {
                        Console.WriteLine("Test");
                    }
                }
                else {
                    placeholder.text = "Incorrect input!";
                    placeholder.color = Color.red;
                }
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
    }

    public void testMethod()
    {
        cylinder.name = "testing";

        //spawn the parent object with specified coordinates
        Instantiate(cylinder);

        if (GameObject.Find("testing(Clone)") != null)
        {
            Console.WriteLine("found One");
        }
        else
        {
            UnityEngine.Debug.Log("not there");
        }
    }

    //method to check if the node already exists
    public Boolean checkExist(IDictionary<Node, Node[]> tree, Node node) {
        //Checking if the parent already exists
        if (tree.ContainsKey(node))
        {
            return true;//exists an object with the same name
        }
        else 
        {
            return false;//does not exist an object with the same name
        }
    }

    //method to check if there are two child nodes with the same preposition
    public Boolean checkPreposition(IDictionary<Node, Node[]> tree, Node parent, Node child)
    {
        //Check if there already exists an object with the same preposition, ex: book is *left_of table*, man is *left_of table*

        string preposition = child.getPreposition();//getting the preposition of the child

        Node[] children = tree[parent];//getting the current objects related to the parent

        //checking if the objects have the same prepositions as the new child node
        foreach (Node node in children)
        {
            if (node.getPreposition().Equals(preposition))
            {
                return false; //already exists a child with the same preposition
            }
        }

        return true;// not found
    }

    //method used to add nodes
    public Boolean addChildren(IDictionary<Node, Node[]> tree, Node parent, Node child)
    {
        Node[] temp = new Node[] { }; //utility array

        //Checking if the parent already exists
        if (tree.ContainsKey(parent))
        {
            //Check if there already exists an object with the same preposition, ex: book is *left_of table*, man is *left_of table*

            string preposition = child.getPreposition();//getting the preposition of the child

            //get the current children
            temp = tree[parent];

            //add the new child
            temp = temp.Append(child).ToArray();

            //overwrite the current children
            tree[parent] = temp;

            //in the case of the between preposition
            if (tree.ContainsKey(child) == false)
            {
                //create new record with null children
                tree[child] = new Node[] { };
            }

            return true; //successful creation
        }
        else
        {//if it is the first time adding a relationship
            if (parent.getCoordinatesNotNull() == true)
            { //if the node is a starter node, it must have predefined coordinates 
                tree[parent] = new Node[] { };

                TreeSet = tree;

                return true; //successful creation
            }
            else
            {
                return false; //unsuccessful creation
            }
        }
    }

    public void createScene()
    {
        //check if the tree has objects in it
        if (Tree.Count() == 0)
        {
            placeholder.text = "No objects have been created";
            placeholder.color = Color.red;
        }
        else
        {
            //iterate through the Tree
            foreach (KeyValuePair<Node, Node[]> entry in Tree)
            {
                //first thing to do is spawn the parent

                //get name of the parent
                string name1 = entry.Key.ToString();

                //Check if the object has already been created
                Boolean existsFlag1 = checkScene(name1 + "(Clone)");

                if (existsFlag1 == false)
                {
                    //get x coordinate
                    int x = entry.Key.returnX();
                    int y = entry.Key.returnY();
                    int z = entry.Key.returnZ();

                    //create coordinates for the object
                    Vector3 coordinates1 = new Vector3(x, y, z);

                    //check what type of object it is
                    string objectType1 = entry.Key.getObjectType();

                    //if the object type is a cube
                    if (string.Equals(objectType1, "Cube", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //giving the cube the name
                        cube.name = name1;

                        //spawn the parent object with specified coordinates
                        Instantiate(cube, coordinates1, cube.transform.rotation);

                        //giving the cube the name
                        enemy.name = name1 + "temp";

                        //spawn the parent object with specified coordinates
                        Instantiate(enemy, coordinates1, enemy.transform.rotation);
                    }
                    else if (string.Equals(objectType1, "Sphere", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //giving the sphere the name
                        sphere.name = name1;

                        //spawn the parent object with specified coordinates
                        Instantiate(sphere, coordinates1, sphere.transform.rotation);
                    }
                    else if (string.Equals(objectType1, "Cylinder", StringComparison.CurrentCultureIgnoreCase))
                    {
                        //giving the cylinder the name
                        cylinder.name = name1;

                        //spawn the parent object with specified coordinates
                        Instantiate(cylinder, coordinates1, cylinder.transform.rotation);
                    }
                }


                //checking if it has children
                if (entry.Value.Length != 0)
                {
                    //iterate through the children
                    foreach (Node child in entry.Value)
                    {
                        //spawn the children objects

                        //get name of the child
                        string name2 = child.ToString();

                        //Check if the object has already been created
                        Boolean existsFlag2 = checkScene(name2 + "(Clone)");

                        if (existsFlag2 == false)
                        {
                            //get the coordinates - this is done by calling preposition method
                            //check which type of method is needed depending on the preposition of the object

                            string preposition = child.getPreposition();

                            //if the preposition is 'between', that means that the that it has two parents
                            if (preposition.Equals("between"))
                            {

                                //array to store the parents of the node
                                Node[] parents = new Node[] { };

                                //there should be 2 records in dictionary if the preposition is 'between'
                                foreach (KeyValuePair<Node, Node[]> possibleParent in Tree)
                                {
                                    //get the values of the current record
                                    Node[] tempArray = possibleParent.Value;

                                    //checking if the child is in the list of values of the current record
                                    if (tempArray.Contains(child))
                                    {
                                        //if found, that means a parent has been found
                                        parents = parents.Append(possibleParent.Key).ToArray();
                                    }
                                }

                                //calculate the coordinates
                                calculatePrepositionCoordinates2Parents(parents[0], parents[1], child);
                            }
                            else
                            {
                                //calculate the coordinates
                                calculatePrepositionCoordinates(entry.Key, child);
                            }

                            //get x coordinate
                            int X = child.returnX();
                            int Y = child.returnY();
                            int Z = child.returnZ();

                            //create coordinates for the object
                            Vector3 coordinates2 = new Vector3(X, Y, Z);

                            //check what type of object it is
                            string objectType2 = entry.Key.getObjectType();

                            //if the object type is a cube
                            if (string.Equals(objectType2, "Cube", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //giving the cube the name
                                cube.name = name2;

                                //spawn the parent object
                                Instantiate(cube, coordinates2, cube.transform.rotation);

                                //giving the cube the name
                                enemy.name = name2 + "temp";

                                //spawn the parent object with specified coordinates
                                Instantiate(enemy, coordinates2, enemy.transform.rotation);

                            }
                            else if (string.Equals(objectType2, "Sphere", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //giving the cube the name
                                sphere.name = name2;

                                //spawn the parent object
                                Instantiate(sphere, coordinates2, sphere.transform.rotation);
                            }
                            else if (string.Equals(objectType2, "Cylinder", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //giving the cube the name
                                cylinder.name = name2;

                                //spawn the parent object
                                Instantiate(cylinder, coordinates2, cylinder.transform.rotation);
                            }
                        }
                    }
                }
            }
        }
    }

    public Boolean checkScene(String name)
    {
        if (GameObject.Find(name) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator checkSceneCollidersParent(Node node)
    {
        canCreate = true;

        //passing the attributes to the global temporary node
        temporaryNode = node;

        //giving permission to the update method
        yield return new WaitForSeconds(creationDelay);

        //object is created in the update method

        //get name of the parent
        string name = node.ToString() + "temp";

        //here we should check
        if (GameObject.Find(name + "(Clone)") != null)
        {
            //found
            found = true;

            if (GameObject.FindWithTag("Enemy")) {
                //if found, change the tag to Final from Enemy
                GameObject obj = GameObject.FindWithTag("Enemy");
                obj.tag = "Final";
            }
            
        }
        else
        {
            //not found
            found = false;
        }

        //destroy the object after anyways
        //GameObject obj = GameObject.FindWithTag("Enemy");
        //Destroy(obj);
    }

    public IEnumerator checkSceneCollidersChild(Node node, Node childnode)
    {
        canCreate = true;

        //check if the parent node exists
        if (Tree.ContainsKey(node))
        {

            //passing the attributes to the global temporary node
            temporaryNode = childnode;

            //giving permission to the update method
            yield return new WaitForSeconds(creationDelay);

            //object is created in the update method

            //get name of the parent
            string name = childnode.ToString() + "temp";

            //here we should check
            if (GameObject.Find(name + "(Clone)") != null)
            {
                //found
                found = true;

                //if found, change the tag to Final from Enemy
                GameObject obj = GameObject.FindWithTag("Enemy");
                obj.tag = "Final";
            }
            else
            {
                //not found
                found = false;
            }
        }
        else {
            //not found
            found = false;
        }

        ////destroy the object after anyways
        //GameObject obj = GameObject.FindWithTag("Enemy");
        //Destroy(obj);
    }

    public void changeTree(IDictionary<Node, Node[]> tree, Node node) {
        //iterate through the tree
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //first thing to do is spawn the parent
            if (entry.Key.ToString().Equals(node.ToString())) {
                int x = node.returnX();
                int y = node.returnY();
                int z = node.returnZ();

                //adding the coordinates
                int[] coordinates = new int[3] { x,y,z};

                entry.Key.setCoordinates(coordinates);
            }
        }


        ////get the current children of the parentNode
        //Node[] currentChildren = TreeSet[node];

        ////iterate through the Tree, delete all instances of the node
        //tree.Remove(node);

        ////add a new record in the Tree with the children, it should be updated with the new coordinates

        //tree[node] = currentChildren;

        //foreach (Node currentNode in currentChildren) {
        //    addChildren(tempTree, node, currentNode);
        //}
        
    }

    public void setCoordinates(IDictionary<Node, Node[]> tree)
    {
        //check if the tree has objects in it
        if (tree.Count() == 0)
        {
            placeholder.text = "No objects have been created";
            placeholder.color = Color.red;
        }
        else
        {
            //iterate through the Tree
            foreach (KeyValuePair<Node, Node[]> entry in tree)
            {
                //first thing to do is spawn the parent

                //get name of the parent
                string name1 = entry.Key.ToString();


                //checking if it has children
                if (entry.Value.Length != 0)
                {
                    //iterate through the children
                    foreach (Node child in entry.Value)
                    {
                        //spawn the children objects

                        //get name of the child
                        string name2 = child.ToString();

                        //get the coordinates - this is done by calling preposition method
                        //check which type of method is needed depending on the preposition of the object

                        string preposition = child.getPreposition();

                        //if the preposition is 'between', that means that the that it has two parents
                        if (preposition.Equals("between"))
                        {

                            //array to store the parents of the node
                            Node[] parents = new Node[] { };

                            //there should be 2 records in dictionary if the preposition is 'between'
                            foreach (KeyValuePair<Node, Node[]> possibleParent in tree)
                            {
                                //get the values of the current record
                                Node[] tempArray = possibleParent.Value;

                                //checking if the child is in the list of values of the current record
                                if (tempArray.Contains(child))
                                {
                                    //if found, that means a parent has been found
                                    parents = parents.Append(possibleParent.Key).ToArray();
                                }
                            }

                            //calculate the coordinates
                            calculatePrepositionCoordinates2Parents(parents[0], parents[1], child);
                        }
                        else
                        {
                            //calculate the coordinates
                            calculatePrepositionCoordinates(entry.Key, child);
                        }
                    }
                }
            }
        }
    }

    //method to create an object
    public void creator(Node node)
    {
        //get name of the parent
        string name = node.ToString();

        //get x coordinate
        int x = node.returnX();
        int y = node.returnY();
        int z = node.returnZ();

        //create the cube that will not be destroyed
        Vector3 coordinates = new Vector3(x, y, z);

        //check what type of object it is
        string objectType1 = node.getObjectType();

        //if the object type is a cube
        if (string.Equals(objectType1, "Cube", StringComparison.CurrentCultureIgnoreCase))
        {
            //giving the cube the name
            cube.name = name;

            //spawn the parent object with specified coordinates
            Instantiate(cube, coordinates, cube.transform.rotation);
        }
    }

    public void calculatePrepositionCoordinates(Node parent, Node child)
    {
        string preposition = child.getPreposition();

        //get the coordinates of the first parent
        int coordX = parent.returnX();
        int coordY = parent.returnY();
        int coordZ = parent.returnZ();

        //creating values to store the coordinates of the child
        int coordx;
        int coordy;
        int coordz;

        //array to store the new coordinates
        int[] coordinates;

        if (preposition.Equals("left_of"))
        {
            coordx = coordX - 2; //movement on the x-axis
            coordy = coordY;
            coordz = coordZ;

            //adding the coordinates
            coordinates = new int[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("right_of"))
        {
            coordx = coordX + 2; //movement on the x-axis
            coordy = coordY;
            coordz = coordZ;

            //adding the coordinates
            coordinates = new int[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("on"))
        {
            coordx = coordX;
            coordy = coordY + 1; //movement on the y-axis
            coordz = coordZ;

            //adding the coordinates
            coordinates = new int[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("under"))
        {
            coordx = coordX;
            coordy = coordY; //movement on the y-axis
            coordz = coordZ;

            //adding the coordinates
            coordinates = new int[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("infront"))
        {
            coordx = coordX;
            coordy = coordY; //movement on the y-axis
            coordz = coordZ - 2;

            //adding the coordinates
            coordinates = new int[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("behind"))
        {
            coordx = coordX;
            coordy = coordY;
            coordz = coordZ + 2;//movement on the z-axis

            //adding the coordinates
            coordinates = new int[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
    }

    IEnumerator CheckAllNodes(Node[] nodesArray)
    {
        foreach (Node node in nodesArray)
        {
            yield return StartCoroutine(checkSceneCollidersParent(node));
        }
    }

    public void deleteNode(IDictionary<Node, Node[]> tree, Node node) {
        IDictionary<Node, Node[]> tempTree = tree.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);

        //iterate through the Tree, delete all instances of the node
        tempTree.Remove(node);

        // Create a list of nodes to be deleted
        List<Node> nodesToDelete = new List<Node>();

        // Iterate through the original dictionary to find all instances of the node to be deleted
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            // Iterate through the children
            for (int i = 0; i < entry.Value.Length; i++)
            {
                Node child = entry.Value[i];
                if (child.Equals(node))
                {
                    // Add the node to the list of nodes to be deleted
                    nodesToDelete.Add(node);

                    // Remove the node from the parent's child array
                    List<Node> tempList = new List<Node>(entry.Value);
                    tempList.RemoveAt(i);

                    // Add the updated child array back to the dictionary
                    tempTree.Remove(entry.Key);
                    tempTree.Add(entry.Key, tempList.ToArray());
                }
            }
        }

        // Delete all nodes from the list of nodes to be deleted
        foreach (Node nodeToDelete in nodesToDelete)
        {
            tree.Remove(nodeToDelete);
        }

        // Set the original dictionary to the modified dictionary
        TreeSet = tempTree;

        //delete from the object from the scene aswell
        GameObject obj = GameObject.Find(node.ToString() + "(Clone)");
        Destroy(obj);

        //delete from the object from the scene aswell
        GameObject objTemp = GameObject.Find(node.ToString() + "temp" + "(Clone)");
        Destroy(objTemp);
    }

    //public IEnumerator MoveNode(Node parentNode, int[] newPosition)
    //{
    //    // Move the parent object to the new position
    //    GameObject parentObj = GameObject.Find(parentNode.ToString() + "(Clone)");
    //    parentObj.transform.position = Vector3(newPosition[0], newPosition[0], newPosition[0]);

    //    // Move the child objects with the parent
    //    foreach (Node childNode in TreeSet[parentNode])
    //    {
    //        GameObject childObj = GameObject.Find(childNode.ToString() + "(Clone)");
    //        childObj.transform.SetParent(parentObj.transform, true);
    //    }

    //    // Wait for a frame to allow the child objects to update their positions
    //    yield return null;
    //}




    //public void deleteNode(IDictionary<Node, Node[]> tree, Node node) {
    //    IDictionary<Node, Node[]> tempTree = tree;


    //    //iterate through the Tree, delete all instances of the node
    //    //tempTree.Remove(node);

    //    //iterate through the Tree
    //    foreach (KeyValuePair<Node, Node[]> entry in tree)
    //    {
    //        //iterate through the children
    //        foreach (Node child in entry.Value)
    //        {
    //            if (child.Equals(node)) {
    //                //get the full array of children for the current parent
    //                Node[] temp = tree[entry.Key];
    //                List<Node> tempList = new List<Node>();

    //                //traversing the array
    //                foreach(Node nodeTemp in temp) {
    //                    if (!nodeTemp.Equals(node)) {
    //                        tempList.Add(nodeTemp);
    //                    }
    //                }

    //                //overwriting the array
    //                temp = tempList.ToArray();

    //                tempTree[entry.Key] = temp;

    //                var x = 0;
    //            }

    //            var y = 0;
    //        }

    //        var z = 0;
    //    }

    //    TreeSet = tempTree;

    //    //delete from the object from the scene aswell
    //    GameObject obj = GameObject.Find(node.ToString() + "(Clone)");
    //    Destroy(obj);

    //    //delete from the object from the scene aswell
    //    GameObject objTemp = GameObject.Find(node.ToString() + "temp" + "(Clone)");
    //    Destroy(objTemp);
    //}

    public void calculatePrepositionCoordinates2Parents(Node parent1, Node parent2, Node child)
    {
        //this is obvious that it is "between"
        string preposition = child.getPreposition();

        if (preposition.Equals("between"))
        {
            //get the coordinates of the first parent
            int coordX1 = parent1.returnX();
            int coordY1 = parent1.returnY();
            int coordZ1 = parent1.returnZ();

            //get the coordinates of the second parent
            int coordX2 = parent2.returnX();
            int coordY2 = parent2.returnY();
            int coordZ2 = parent2.returnZ();

            //calculating the coordinates of the child
            int coordX3 = (coordX1 + coordX2) / 2;
            int coordY3 = (coordY1 + coordY2) / 2;
            int coordZ3 = (coordZ1 + coordZ2) / 2;

            //array to store the new coordinates
            int[] coordinates = new int[3] { coordX3, coordY3, coordZ3 };

            //create coordinates for the object
            child.setCoordinates(coordinates);
        }
    }

    public Vector3 getSize(GameObject ObjectUnity)
    {
        return ObjectUnity.GetComponent<Collider>().bounds.size;
    }

    public double getWidth(GameObject ObjectUnity)
    {
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

    public void ResetScene()
    {
        //Destroy(table.getObject());

        //Node chair = new Node("Chair");
        //chair.setPreposition("on");
        //table.setObject(sphere);
    }

    public void testMSAGL2()
    {

        ProcessStartInfo psi = new ProcessStartInfo(@"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release\MSAGL2.exe");
        //psi.FileName = "MSAGL2.exe";
        psi.Arguments = "[Gerard,Chair-on|Book-under|Laptop-right_of]&[Chair,Box-left_of]&[Book,Cat-left_of|Mouse-behind|Stapler-infront]&[Laptop,Monitor-on]&[Box,Man-under]&[Cat,]&[Mouse,Cheese-on]&[Stapler,]&[Monitor,]&[Man,]&[Cheese,]"; //or just a filename with data
                                                                                                                                                                                                                                                //psi.WorkingDirectory = @"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release\MSAGL2.exe"; //or directory where you put the winapp 
        Process.Start(psi);

        ////Connect to MSAGL
        //ProcessStartInfo psi = new ProcessStartInfo(@"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release\MSAGL2.exe");
        ////psi.FileName = "MSAGL2.exe";
        //psi.Arguments = "[Gerard,Chair-on|Book-under|Laptop-right_of]"; //or just a filename with data
        ////psi.WorkingDirectory = @"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release\MSAGL2.exe"; //or directory where you put the winapp 
        //Process.Start(psi);
    }

    //method used to turn a tree into a string
    public void TreeToString(IDictionary<Node, Node[]> tree)
    {
        //check if the tree has objects in it
        if (tree.Count() == 0)
        {
            placeholder.text = "No objects have been created";
            placeholder.color = Color.red;
        }
        else
        {
            //the new Tree which has a format of string,string
            IDictionary<string, string> Tree = new Dictionary<string, string>();

            //check if the Tree is empty 

            string ValueArrayToString; //variable used to convert the values array into one continuous string
            string KeyToString; //variable used to convert Node key to string

            foreach (KeyValuePair<Node, Node[]> entry in tree)
            {
                ValueArrayToString = string.Join("|", Array.ConvertAll(entry.Value, item => item.ToStringPrep())); //turning the value array into a string divided by '|'
                KeyToString = entry.Key.ToString(); //turning the Node to a string
                Tree.Add(KeyToString, ValueArrayToString); //adding a record but in string format
            }

            //Method to convert the Semantic Tree into a String
            TreeString = string.Join(Environment.NewLine, Tree);

            //remove \r\n
            TreeString = TreeString.Replace("\r\n", "&");

            //remove spaces
            TreeString = TreeString.Replace(" ", "");
        }
    }

    //method used to turn a tree into a string
    public void TreeToStringExtended(IDictionary<Node, Node[]> tree)
    {
        //check if the tree has objects in it
        if (tree.Count() == 0)
        {
            placeholder.text = "No objects have been created";
            placeholder.color = Color.red;
        }
        else
        {
            //the new Tree which has a format of string, string
            IDictionary<string, string> StringTree = new Dictionary<string, string>();

            //check if the Tree is empty 

            string ValueArrayToString; //variable used to convert the values array into one continuous string
            string KeyToString; //variable used to convert Node key to string

            foreach (KeyValuePair<Node, Node[]> entry in tree)
            {
                ValueArrayToString = string.Join("|", Array.ConvertAll(entry.Value, item => item.ToStringWithLocationAndPreposition())); //turning the value array into a string divided by '|'
                KeyToString = entry.Key.ToStringWithLocationAndPreposition(); //turning the Node to a string
                StringTree.Add(KeyToString, ValueArrayToString); //adding a record but in string format
            }

            //Method to convert the Semantic Tree into a String
            TreeStringSave = string.Join(Environment.NewLine, StringTree);

            //remove \r\n and replace with "&"
            TreeStringSave = TreeStringSave.Replace("\r\n", "&");

            //remove spaces
            TreeStringSave = TreeStringSave.Replace(" ", "");
        }
    }

    //Function used to view the Semantic Network
    public void ViewSemanticNetwork()
    {
        //Instantiate the TreeString
        TreeToStringExtended(Tree);

        //Connect to MSAGL
        ProcessStartInfo psi = new ProcessStartInfo(@"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release\MSAGL2.exe");
        psi.Arguments = TreeStringSave;
        
        //psi.WorkingDirectory = @"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release\MSAGL2.exe"; //or directory where you put the winapp 
        Process.Start(psi);

        ////Connect to MSAGL
        //ProcessStartInfo psi = new ProcessStartInfo();
        //psi.FileName = "MSAGL2.exe";
        //psi.Arguments = TreeString; //or just a filename with data
        //psi.WorkingDirectory = @"C:\Users\User\Desktop\MSAGL2\MSAGL2\bin\Release"; //or directory where you put the winapp 
        //Process.Start(psi);
    }

    public void saveScene()
    {
        //Instantiate the TreeString
        TreeToStringExtended(Tree);

        //Add a UI
        //Directory location
        StreamWriter streamWriter = new StreamWriter(@"C:\Users\User\Desktop\Test.txt");

        //Write the TreeString in the Text file
        streamWriter.WriteLine(TreeStringSave);

        //Close the file
        streamWriter.Close();
    }

    public void loadScene()
    {
        //runner object to use methods
        Test graphObject = new Test();

        //Directory location
        StreamReader streamReader = new StreamReader(@"C:\Users\User\Desktop\Test.txt");

        //checking if the notepad is empty
        if ((TreeStringLoad = streamReader.ReadLine()) != null)
        {

            //dividing the string into records
            string[] TreeRecords = TreeStringLoad.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            //iterating through the records
            foreach (string record in TreeRecords)
            {
                string removeBrackets = record.Replace("[", "");
                removeBrackets = removeBrackets.Replace("]", "");

                //split the record into the key and values
                string[] KeyAndValue = removeBrackets.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //creating the parent node its values will be set later
                string name = "";
                Node parent = new Node(name);

                //iterate through the keys and values
                for (int i = 0; i < KeyAndValue.Length; i++)
                {
                    //if it is the first node it is the parent node, i.e. the key
                    if (i == 0)
                    {
                        //split the key/value into name, coordinates and preposition
                        string[] NameCoordinatesPreposition = KeyAndValue[i].Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

                        //setting objectType from the name, Ex: cube_1
                        string[] objectTypeWithNo = NameCoordinatesPreposition[0].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                        //setting preposition, Ex: on, null etc
                        string preposition = NameCoordinatesPreposition[2];

                        //setting coordinatesString
                        string coordinatesString = NameCoordinatesPreposition[1];
                        string[] coordinates = coordinatesString.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] intCoordinates = Array.ConvertAll(coordinates, int.Parse);

                        //setting value
                        parent.setValue(NameCoordinatesPreposition[0]);
                        parent.setObjectType(objectTypeWithNo[0]);
                        parent.setPreposition(preposition);
                        parent.setCoordinates(intCoordinates);

                        //check if the Tree already contains the node
                        if (Tree.ContainsKey(parent) == false)
                        {
                            Node newNodeCopy = parent.Copy();

                            //Add the node to the Tree
                            Boolean resultAddChildren = graphObject.addChildren(Tree, newNodeCopy, null); //adding the first node to the tree - it has no children

                            if (resultAddChildren == false)
                            {
                                placeholder.text = "Could not add node!";
                                placeholder.color = Color.red;
                            }
                        }
                    }
                    else //else it is a child node, i.e. a value
                    {
                        //creating the child node its values will be set later
                        Node child = new Node(name);

                        //split the values by the '|'
                        string[] values = KeyAndValue[i].Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string value in values){
                            //split the key/value into name, coordinates and preposition
                            string[] NameCoordinatesPreposition = value.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

                            //setting objectType from the name, Ex: cube_1
                            string[] objectTypeWithNo = NameCoordinatesPreposition[0].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                            //setting preposition, Ex: on, null etc
                            string preposition = NameCoordinatesPreposition[2];

                            //setting coordinatesString
                            string coordinatesString = NameCoordinatesPreposition[1];
                            string[] coordinates = coordinatesString.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                            int[] intCoordinates = Array.ConvertAll(coordinates, int.Parse);

                            //setting value
                            child.setValue(NameCoordinatesPreposition[0]);
                            child.setObjectType(objectTypeWithNo[0]);
                            child.setPreposition(preposition);
                            child.setCoordinates(intCoordinates);

                            //check if the Tree already contains the node
                            if (Tree.ContainsKey(child) == false)
                            {
                                Node newNodeCopyParent = parent.Copy();

                                Node newNodeCopy = child.Copy();

                                Boolean resultAddChildren = graphObject.addChildren(Tree, newNodeCopyParent, newNodeCopy);

                                if (resultAddChildren == false)
                                {
                                    placeholder.text = "Could not add relationship!";
                                    placeholder.color = Color.red;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void minimiseUI()
    {
        minButton.SetActive(false);
        Menu.SetActive(false);
        maxButton.SetActive(true);
    }

    public void maximiseUI()
    {
        maxButton.SetActive(false);
        Menu.SetActive(true);
        minButton.SetActive(true);
    }
}