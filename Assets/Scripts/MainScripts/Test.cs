using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    //camera buttons
    public Button topCameraButton; //change to top camera button
    public Button frontCameraButton; //change to top camera button
    public Button backCameraButton; //change to top camera button
    public Button leftCameraButton; //change to top camera button
    public Button rightCameraButton; //change to top camera button
    public Button worldCameraButton; //change to top camera button
    public Button cameraButton; //to access the camera menu
    public Button minCameraButton; //to minimise the camera menu

    //collision box button
    public Button collisionBoxButton; //button used to enable/disable the collision boxes

    //rotation axis button
    public Button axisButton; //button used to enable/disable the rotation axis

    //cameras
    public Camera topCamera;
    public Camera frontCamera;
    public Camera backCamera;
    public Camera leftCamera;
    public Camera rightCamera;
    public Camera worldCamera;

    //rotation axis rings
    public GameObject ringX;
    public GameObject ringY;
    public GameObject ringZ;

    //GUI elements
    GameObject Menu;
    GameObject CameraMenu;
    GameObject maxButton;
    GameObject minButton;

    //flags to indicate if certain GUI elements show or not
    protected Boolean nameAndCoordinates;
    protected Boolean faces;
    protected Boolean rotationAxis = false;

    //materials
    public Material Fabric;
    public Material MetalDark;
    public Material MetalLight;
    public Material WoodDark;
    public Material WoodLight;
    public Material UtilityMaterial;

    //a utility array used to store objects when rotating, moving etc
    public List<string> listOfObjects = new List<string>();

    public void AddStringToList(string newString)
    {
        listOfObjects.Add(newString);
    }

    public void RemoveStringFromList(string stringToRemove)
    {
        listOfObjects.Remove(stringToRemove);
    }

    public string[] GetListAsArray()
    {
        return listOfObjects.ToArray();
    }

    //list of all the specific materials
    List<string> colorList = new List<string> { "wood_dark", "wood_light", "metal_dark", "metal_light", "fabric"};

    //list of all the objects
    List<string> objectList = new List<string> { "table", "chair", "sofa", "fridge", "armchair" , "vase", "oven", "lamp", "bed", "cup", "nightstand", "carpet"};

    public GameObject cube; //cube object
    public GameObject sphere; //cube object
    public GameObject cylinder; //cube object

    //Gameobjects
    public GameObject fridge; //fridge object
    public GameObject cup; //cup object
    public GameObject vase; //vase object
    public GameObject sofa; //sofa object
    public GameObject chair; //chair object
    public GameObject table; //table object
    public GameObject armchair; //armchair object
    public GameObject bed; //armchair object
    public GameObject lamp; //lamp object
    public GameObject nightstand; //nightstand object
    public GameObject oven; //nightstand object
    public GameObject carpet; //nightstand object


    public GameObject wallVertical; //wall object
    public GameObject wallHorizontal; //wall object

    //enemy Gameobjects
    public GameObject enemyCube;
    public GameObject enemyFridge; //fridge object
    public GameObject enemyCup; //cup object
    public GameObject enemySofa; //sofa object
    public GameObject enemyChair; //chair object
    public GameObject enemyTable; //table object
    public GameObject enemyArmchair; //armchair object
    public GameObject enemyBed; //armchair object
    public GameObject enemyLamp; //lamp object
    public GameObject enemyNightstand; //nightstand object
    public GameObject enemyOven; //nightstand object
    public GameObject enemyCarpet; //nightstand object
    public GameObject enemyVase; //nightstand object

    public GameObject enemyWallVertical; //wall object
    public GameObject enemyWallHorizontal; //wall object

    private GameObject[] gameObjects; //a list of all the current Object GameObjects in the scene
    public Material materialCollisionBox; //the collision box material
    private Boolean collisionBoxFlag = true; //the collision box flag
    
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

    //Update method parameters
    public float creationDelay = 2.0f; // delay between object creation
    private bool canCreate = false; // flag to check if the object can be created
    private bool canCreateTrue = false; // flag to check if the true object can be created
    private bool found = false;// flag used to check if the object has been destroyed or not
    public Node temporaryNode;//used in the update method

    // Start is called before the first frame update
    void Start()
    {
        Menu = GameObject.Find("Canvas");
        minButton = GameObject.Find("MinimiseButton");
        maxButton = GameObject.Find("MaximiseButton");
        maxButton.SetActive(false);

        //setting the camera menu to inactive
        CameraMenu = GameObject.Find("CameraCanvas");
        CameraMenu.SetActive(false);

        //initialising the front camera as the default camera
        frontCamera.enabled = true;    // enable the top camera
        frontCamera.tag = "MainCamera";


        //setting the cameras to false
        topCamera.enabled = false;
        leftCamera.enabled = false;
        rightCamera.enabled = false;
        backCamera.enabled = false;
        worldCamera.enabled = false;

        //setting the rotation axis to false
        ringX.SetActive(false);
        ringY.SetActive(false);
        ringZ.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //checking if the true object can be created
        if (canCreateTrue) {
            //get name of the parent
            string name = temporaryNode.ToString();

            //get x coordinate
            float x = temporaryNode.returnX();
            float y = temporaryNode.returnY();
            float z = temporaryNode.returnZ();

            //Rotate on the Y-axis
            Quaternion rotation = Quaternion.Euler(temporaryNode.getRotationX(), temporaryNode.getRotationY(), temporaryNode.getRotationZ());

            //create the cube that will not be destroyed
            Vector3 coordinates = new Vector3(x, y, z);

            //check if the object is already created
            if (GameObject.Find(name + "(Clone)") != null)
            {
                //get the gameObject associated with the node, get the enemyObject
                GameObject enemy = temporaryNode.getObjectTrue();

                //giving the gameObject the name
                enemy.name = name;

                //spawn the parent object with specified coordinates
                Instantiate(enemy, coordinates, rotation);

                ////update the position
                //GameObject obj = GameObject.Find(name + "(Clone)");
                //obj.transform.position = coordinates;
            }
            else
            {
                //get the gameObject associated with the node, get the enemyObject
                GameObject enemy = temporaryNode.getObjectTrue();

                //giving the gameObject the name
                enemy.name = name;

                //spawn the parent object with specified coordinates
                Instantiate(enemy, coordinates, rotation);
            }

            //resetting the flag
            canCreate = false;

            //resetting the flag
            canCreateTrue = false;
        }

        //checking if the object can be created
        if (canCreate)
        {
            //get name of the parent
            string name = temporaryNode.ToString() + "temp";

            //get x coordinate
            float x = temporaryNode.returnX();
            float y = temporaryNode.returnY();
            float z = temporaryNode.returnZ();

            Quaternion rotation = Quaternion.Euler(temporaryNode.getRotationX(), temporaryNode.getRotationY(), temporaryNode.getRotationZ()); // Rotate on the Y-axis

            //create the cube that will not be destroyed
            Vector3 coordinates = new Vector3(x, y, z);

            //check if the object is already created
            if (GameObject.Find(name + "(Clone)") != null)
            {
                //get the gameObject associated with the node, get the enemyObject
                GameObject enemy = temporaryNode.getObjectEnemy();

                //giving the gameObject the name
                enemy.name = name;

                //spawn the parent object with specified coordinates
                Instantiate(enemy, coordinates, rotation);

                ////update the position
                //GameObject obj = GameObject.Find(name + "(Clone)");
                //obj.transform.position = coordinates;
            }
            else
            {
                //get the gameObject associated with the node, get the enemyObject
                GameObject enemy = temporaryNode.getObjectEnemy();

                //giving the gameObject the name
                enemy.name = name;

                //spawn the parent object with specified coordinates
                Instantiate(enemy, coordinates, rotation);
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
        minCameraButton.onClick.AddListener(() => maximiseUI());
        viewButton.onClick.AddListener(() => ViewSemanticNetwork());

        //Save/Load Buttons
        saveSceneButton.onClick.AddListener(() => saveScene());
        loadSceneButton.onClick.AddListener(() => loadScene());

        //Camera Buttons
        topCameraButton.onClick.AddListener(() => SwitchTopCamera());
        frontCameraButton.onClick.AddListener(() => SwitchFrontCamera());
        backCameraButton.onClick.AddListener(() => SwitchBackCamera());
        leftCameraButton.onClick.AddListener(() => SwitchLeftCamera());
        rightCameraButton.onClick.AddListener(() => SwitchRightCamera());
        worldCameraButton.onClick.AddListener(() => SwitchWorldCamera());

        //Camera Menu
        cameraButton.onClick.AddListener(() => getCameras());

        //Axis Button
        axisButton.onClick.AddListener(() => ChangeRotationAxisStatus());

        //Collision Box Button
        collisionBoxButton.onClick.AddListener(() => ChangeCollisionBoxStatus());
    }

    public void SwitchTopCamera()
    {
        frontCamera.tag = "Untagged";
        frontCamera.enabled = false;

        topCamera.enabled = true;
        topCamera.tag = "MainCamera";

        leftCamera.enabled = false;
        leftCamera.tag = "Untagged";

        rightCamera.enabled = false;
        rightCamera.tag = "Untagged";

        backCamera.enabled = false;
        backCamera.tag = "Untagged";

        worldCamera.enabled = false;
        worldCamera.tag = "Untagged";
    }

    public void SwitchFrontCamera()
    {
        frontCamera.tag = "MainCamera";
        frontCamera.enabled = true;

        topCamera.enabled = false;
        topCamera.tag = "Untagged";

        leftCamera.enabled = false;
        leftCamera.tag = "Untagged";

        rightCamera.enabled = false;
        rightCamera.tag = "Untagged";

        backCamera.enabled = false;
        backCamera.tag = "Untagged";

        worldCamera.enabled = false;
        worldCamera.tag = "Untagged";
    }

    public void ChangeRotationAxisStatus()
    {
        rotationAxis = !rotationAxis;

        if (rotationAxis)
        {
            ringX.SetActive(true);
            ringY.SetActive(true);
            ringZ.SetActive(true);
        }
        else {
            ringX.SetActive(false);
            ringY.SetActive(false);
            ringZ.SetActive(false);
        }
    }

    public void SwitchBackCamera()
    {
        frontCamera.enabled = false;
        frontCamera.tag = "Untagged";

        topCamera.enabled = false;
        topCamera.tag = "Untagged";

        leftCamera.enabled = false;
        leftCamera.tag = "Untagged";

        rightCamera.enabled = false;
        rightCamera.tag = "Untagged";

        backCamera.enabled = true;
        backCamera.tag = "MainCamera";

        worldCamera.enabled = false;
        worldCamera.tag = "Untagged";
    }

    public void SwitchLeftCamera()
    {
        frontCamera.enabled = false;
        frontCamera.tag = "Untagged";

        topCamera.enabled = false;
        topCamera.tag = "Untagged";

        leftCamera.enabled = true;
        leftCamera.tag = "MainCamera";

        rightCamera.enabled = false;
        rightCamera.tag = "Untagged";

        backCamera.enabled = false;
        backCamera.tag = "Untagged";

        worldCamera.enabled = false;
        worldCamera.tag = "Untagged";
    }

    public void SwitchRightCamera()
    {
        frontCamera.enabled = false;
        frontCamera.tag = "Untagged";

        topCamera.enabled = false;
        topCamera.tag = "Untagged";

        leftCamera.enabled = false;
        leftCamera.tag = "Untagged";

        rightCamera.enabled = true;
        rightCamera.tag = "MainCamera";

        backCamera.enabled = false;
        backCamera.tag = "Untagged";

        worldCamera.enabled = false;
        worldCamera.tag = "Untagged";
    }

    public void SwitchWorldCamera()
    {
        frontCamera.enabled = false;
        frontCamera.tag = "Untagged";

        topCamera.enabled = false;
        topCamera.tag = "Untagged";

        leftCamera.enabled = false;
        leftCamera.tag = "Untagged";

        rightCamera.enabled = false;
        rightCamera.tag = "Untagged";

        backCamera.enabled = false;
        backCamera.tag = "Untagged";

        worldCamera.enabled = true;
        worldCamera.tag = "MainCamera";
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
            //the sentence is split into words by the spaces
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

            //if the command is Move, flag to 3, 4, 5, 6
            else if (String.Equals(words[0], "Move", StringComparison.OrdinalIgnoreCase))
            {
                //check which type of move command it is
                //Move compound cube_1 to 2,0,0
                if (String.Equals(words[1], "compound", StringComparison.OrdinalIgnoreCase) && 
                    String.Equals(words[3], "to", StringComparison.OrdinalIgnoreCase))
                {
                    flag = 3;
                }
                //Move compound cube_1 on cube_2 or Move compound cube_1 under cube_2
                else if (String.Equals(words[1], "compound", StringComparison.OrdinalIgnoreCase))
                {
                    flag = 4;
                }
                //Move simple cube_1 to 2,0,0
                else if (String.Equals(words[1], "simple", StringComparison.OrdinalIgnoreCase) && 
                    String.Equals(words[3], "to", StringComparison.OrdinalIgnoreCase))
                {
                    flag = 5;
                }
                //Move simple cube_1 on cube_2 or Move simple cube_1 under cube_2
                else if (String.Equals(words[1], "simple", StringComparison.OrdinalIgnoreCase))
                { 
                    flag = 6;
                }
                else //if it is none of them there is an error
                { 
                    flag = -1;
                }
            }

            //if the command is Change, flag to 7
            else if (String.Equals(words[0], "Change", StringComparison.OrdinalIgnoreCase))
            {
                flag = 7;
            }

            //if the command is RotateY, flag to 8, 9
            else if (String.Equals(words[0], "RotateY", StringComparison.OrdinalIgnoreCase))
            {
                //check which type of move command it is
                if (String.Equals(words[1], "simple", StringComparison.OrdinalIgnoreCase))//RotateY simple table_1 by 90
                {
                    flag = 8;
                }
                else if (String.Equals(words[1], "compound", StringComparison.OrdinalIgnoreCase))//RotateY compound table_1 by 90
                {
                    flag = 9;
                }
                else {
                    //if it is none of them there is an error
                    flag = -1;
                }
            }

            //if the command is RotateY, flag to 10
            else if (String.Equals(words[0], "RotateX", StringComparison.OrdinalIgnoreCase))
            {
                flag = 10;
            }

            //if the command is RotateY, flag to 11
            else if (String.Equals(words[0], "RotateZ", StringComparison.OrdinalIgnoreCase))
            {
                flag = 11;
            }

            //if the command is Break, flag to 12
            else if (String.Equals(words[0], "Break", StringComparison.OrdinalIgnoreCase))
            {
                flag = 12;
            }

            //if the command is RotateY, flag to 11
            else if (String.Equals(words[0], "Reset", StringComparison.OrdinalIgnoreCase))
            {
                flag = 13;
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
            //if the command is an Add command, that means that it is a start node, ex: Add cube_1 at 2,0,0
            if (words.Length == 4 && flag == 0)
            {
                //getting the name of the object
                string parent = words[1];

                //getting the object and removing the number from it, Tree_1 -> Tree, 1
                string[] objectTypeWithNo = words[1].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //check that the object to be created exists
                if (objectList.Any(x => x.Equals(objectTypeWithNo[0], StringComparison.OrdinalIgnoreCase)))
                {

                    //getting the string of the coordinates, ex: 2,0,0
                    string coordinates = words[3];

                    //contains the coordinates 
                    string[] coordinatesXYZ = { };

                    //split the string into x, y and z coordnates
                    coordinatesXYZ = words[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    //float coordinates array
                    float[] coords = coordinatesXYZ.Select(float.Parse).ToArray();

                    //creating the parent node
                    Node parentNode = new Node(parent);

                    //setting the object type 
                    parentNode.setObjectType(objectTypeWithNo[0]);

                    //get reference to the gameObject of the node
                    GameObject obj = parentNode.getObjectTrue();

                    //get the dimensions of the gameObject
                    Vector3 size = obj.transform.localScale;

                    //given that the unity coordinates are calculated on the centre of the object,
                    //we need to add half of the object's height to the 'y' coordinate such that
                    //the bottom of the object touches the ground
                    coords[1] = coords[1] + ((size.y) / 2);

                    //set the initial rotation to 0
                    parentNode.setRotationX(0);
                    parentNode.setRotationY(0);
                    parentNode.setRotationZ(0);

                    //setting the coordinates
                    parentNode.setCoordinates(coords);

                    //check if there already exists an object with the same name
                    Boolean resultCheckName = graphObject.checkExist(TreeSet, parentNode);

                    if (resultCheckName == false) //object is unique
                    {
                        //check if it collides with any other object
                        StartCoroutine(checkSceneCollidersParent(parentNode));

                        yield return new WaitForSeconds(creationDelay);

                        //if it does not collide
                        if (found == true)
                        {
                            //reset the flag
                            found = false;

                            Boolean resultAddChildren = graphObject.addChildren(TreeSet, parentNode, null); //adding the first node to the tree - it has no children

                            //when adding a new object collisions are set to true
                            collisionBoxFlag = true;
                            CollisionBoxStatus();

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
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

            }

            //LIMITATION: CANNOT CREATE OBJECT ON OBJECT WITH ROTATION ON X AND Z AXIS

            //if the command is a Create command and consists of 5 words, that means that it is a single relationship, ex: Create touching Book_1 under Tree_1
            else if (words.Length == 5 && flag == 1)
            {
                //the parent node
                string parent = words[4];

                //creating the parent node
                Node parentNode = new Node(parent);

                //the child node
                string child = words[2];

                //creating the child node
                Node childNode = new Node(child);

                //the preposition
                string preposition = words[3];

                string[] objectTypeWithNo = child.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the spacing for the child
                childNode.setSpacing(words[1]);

                //setting the preposition for the child
                childNode.setPreposition(preposition);

                //setting the object type
                childNode.setObjectType(objectTypeWithNo[0]);

                //check that the parent gameObject exists
                if (GameObject.Find(parentNode.ToString() + "temp(Clone)")) {
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
                                //get reference to the parent object
                                GameObject parentObj = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                                //set the initial rotation on the Y axis to be the same as the parent
                                childNode.setRotationY(parentObj.transform.rotation.eulerAngles.y);

                                //initialise other rotations to 0
                                childNode.setRotationX(0);
                                childNode.setRotationZ(0);

                                //create a copy of the current Tree
                                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                                //add the new records
                                graphObject.addChildren(tempTree, parentNode, childNode);

                                //calculate the new coordinates
                                graphObject.setCoordinatesSimple(tempTree, parentNode, childNode);

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

                                            //when adding a new object collisions are set to true
                                            collisionBoxFlag = true;
                                            CollisionBoxStatus();
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
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }
            }

            //if the command is a Create command and consists of 6 words, that means that it is a double relationship, ex: Create Book_1 between Tree_1 and Man_1
            else if (words.Length == 6 && flag == 1)
            {
                //TO BE REWRITTEN

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

            //if the command is a Delete command and consists of 2 words, ex: Delete cube_1
            else if (words.Length == 2 && flag == 2)
            {
                //the first parent node
                string delete = words[1];

                //creating the first parent node
                Node delNode = new Node(delete);

                //check that the object to be deleted exists
                Boolean checkResult = checkExist(TreeSet, delNode);

                //if it exists
                if (checkResult)
                {
                    //calling the delete method
                    deleteNode(Tree, delNode);

                    //letting the frame pass
                    yield return new WaitForSeconds(creationDelay);
                }
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }
            }

            //if the command is a Move command and consists of 5 words, ex: Move compound cube_1 to 2,0,0
            else if (words.Length == 5 && flag == 3)
            {
                //the parent node
                string parent = words[2];

                //creating the parent node
                Node parentNode = new Node(parent);

                //check that the object to be moved exists
                Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                if (resultCheckExist)//if the parent object exists
                {
                    //getting the object and removing the number from it, Tree_1 -> Tree, 1
                    string[] objectTypeWithNo = parent.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                    //setting the object type 
                    parentNode.setObjectType(objectTypeWithNo[0]);

                    //getting the string of the coordinates, ex: 2,0,0
                    string coordinates = words[4];

                    //contains the coordinates 
                    string[] coordinatesXYZ = { };

                    //split the string into x, y and z coordinates
                    coordinatesXYZ = coordinates.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    //float coordinates array
                    float[] coords = coordinatesXYZ.Select(float.Parse).ToArray();

                    //get reference to the gameObject of the node
                    GameObject obj = parentNode.getObjectTrue();

                    //get the dimensions of the gameObject
                    Vector3 size = obj.transform.localScale;

                    //given that the unity coordinates are calculated on the centre of the object,
                    //we need to add half of the object's height to the 'y' coordinate such that
                    //the bottom of the object touches the ground
                    coords[1] = coords[1] + ((size.y) / 2);

                    //setting the new coordinates of the object
                    parentNode.setCoordinates(coords);
                    //create a temporary tree and recalculate the coordinates
                    //create a copy of the current Tree
                    IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                    //break relationship for the parentNode with its parents
                    tempTree = removeNodeAsChild(tempTree, parentNode);

                    //change the coordinates of the parent node in the tree datastructure
                    changeTreeCoordinates(tempTree, parentNode);

                    //List to store all the nodes involved
                    List<Node> nodesToMove = new List<Node>();

                    //add the parentNode as the start
                    foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                    {
                        //first thing to do is spawn the parent
                        if (entry.Key.ToString().Equals(parentNode.ToString()))
                        {
                            Node newNodeCopy = entry.Key.CopyDeep();
                            nodesToMove.Add(newNodeCopy);
                        }
                    }

                    //get all the objects involved through iteration-----

                    //start with the children of the parentNode
                    Node[] temp = null;

                    foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                    {
                        //if the current node is equal to the parentNode
                        if (entry.Key.ToString().Equals(parentNode.ToString()))
                        {
                            //intialise the temp
                            temp = tempTree[entry.Key];
                        }
                    }

                    //getting the first nodes from the array
                    foreach (Node tempNode in temp)
                    {
                        Node newNodeCopy = tempNode.CopyDeep();
                        nodesToMove.Add(newNodeCopy);
                    }

                    //list used to compare and check if any new nodes have been added
                    List<Node> nodesToMoveCopy = new List<Node>(nodesToMove);

                    Boolean repeat = true;

                    while (repeat)
                    {
                        foreach (Node tNode in nodesToMove)
                        {
                            Node[] temporary = null;

                            //get the children and add them to the list
                            foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                            {
                                //if the current node is equal to the parentNode
                                if (entry.Key.ToString().Equals(tNode.ToString()))
                                {
                                    //intialise the temp
                                    temporary = tempTree[entry.Key];
                                }
                            }

                            //check if it has children
                            foreach (Node tempNode in temporary)
                            {
                                //check if the nodes are not already in the list
                                Boolean checkNode = nodesToMove.Any(x => x.ToString().Equals(tempNode.ToString()));


                                if (checkNode == false)
                                {
                                    Node newNodeCopy = tempNode.CopyDeep();
                                    nodesToMoveCopy.Add(newNodeCopy);//add the new children (if any)
                                }

                            }
                        }

                        if (nodesToMove.Count == nodesToMoveCopy.Count)
                        {
                            repeat = false;
                        }
                        else
                        {
                            //update the list with the new Nodes
                            nodesToMove = new List<Node>(nodesToMoveCopy);
                            repeat = true;
                        }
                    }

                    //update the list of objects that are exempt from collisions
                    foreach (Node node in nodesToMove) {
                        listOfObjects.Add(node.ToString());
                    }

                    //calculate the coordinates of the gameobjects
                    nodesToMove = setCoordinatesCompound(tempTree, nodesToMove);

                    //check that the new coordinates are not the same as the old coordinates
                    GameObject objOld = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //current position of the object
                    Vector3 currentPosition = objOld.transform.position;

                    float x = parentNode.returnX();
                    float y = parentNode.returnY();
                    float z = parentNode.returnZ();

                    if (currentPosition != new Vector3(x, y, z))
                    { //if they are not equal
                      //check that the object to be moved exists
                      //Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                        if (resultCheckExist)//if the object exists
                        {
                            Boolean create = true;

                            //loop through the nodes to be moved
                            foreach (Node node in nodesToMove)
                            {
                                Node nodeCopy = node.CopyDeep();

                                //change name as it is a temporary duplicate
                                nodeCopy.setValue(node.ToString() + "Copy");

                                //check that the nodes can be moved
                                StartCoroutine(checkSceneCollidersParent(nodeCopy));

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
                                foreach (Node node in nodesToMove)
                                {
                                    GameObject objToDelete = GameObject.Find(node.ToString() + "Copytemp(Clone)");

                                    //destroying the object
                                    Destroy(objToDelete);

                                    yield return new WaitForSeconds(creationDelay);

                                    ////find the object
                                    //String originalObject = node.ToString().Replace("Copy", "");

                                    GameObject objToUpdate = GameObject.Find(node.ToString() + "temp(Clone)");
                                    objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                    yield return new WaitForSeconds(creationDelay);

                                    //find the created
                                    if (GameObject.Find(node.ToString() + "(Clone)"))
                                    {
                                        GameObject objToMove = GameObject.Find(node.ToString() + "(Clone)");
                                        objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                        yield return new WaitForSeconds(creationDelay);
                                    }
                                }

                                //update the Tree
                                TreeSet = tempTree;
                            }
                            else
                            {
                                //remember to destroy the nodes
                                //delete the copies of the objects
                                foreach (Node node in nodesToMove)
                                {
                                    GameObject objToDelete = GameObject.Find(node.ToString() + "Copytemp(Clone)");

                                    //destroying the object
                                    Destroy(objToDelete);

                                    yield return new WaitForSeconds(creationDelay);
                                }


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
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Move command and consists of 6 words, ex: Move compound cube_1 on cube_3 0.5
            else if (words.Length == 6 && flag == 4)
            {
                //rotations stay the same

                Boolean collisions = false;

                //the parent node
                string parent = words[2];

                //creating the parent node
                Node parentNode = new Node(parent);

                //storing the preposition
                string preposition = words[3];

                //setting the preposition
                parentNode.setPreposition(preposition);

                //setting the spacing
                parentNode.setSpacing(words[5]);

                string[] objectTypeWithNo = parent.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the object type
                parentNode.setObjectType(objectTypeWithNo[0]);

                //the name of the node that will be the new parent
                string newParent = words[4];

                //creating the new parent node
                Node newParentNode = new Node(newParent);

                string[] objectTypeWithNo1 = parent.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the object type
                newParentNode.setObjectType(objectTypeWithNo1[0]);

                //create a temporary tree and recalculate the coordinates
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //check that the object to be moved exists
                Boolean resultCheckExistParent = checkExist(TreeSet, parentNode);

                if (resultCheckExistParent)//if the parent object exists
                {
                    //check that the object to be moved to exists
                    Boolean resultCheckExistParentNew = checkExist(TreeSet, newParentNode);

                    if (resultCheckExistParentNew)//if the new parent object exists
                    {
                        //break relations with other nodes for the parentNode
                        tempTree = removeNodeAsChild(tempTree, parentNode);

                        //if the newParentNode is a child of parentNode, we break it
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //if the current node is equal to the parentNode
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                Node[] children = tempTree[entry.Key];

                                //if the newParentNode is a child of the tempTree
                                if (children.Any(x => x.ToString().Equals(newParentNode.ToString()))){
                                    //break relations with other nodes for the parentNode
                                    tempTree = removeNodeAsChild(tempTree, newParentNode);
                                }
                            }
                        }

                        //add the new relationships, the parentNode coordinates have changed
                        tempTree = changeTreeRelations(tempTree, newParentNode, parentNode);

                        //List to store all the nodes involved
                        List<Node> nodesToMove = new List<Node>();

                        //add the parentNode as the start
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //first thing to do is spawn the parent
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                Node newNodeCopy = entry.Key.CopyDeep();
                                nodesToMove.Add(newNodeCopy);
                            }
                        }

                        //get all the objects involved through iteration-----

                        //start with the children of the parentNode
                        Node[] temp = null;

                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //if the current node is equal to the parentNode
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                //intialise the temp
                                temp = tempTree[entry.Key];
                            }
                        }

                        //getting the first nodes from the array
                        foreach (Node tempNode in temp)
                        {
                            Node newNodeCopy = tempNode.CopyDeep();
                            nodesToMove.Add(newNodeCopy);
                        }

                        //list used to compare and check if any new nodes have been added
                        List<Node> nodesToMoveCopy = new List<Node>(nodesToMove);

                        Boolean repeat = true;

                        while (repeat)
                        {
                            foreach (Node tNode in nodesToMove)
                            {
                                Node[] temporary = null;

                                //get the children and add them to the list
                                foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                                {
                                    //if the current node is equal to the parentNode
                                    if (entry.Key.ToString().Equals(tNode.ToString()))
                                    {
                                        //intialise the temp
                                        temporary = tempTree[entry.Key];
                                    }
                                }

                                //check if it has children
                                foreach (Node tempNode in temporary)
                                {
                                    //check if the nodes are not already in the list
                                    Boolean checkNode = nodesToMove.Any(x => x.ToString().Equals(tempNode.ToString()));


                                    if (checkNode == false)
                                    {
                                        Node newNodeCopy = tempNode.CopyDeep();
                                        nodesToMoveCopy.Add(newNodeCopy);//add the new children (if any)
                                    }

                                }
                            }

                            if (nodesToMove.Count == nodesToMoveCopy.Count)
                            {
                                repeat = false;
                            }
                            else
                            {
                                //update the list with the new Nodes
                                nodesToMove = new List<Node>(nodesToMoveCopy);
                                repeat = true;
                            }
                        }

                        //update the list of objects that are exempt from collisions
                        foreach (Node node in nodesToMove)
                        {
                            listOfObjects.Add(node.ToString());
                        }

                        //calculate the coordinates of the gameobjects
                        nodesToMove = setCoordinatesCompound(tempTree, nodesToMove);

                        //check that the new coordinates are not the same as the old coordinates
                        GameObject objOld = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                        //current position of the object
                        Vector3 currentPosition = objOld.transform.position;

                        float x = parentNode.returnX();
                        float y = parentNode.returnY();
                        float z = parentNode.returnZ();

                        if (currentPosition != new Vector3(x, y, z))
                        { //if they are not equal
                          //check that the object to be moved exists
                          Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                            if (resultCheckExist)//if the object exists
                            {
                                Boolean create = true;

                                //loop through the nodes to be moved
                                foreach (Node node in nodesToMove)
                                {
                                    Node nodeCopy = node.CopyDeep();

                                    //change name as it is a temporary duplicate
                                    nodeCopy.setValue(node.ToString() + "Copy");

                                    //check that the nodes can be moved
                                    StartCoroutine(checkSceneCollidersParent(nodeCopy));

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
                                    foreach (Node node in nodesToMove)
                                    {
                                        GameObject objToDelete = GameObject.Find(node.ToString() + "Copytemp(Clone)");

                                        //destroying the object
                                        Destroy(objToDelete);

                                        yield return new WaitForSeconds(creationDelay);

                                        ////find the object
                                        //String originalObject = node.ToString().Replace("Copy", "");

                                        GameObject objToUpdate = GameObject.Find(node.ToString() + "temp(Clone)");
                                        objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                        yield return new WaitForSeconds(creationDelay);

                                        //find the created
                                        if (GameObject.Find(node.ToString() + "(Clone)"))
                                        {
                                            GameObject objToMove = GameObject.Find(node.ToString() + "(Clone)");
                                            objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                            yield return new WaitForSeconds(creationDelay);
                                        }
                                    }

                                    //update the Tree
                                    TreeSet = tempTree;
                                }
                                else
                                {
                                    //remember to destroy the nodes
                                    //delete the copies of the objects
                                    foreach (Node node in nodesToMove)
                                    {
                                        GameObject objToDelete = GameObject.Find(node.ToString() + "Copytemp(Clone)");

                                        //destroying the object
                                        Destroy(objToDelete);

                                        yield return new WaitForSeconds(creationDelay);
                                    }

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
                    } 
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Move command and consists of 6 words, ex: Move compound cube_1 left by 2
            else if (words.Length == 6 && flag == 5)
            {
                //rotations stay the same

                Boolean collisions = false;

                //the parent node
                string parent = words[2];

                //creating the parent node
                Node parentNode = new Node(parent);

                //storing the preposition
                string preposition = words[3];

                //setting the preposition
                parentNode.setPreposition(preposition);

                //setting the spacing
                parentNode.setSpacing(words[5]);

                string[] objectTypeWithNo = parent.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the object type
                parentNode.setObjectType(objectTypeWithNo[0]);

                //the name of the node that will be the new parent
                string newParent = words[4];

                //creating the new parent node
                Node newParentNode = new Node(newParent);

                string[] objectTypeWithNo1 = parent.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the object type
                newParentNode.setObjectType(objectTypeWithNo1[0]);

                //create a temporary tree and recalculate the coordinates
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //check that the object to be moved exists
                Boolean resultCheckExistParent = checkExist(TreeSet, parentNode);

                if (resultCheckExistParent)//if the parent object exists
                {
                    //check that the object to be moved to exists
                    Boolean resultCheckExistParentNew = checkExist(TreeSet, newParentNode);

                    if (resultCheckExistParentNew)//if the new parent object exists
                    {
                        //break relations with other nodes for the parentNode
                        tempTree = removeNodeAsChild(tempTree, parentNode);

                        //if the newParentNode is a child of parentNode, we break it
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //if the current node is equal to the parentNode
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                Node[] children = tempTree[entry.Key];

                                //if the newParentNode is a child of the tempTree
                                if (children.Any(x => x.ToString().Equals(newParentNode.ToString())))
                                {
                                    //break relations with other nodes for the parentNode
                                    tempTree = removeNodeAsChild(tempTree, newParentNode);
                                }
                            }
                        }

                        //add the new relationships, the parentNode coordinates have changed
                        tempTree = changeTreeRelations(tempTree, newParentNode, parentNode);

                        //List to store all the nodes involved
                        List<Node> nodesToMove = new List<Node>();

                        //add the parentNode as the start
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //first thing to do is spawn the parent
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                Node newNodeCopy = entry.Key.CopyDeep();
                                nodesToMove.Add(newNodeCopy);
                            }
                        }

                        //get all the objects involved through iteration-----

                        //start with the children of the parentNode
                        Node[] temp = null;

                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //if the current node is equal to the parentNode
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                //intialise the temp
                                temp = tempTree[entry.Key];
                            }
                        }

                        //getting the first nodes from the array
                        foreach (Node tempNode in temp)
                        {
                            Node newNodeCopy = tempNode.CopyDeep();
                            nodesToMove.Add(newNodeCopy);
                        }

                        //list used to compare and check if any new nodes have been added
                        List<Node> nodesToMoveCopy = new List<Node>(nodesToMove);

                        Boolean repeat = true;

                        while (repeat)
                        {
                            foreach (Node tNode in nodesToMove)
                            {
                                Node[] temporary = null;

                                //get the children and add them to the list
                                foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                                {
                                    //if the current node is equal to the parentNode
                                    if (entry.Key.ToString().Equals(tNode.ToString()))
                                    {
                                        //intialise the temp
                                        temporary = tempTree[entry.Key];
                                    }
                                }

                                //check if it has children
                                foreach (Node tempNode in temporary)
                                {
                                    //check if the nodes are not already in the list
                                    Boolean checkNode = nodesToMove.Any(x => x.ToString().Equals(tempNode.ToString()));


                                    if (checkNode == false)
                                    {
                                        Node newNodeCopy = tempNode.CopyDeep();
                                        nodesToMoveCopy.Add(newNodeCopy);//add the new children (if any)
                                    }

                                }
                            }

                            if (nodesToMove.Count == nodesToMoveCopy.Count)
                            {
                                repeat = false;
                            }
                            else
                            {
                                //update the list with the new Nodes
                                nodesToMove = new List<Node>(nodesToMoveCopy);
                                repeat = true;
                            }
                        }

                        //update the list of objects that are exempt from collisions
                        foreach (Node node in nodesToMove)
                        {
                            listOfObjects.Add(node.ToString());
                        }

                        //calculate the coordinates of the gameobjects
                        nodesToMove = setCoordinatesCompound(tempTree, nodesToMove);

                        //check that the new coordinates are not the same as the old coordinates
                        GameObject objOld = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                        //current position of the object
                        Vector3 currentPosition = objOld.transform.position;

                        float x = parentNode.returnX();
                        float y = parentNode.returnY();
                        float z = parentNode.returnZ();

                        if (currentPosition != new Vector3(x, y, z))
                        { //if they are not equal
                          //check that the object to be moved exists
                            Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                            if (resultCheckExist)//if the object exists
                            {
                                Boolean create = true;

                                //loop through the nodes to be moved
                                foreach (Node node in nodesToMove)
                                {
                                    Node nodeCopy = node.CopyDeep();

                                    //change name as it is a temporary duplicate
                                    nodeCopy.setValue(node.ToString() + "Copy");

                                    //check that the nodes can be moved
                                    StartCoroutine(checkSceneCollidersParent(nodeCopy));

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
                                    foreach (Node node in nodesToMove)
                                    {
                                        GameObject objToDelete = GameObject.Find(node.ToString() + "Copytemp(Clone)");

                                        //destroying the object
                                        Destroy(objToDelete);

                                        yield return new WaitForSeconds(creationDelay);

                                        ////find the object
                                        //String originalObject = node.ToString().Replace("Copy", "");

                                        GameObject objToUpdate = GameObject.Find(node.ToString() + "temp(Clone)");
                                        objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                        yield return new WaitForSeconds(creationDelay);

                                        //find the created
                                        if (GameObject.Find(node.ToString() + "(Clone)"))
                                        {
                                            GameObject objToMove = GameObject.Find(node.ToString() + "(Clone)");
                                            objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                            yield return new WaitForSeconds(creationDelay);
                                        }
                                    }

                                    //update the Tree
                                    TreeSet = tempTree;
                                }
                                else
                                {
                                    //remember to destroy the nodes
                                    //delete the copies of the objects
                                    foreach (Node node in nodesToMove)
                                    {
                                        GameObject objToDelete = GameObject.Find(node.ToString() + "Copytemp(Clone)");

                                        //destroying the object
                                        Destroy(objToDelete);

                                        yield return new WaitForSeconds(creationDelay);
                                    }

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
                    }
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Move simple command and consists of 5 words, ex: Move simple cube_1 to 2,0,0
            else if (words.Length == 5 && flag == 5)
            {

                //the parent node
                string parent = words[2];

                //creating the parent node
                Node parentNode = new Node(parent);

                //getting the object and removing the number from it, Tree_1 -> Tree, 1
                string[] objectTypeWithNo = words[2].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the object type 
                parentNode.setObjectType(objectTypeWithNo[0]);

                //getting the string of the coordinates, ex: 2,0,0
                string coordinates = words[4];

                //contains the coordinates 
                string[] coordinatesXYZ = { };

                //split the string into x, y and z coordinates
                coordinatesXYZ = coordinates.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //float coordinates array
                float[] coords = coordinatesXYZ.Select(float.Parse).ToArray();

                //copy of the node
                Node copyNode = null;

                //check the rotations of the gameobject
                foreach (KeyValuePair<Node, Node[]> entry in TreeSet)
                {
                    //if the current node is equal to the required node
                    if (entry.Key.ToString().Equals(parentNode.ToString()))
                    {
                        copyNode = entry.Key.CopyDeep();
                    }
                }

                if (copyNode.getRotationX() != 0 || copyNode.getRotationZ() != 0)
                {
                    coords[1] = copyNode.returnY();
                }
                else
                {
                    //get reference to the gameObject of the node
                    GameObject obj = parentNode.getObjectTrue();

                    //get the dimensions of the gameObject
                    Vector3 size = obj.transform.localScale;

                    //given that the unity coordinates are calculated on the centre of the object,
                    //we need to add half of the object's height to the 'y' coordinate such that
                    //the bottom of the object touches the ground
                    coords[1] = coords[1] + ((size.y) / 2);
                }

                //setting the coordinates
                parentNode.setCoordinates(coords);

                //create a temporary tree and recalculate the coordinates
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //check that the object to be moved exists
                Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                if (resultCheckExist)//if the parent object exists
                {
                    //break relationship with parent
                    tempTree = removeNodeAsChild(tempTree, parentNode);

                    //remove children since it is a simple
                    tempTree = removeChildren(tempTree, parentNode);

                    //change the coordinates of the parent
                    changeTreeCoordinates(tempTree, parentNode);

                    Node nodeReference = null;

                    //add the parentNode as the start
                    foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                    {
                        //first thing to do is spawn the parent
                        if (entry.Key.ToString().Equals(parentNode.ToString()))
                        {
                            nodeReference = entry.Key.Copy();
                        }
                    }

                    //update the list of objects that are exempt from collisions
                    listOfObjects.Add(nodeReference.ToString());

                    //check that the new coordinates are not the same as the old coordinates
                    GameObject objOld = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //current position of the object
                    Vector3 currentPosition = objOld.transform.position;

                    float x = parentNode.returnX();
                    float y = parentNode.returnY();
                    float z = parentNode.returnZ();


                    if (currentPosition != new Vector3(x, y, z))
                    { //if they are not equal
                      ////check that the object to be moved exists
                      //Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                        if (resultCheckExist)//if the object exists
                        {
                            Boolean create = true;

                            //change name as it is a temporary duplicate
                            nodeReference.setValue(nodeReference.ToString() + "Copy");

                            //check that the nodes can be moved
                            StartCoroutine(checkSceneCollidersParent(nodeReference));

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
                            }

                            //if there are no collisions at all
                            if (create == true)
                            {
                                //the objects can be moved

                                GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                //destroying the object
                                Destroy(objToDelete);

                                yield return new WaitForSeconds(creationDelay);

                                //find the object
                                String originalObject = nodeReference.ToString().Replace("Copy", "");

                                GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                objToUpdate.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                yield return new WaitForSeconds(creationDelay);

                                //find the created
                                if (GameObject.Find(originalObject + "(Clone)"))
                                {
                                    GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                    objToMove.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                    yield return new WaitForSeconds(creationDelay);
                                }

                                //update the Tree
                                TreeSet = tempTree;
                            }
                            else
                            {
                                //remember to destroy the nodes
                                GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                //destroying the object
                                Destroy(objToDelete);

                                yield return new WaitForSeconds(creationDelay);

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
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Move simple command and consists of 5 words, ex: Move simple cube_1 on cube_3
            else if (words.Length == 5 && flag == 6)
            {
                //the parent node
                string parent = words[2];

                //creating the parent node
                Node parentNode = new Node(parent);

                //storing the preposition
                string preposition = words[3];

                //setting the preposition
                parentNode.setPreposition(preposition);

                //the name of the node that will be the new parent
                string newParent = words[4];

                //creating the new parent node
                Node newParentNode = new Node(newParent);

                //create a temporary tree and recalculate the coordinates
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //check that the object to be moved exists
                Boolean resultCheckExistParent = checkExist(TreeSet, parentNode);

                if (resultCheckExistParent)//if the parent object exists
                {
                    //check that the object to be moved to exists
                    Boolean resultCheckExistParentNew = checkExist(TreeSet, newParentNode);

                    if (resultCheckExistParentNew)//if the new parent object exists
                    {
                        //break relations with other nodes for the parentNode
                        tempTree = removeNodeAsChild(tempTree, parentNode);

                        //remove children since it is a simple
                        tempTree = removeChildren(tempTree, parentNode);

                        //add the new records and change coordinates for the parent
                        tempTree = changeTreeRelations(tempTree, newParentNode, parentNode);

                        Node nodeReference = null;

                        //add the parentNode as the start
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //first thing to do is spawn the parent
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                nodeReference = entry.Key.Copy();
                            }
                        }

                        //update the list of objects that are exempt from collisions
                        listOfObjects.Add(nodeReference.ToString());

                        //check that the new coordinates are not the same as the old coordinates
                        GameObject objOld = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                        //current position of the object
                        Vector3 currentPosition = objOld.transform.position;

                        float x = parentNode.returnX();
                        float y = parentNode.returnY();
                        float z = parentNode.returnZ();


                        if (currentPosition != new Vector3(x, y, z))
                        { //if they are not equal
                          ////check that the object to be moved exists
                          //Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                            if (resultCheckExistParent)//if the object exists
                            {
                                Boolean create = true;

                                //change name as it is a temporary duplicate
                                nodeReference.setValue(nodeReference.ToString() + "Copy");

                                //check that the nodes can be moved
                                StartCoroutine(checkSceneCollidersParent(nodeReference));

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
                                }

                                //if there are no collisions at all
                                if (create == true)
                                {
                                    //the objects can be moved

                                    GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                    //destroying the object
                                    Destroy(objToDelete);

                                    yield return new WaitForSeconds(creationDelay);

                                    //find the object
                                    String originalObject = nodeReference.ToString().Replace("Copy", "");

                                    GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                    objToUpdate.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                    yield return new WaitForSeconds(creationDelay);

                                    //find the created
                                    if (GameObject.Find(originalObject + "(Clone)"))
                                    {
                                        GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                        objToMove.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                        yield return new WaitForSeconds(creationDelay);
                                    }

                                    //update the Tree
                                    TreeSet = tempTree;
                                }
                                else
                                {
                                    //remember to destroy the nodes
                                    GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                    //destroying the object
                                    Destroy(objToDelete);

                                    yield return new WaitForSeconds(creationDelay);

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
                    }
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }



            //if the command is a Change color command and consists of 4 words, ex: Change cube_1 to red
            else if (words.Length == 4 && flag == 7)
            {
                //the object to change
                string objectToChange = words[1];

                //the new color of the object
                string color = words[3];

                // Next, create a new Color variable
                Color newColor;

                //check if it exists
                if (GameObject.Find(objectToChange + "(Clone)"))
                {

                    // First, get a reference to the GameObject you want to change the color of
                    GameObject myObject = GameObject.Find(objectToChange + "(Clone)");

                    GameObject childObject = myObject.transform.GetChild(0).gameObject;

                    // Next, get a reference to the object's Renderer component
                    Renderer myRenderer = childObject.GetComponent<Renderer>();

                    //check if the color is a specific color
                    if (colorList.Any(x => x.Equals(color, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (color.Equals("wood_dark", StringComparison.OrdinalIgnoreCase))
                        {
                            //changing the material
                            myRenderer.material = WoodDark;
                        }
                        else if (color.Equals("wood_light", StringComparison.OrdinalIgnoreCase))
                        {
                            //changing the material
                            myRenderer.material = WoodLight;
                        }
                        else if (color.Equals("metal_dark", StringComparison.OrdinalIgnoreCase))
                        {
                            //changing the material
                            myRenderer.material = MetalDark;
                        }
                        else if (color.Equals("metal_light", StringComparison.OrdinalIgnoreCase))
                        {
                            //changing the material
                            myRenderer.material = MetalLight;
                        }
                        else if (color.Equals("fabric", StringComparison.OrdinalIgnoreCase))
                        {
                            //changing the material
                            myRenderer.material = Fabric;
                        }
                    }
                    // then, use the ColorUtility class to convert the string to a Color value
                    else if (ColorUtility.TryParseHtmlString(color, out newColor))
                    {
                        // If the string was successfully parsed, you can now use the newColor value to set the color of your GameObject
                        UtilityMaterial.color = newColor;
                        myRenderer.material = UtilityMaterial;
                    }
                    else
                    {
                        //unable to find the color
                        placeholder.text = "Incorrect input!";
                        placeholder.color = Color.red;
                    }
                }
                else
                {
                    placeholder.text = "Incorrect input!";
                    placeholder.color = Color.red;
                }
            }

            //if the command is a (Simple) Rotate command, ex: RotateY simple table_1 by 90
            else if (words.Length == 5 && flag == 8)
            {
                //check on which axis it is being rotated

                //the parent node name
                string parent = words[2];

                //Add to the list of exempt objects
                listOfObjects.Add(parent);

                //creating the parent node
                Node parentNode = new Node(parent);

                //check if the object to be rotated exists
                Boolean checkObjectExist = checkExist(Tree, parentNode);

                //if the object exists
                if (checkObjectExist)
                {
                    //getting the rotation ex: 180
                    string rotation = words[4];

                    //normalise the rotation
                    float rotationInteger = float.Parse(rotation) % 360;

                    //get the current rotation of the gameobject
                    GameObject obj = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //current rotation of the object
                    Quaternion currentRotationQuaternion = obj.transform.rotation;

                    //get the rotation on the y-axis only
                    float currentRotation = currentRotationQuaternion.eulerAngles.y;

                    //normalise the current rotation, 450 same as 90
                    currentRotation = currentRotation % 360;

                    //rotation to be
                    float futureRotation = (currentRotation + rotationInteger) % 360;

                    //check if they are the same
                    if (currentRotation != futureRotation) {

                        //setting the rotation on the Y axis
                        parentNode.setRotationY(futureRotation);

                        //keep the other rotations the same
                        parentNode.setRotationX(currentRotationQuaternion.eulerAngles.x);
                        parentNode.setRotationZ(currentRotationQuaternion.eulerAngles.z);

                        //create a temporary tree and recalculate the rotation
                        //create a copy of the current Tree
                        IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                        //if the node has children, remove the relationships
                        tempTree = removeChildren(tempTree, parentNode);

                        //change the rotation of the parentNode in the Tree datastructure
                        changeTreeRotationsY(tempTree, parentNode);

                        //get the node from the temptree with coordinates, object type etc.
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //check the name of each node
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                //create a copy and add it to the list
                                parentNode = entry.Key.CopyDeep();
                            }
                        }

                        //flag to show if the rotation can be done or not
                        Boolean create = true;

                        //change name as it is a temporary duplicate
                        parentNode.setValue(parentNode.ToString() + "Copy");

                        //check that the nodes can be moved
                        StartCoroutine(checkSceneCollidersParent(parentNode));

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
                        }

                        //if there are no collisions at all
                        if (create == true)
                        {
                            //the objects can be moved
                            GameObject objToDelete = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //destroying the object
                            Destroy(objToDelete);

                            yield return new WaitForSeconds(creationDelay);

                            //find the object
                            String originalObject = parentNode.ToString().Replace("Copy", "");

                            GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                            objToUpdate.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());

                            yield return new WaitForSeconds(creationDelay);

                            //find the created object
                            if (GameObject.Find(originalObject + "(Clone)"))
                            {
                                GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                objToMove.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());

                                yield return new WaitForSeconds(creationDelay);
                            }

                            //update the Tree
                            TreeSet = tempTree;
                        }
                        else
                        {
                            //destroy the copy gameobject
                            GameObject objToDelete = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //destroying the object
                            Destroy(objToDelete);

                            yield return new WaitForSeconds(creationDelay);

                            placeholder.text = "Could not add node!";
                            placeholder.color = Color.red;
                        }
                    }
                }
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

                //reset the object list
                listOfObjects.Clear();
            }

            //if the command is a (Compound) Rotate command, ex: RotateY compound table_1 by 180
            else if (words.Length == 5 && flag == 9)
            {
                //check on which axis it is being rotated
                Boolean collisions = false;

                //the parent node name
                string parent = words[2];

                //creating the parent node
                Node parentNode = new Node(parent);

                //check if the object to be rotated exists
                Boolean checkObjectExist = checkExist(Tree, parentNode);

                //if the object exists
                if (checkObjectExist)
                {
                    //getting the rotation ex: 180
                    string rotation = words[4];

                    //normalise the rotation
                    float rotationInteger = float.Parse(rotation) % 360;

                    //get the current rotation of the gameobject
                    GameObject obj = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //current rotation of the object
                    Quaternion currentRotationQuaternion = obj.transform.rotation;

                    //get the rotation on the y-axis only
                    float currentRotation = currentRotationQuaternion.eulerAngles.y;

                    //normalise the current rotation, 450 same as 90
                    currentRotation = currentRotation % 360;

                    //rotation to be
                    float futureRotation = (currentRotation + rotationInteger) % 360;

                    //check if they are the same
                    if (currentRotation != futureRotation)
                    {
                        //setting the rotation on the Y axis
                        parentNode.setRotationY(futureRotation);

                        //setting the other rotations as they were
                        parentNode.setRotationX(currentRotationQuaternion.eulerAngles.x);
                        parentNode.setRotationZ(currentRotationQuaternion.eulerAngles.z);

                        //create a temporary tree and recalculate the rotation
                        //create a copy of the current Tree
                        IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                        //change the rotation of the parentNode
                        changeTreeRotationsY(tempTree, parentNode);

                        //List to store all the nodes involved
                        List<Node> nodesToRotate = new List<Node>();

                        //add the parentNode as the start
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //first thing to do is spawn the parent
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                Node newNodeCopy = entry.Key.CopyDeep();
                                nodesToRotate.Add(newNodeCopy);
                            }
                        }

                        //get all the objects involved through iteration-----

                        //start with the children of the parentNode
                        Node[] temp = null;

                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //if the current node is equal to the parentNode
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                //intialise the temp
                                temp = tempTree[entry.Key];
                            }
                        }

                        //getting the first nodes from the array
                        foreach (Node tempNode in temp)
                        {
                            Node newNodeCopy = tempNode.CopyDeep();
                            nodesToRotate.Add(newNodeCopy);
                        }

                        //list used to compare and check if any new nodes have been added
                        List<Node> nodesToRotateCopy = new List<Node>(nodesToRotate);

                        Boolean repeat = true;

                        while (repeat)
                        {
                            foreach (Node tNode in nodesToRotate)
                            {
                                //get the children and add them to the list
                                Node[] temporary = tempTree[tNode];

                                //check if it has children
                                foreach (Node tempNode in temporary)
                                {
                                    //check if the nodes are not already in the list
                                    Boolean checkNode = nodesToRotate.Contains(tempNode);

                                    if (checkNode == false)
                                    {
                                        Node newNodeCopy = tempNode.CopyDeep();
                                        nodesToRotateCopy.Add(newNodeCopy);//add the new children (if any)
                                    }

                                }
                            }

                            if (nodesToRotate.Count == nodesToRotateCopy.Count)
                            {
                                repeat = false;
                            }
                            else
                            {
                                //update the list with the new Nodes
                                nodesToRotate = new List<Node>(nodesToRotateCopy);
                                repeat = true;
                            }
                        }

                        //update the list of objects that are exempt from collisions
                        foreach (Node node in nodesToRotate)
                        {
                            listOfObjects.Add(node.ToString());
                        }

                        Boolean create = true;

                        //loop through the nodes to be rotated
                        foreach (Node node in nodesToRotate)
                        {
                            //get the children of the node, so their coords and rotations are updated
                            Node[] children = null;

                            //get the children and add them to the list
                            foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                            {
                                //if the current node is equal to the parentNode
                                if (entry.Key.ToString().Equals(node.ToString()))
                                {
                                    //intialise the temp
                                    children = tempTree[entry.Key];
                                }
                            }

                            //copy of the node
                            Node nodeCopy = node.CopyDeep();

                            //change name as it is a temporary duplicate
                            node.setValue(node.ToString() + "Copy");

                            //check that the nodes can be moved
                            StartCoroutine(checkSceneCollidersParent(node));

                            //handing control to Update()
                            yield return new WaitForSeconds(creationDelay);

                            //the parent object is created

                            if (found == true)//the node can be rotated
                            {
                                found = false;//reset the found flag

                                create = true;//boolean to see if there are collisions in all children

                                //iterating through the children so that they are updated
                                foreach (Node childNode in children)
                                {
                                    //for each node, calculate the rotations and coordinates of their children
                                    calculatePrepositionCoordinatesAndRotations(nodeCopy, childNode, rotationInteger);

                                    //update the nodes in nodesToRotate
                                    foreach (Node nodeToChange in nodesToRotate) {
                                        //check if they have the same key
                                        if (nodeToChange.ToString().Equals(childNode.ToString())) {
                                            //update the nodes
                                            nodeToChange.setCoordinates(childNode.getCoordinatesFloat());
                                            nodeToChange.setRotationY(childNode.getRotationY());
                                        }
                                    }
                                }
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
                            foreach (Node node in nodesToRotate)
                            {
                                GameObject objToDelete = GameObject.Find(node.ToString() + "temp(Clone)");

                                //destroying the object
                                Destroy(objToDelete);

                                yield return new WaitForSeconds(creationDelay);

                                //find the object
                                String originalObject = node.ToString().Replace("Copy", "");

                                GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                objToUpdate.transform.rotation = Quaternion.Euler(node.getRotationX(), node.getRotationY(), node.getRotationZ());
                                objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                yield return new WaitForSeconds(creationDelay);

                                //find the created
                                if (GameObject.Find(originalObject + "(Clone)"))
                                {
                                    GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                    objToMove.transform.rotation = Quaternion.Euler(node.getRotationX(), node.getRotationY(), node.getRotationZ());
                                    objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                    yield return new WaitForSeconds(creationDelay);
                                }
                            }

                            //update the Tree
                            TreeSet = tempTree;
                        }
                        else
                        {
                            //remember to destroy the nodes
                            //delete the copies of the objects
                            foreach (Node node in nodesToRotate)
                            {
                                GameObject objToDelete = GameObject.Find(node.ToString() + "temp(Clone)");

                                //destroying the object
                                Destroy(objToDelete);

                                yield return new WaitForSeconds(creationDelay);
                            }

                            placeholder.text = "Could not add node!";
                            placeholder.color = Color.red;
                        }


                    }
                }
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Rotate command, ex: RotateX simple table_1 by 90
            else if (words.Length == 5 && flag == 10)
            {

                //the parent node name
                string parent = words[2];

                //Add to the list of exempt objects
                listOfObjects.Add(parent);

                //creating the parent node
                Node parentNode = new Node(parent);

                //check if the object to be rotated exists
                Boolean checkObjectExist = checkExist(Tree, parentNode);

                //if the object exists
                if (checkObjectExist)
                {
                    //getting the rotation ex: 180
                    string rotation = words[4];

                    //normalise the rotation
                    float rotationInteger = float.Parse(rotation) % 360;

                    //get the current rotation of the gameobject
                    GameObject obj = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //get the dimensions of the gameObject
                    Vector3 size = obj.transform.localScale;

                    //iterate through the tree
                    foreach (KeyValuePair<Node, Node[]> entry in TreeSet)
                    {
                        //check the name of each node
                        if (entry.Key.ToString().Equals(parentNode.ToString()))
                        {
                            //create a copy and add it to the list
                            parentNode = entry.Key.CopyDeep();
                        }
                    }

                    //get the rotation on the x-axis only
                    float currentRotation = parentNode.getRotationX();

                    //normalise the current rotation, 450 same as 90
                    currentRotation = currentRotation % 360;

                    //rotation to be
                    float futureRotation = (currentRotation + rotationInteger) % 360;

                    //get the current coordinates of the object
                    Vector3 coordinates = obj.transform.position;

                    //check what the future rotation will be, if it is 0, 180 then reduce the height, else increase
                    if (futureRotation == 90 || futureRotation == 270) {
                        //given that the unity coordinates are calculated on the centre of the object,
                        //we need to add half of the object's height to the 'y' coordinate such that
                        //the bottom of the object touches the ground
                        coordinates.y = coordinates.y - ((size.y) / 2) + ((size.z) / 2);
                    }
                    else if (futureRotation == 0 || futureRotation == 180) {
                        //given that the unity coordinates are calculated on the centre of the object,
                        //we need to add half of the object's height to the 'y' coordinate such that
                        //the bottom of the object touches the ground
                        coordinates.y = coordinates.y + ((size.y) / 2) - ((size.z) / 2);
                    }

                    //check if they are the same
                    if (currentRotation != futureRotation)
                    {
                        //setting the rotation
                        parentNode.setRotationX(futureRotation);

                        //setting the coordinates
                        float[] coordinatesFloat = new float[] {coordinates.x, coordinates.y, coordinates.z};
                        parentNode.setCoordinates(coordinatesFloat);

                        //create a temporary tree and recalculate the rotation
                        //create a copy of the current Tree
                        IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                        //if the node has children, remove the relationships
                        tempTree = removeChildren(tempTree, parentNode);
                        tempTree = removeNodeAsChild(tempTree, parentNode);

                        //change the rotation and coordinates of the parentNode in the Tree datastructure
                        changeTreeRotationsX(tempTree, parentNode);

                        //get the node from the temptree with coordinates, object type etc.
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //check the name of each node
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                //create a copy and add it to the list
                                parentNode = entry.Key.CopyDeep();
                            }
                        }

                        //flag to show if the rotation can be done or not
                        Boolean create = true;

                        //change name as it is a temporary duplicate
                        parentNode.setValue(parentNode.ToString() + "Copy");

                        //check that the nodes can be moved
                        StartCoroutine(checkSceneCollidersParent(parentNode));

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
                        }

                        //if there are no collisions at all
                        if (create == true)
                        {
                            //the objects can be moved
                            GameObject objToDelete = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //destroying the object
                            Destroy(objToDelete);

                            yield return new WaitForSeconds(creationDelay);

                            //find the object
                            String originalObject = parentNode.ToString().Replace("Copy", "");

                            GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                            objToUpdate.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());
                            objToUpdate.transform.position = new Vector3(parentNode.returnX(), parentNode.returnY(), parentNode.returnZ());

                            yield return new WaitForSeconds(creationDelay);

                            //find the created object
                            if (GameObject.Find(originalObject + "(Clone)"))
                            {
                                GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                objToMove.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());
                                objToMove.transform.position = new Vector3(parentNode.returnX(), parentNode.returnY(), parentNode.returnZ());

                                yield return new WaitForSeconds(creationDelay);
                            }

                            //update the Tree
                            TreeSet = tempTree;
                        }
                        else
                        {
                            //destroy the copy gameobject
                            GameObject objToDelete = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //destroying the object
                            Destroy(objToDelete);

                            yield return new WaitForSeconds(creationDelay);

                            placeholder.text = "Could not add node!";
                            placeholder.color = Color.red;
                        }
                    }
                }
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

                //reset the object list
                listOfObjects.Clear();
            }

            //if the command is a Rotate command, ex: RotateZ simple table_1 by 90
            else if (words.Length == 5 && flag == 11)
            {
                //check on which axis it is being rotated
                Boolean collisions = false;

                //the parent node name
                string parent = words[2];

                //Add to the list of exempt objects
                listOfObjects.Add(parent);

                //creating the parent node
                Node parentNode = new Node(parent);

                //check if the object to be rotated exists
                Boolean checkObjectExist = checkExist(Tree, parentNode);

                //if the object exists
                if (checkObjectExist)
                {
                    //getting the rotation ex: 180
                    string rotation = words[4];

                    //normalise the rotation
                    float rotationInteger = float.Parse(rotation) % 360;

                    //get the current rotation of the gameobject
                    GameObject obj = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //get the dimensions of the gameObject
                    Vector3 size = obj.transform.localScale;

                    //iterate through the tree
                    foreach (KeyValuePair<Node, Node[]> entry in TreeSet)
                    {
                        //check the name of each node
                        if (entry.Key.ToString().Equals(parentNode.ToString()))
                        {
                            //create a copy and add it to the list
                            parentNode = entry.Key.CopyDeep();
                        }
                    }

                    //get the rotation on the x-axis only
                    float currentRotation = parentNode.getRotationZ();

                    //normalise the current rotation, 450 same as 90
                    currentRotation = currentRotation % 360;

                    //rotation to be
                    float futureRotation = (currentRotation + rotationInteger) % 360;

                    //get the current coordinates of the object
                    Vector3 coordinates = obj.transform.position;

                    //check what the future rotation will be, if it is 0, 180 then reduce the height, else increase
                    if (futureRotation == 90 || futureRotation == 270)
                    {
                        //given that the unity coordinates are calculated on the centre of the object,
                        //we need to add half of the object's height to the 'y' coordinate such that
                        //the bottom of the object touches the ground
                        coordinates.y = coordinates.y - ((size.y) / 2) + ((size.x) / 2);
                    }
                    else if (futureRotation == 0 || futureRotation == 180)
                    {
                        //given that the unity coordinates are calculated on the centre of the object,
                        //we need to add half of the object's height to the 'y' coordinate such that
                        //the bottom of the object touches the ground
                        coordinates.y = coordinates.y + ((size.y) / 2) - ((size.x) / 2);
                    }

                    //check if they are the same
                    if (currentRotation != futureRotation)
                    {
                        //setting the rotation
                        parentNode.setRotationZ(futureRotation);

                        //setting the coordinates
                        float[] coordinatesFloat = new float[] { coordinates.x, coordinates.y, coordinates.z };
                        parentNode.setCoordinates(coordinatesFloat);

                        //create a temporary tree and recalculate the rotation
                        //create a copy of the current Tree
                        IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                        //if the node has children, remove the relationships
                        tempTree = removeChildren(tempTree, parentNode);
                        tempTree = removeNodeAsChild(tempTree, parentNode);

                        //change the rotation and coordinates of the parentNode in the Tree datastructure
                        changeTreeRotationsZ(tempTree, parentNode);

                        //get the node from the temptree with coordinates, object type etc.
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //check the name of each node
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                //create a copy and add it to the list
                                parentNode = entry.Key.CopyDeep();
                            }
                        }

                        //flag to show if the rotation can be done or not
                        Boolean create = true;

                        //change name as it is a temporary duplicate
                        parentNode.setValue(parentNode.ToString() + "Copy");

                        //check that the nodes can be moved
                        StartCoroutine(checkSceneCollidersParent(parentNode));

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
                        }

                        //if there are no collisions at all
                        if (create == true)
                        {
                            //the objects can be moved
                            GameObject objToDelete = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //destroying the object
                            Destroy(objToDelete);

                            yield return new WaitForSeconds(creationDelay);

                            //find the object
                            String originalObject = parentNode.ToString().Replace("Copy", "");

                            GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                            objToUpdate.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());
                            objToUpdate.transform.position = new Vector3(parentNode.returnX(), parentNode.returnY(), parentNode.returnZ());

                            yield return new WaitForSeconds(creationDelay);

                            //find the created object
                            if (GameObject.Find(originalObject + "(Clone)"))
                            {
                                GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                objToMove.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());
                                objToMove.transform.position = new Vector3(parentNode.returnX(), parentNode.returnY(), parentNode.returnZ());

                                yield return new WaitForSeconds(creationDelay);
                            }

                            //update the Tree
                            TreeSet = tempTree;
                        }
                        else
                        {
                            //destroy the copy gameobject
                            GameObject objToDelete = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //destroying the object
                            Destroy(objToDelete);

                            yield return new WaitForSeconds(creationDelay);

                            placeholder.text = "Could not add node!";
                            placeholder.color = Color.red;
                        }
                    }
                }
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }

                //reset the object list
                listOfObjects.Clear();
            }

            //if the command is a Break command, ex: Break table_1
            else if (words.Length == 2 && flag == 12)
            {
                //the parent node
                string parent = words[1];

                //creating the parent node
                Node parentNode = new Node(parent);

                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //check that the object to break exists
                Boolean checkObjectExists = checkExist(TreeSet, parentNode);

                if (checkObjectExists) {
                    //call the break method
                    tempTree = breakRelationships(tempTree, parentNode);

                    TreeSet = tempTree;
                }
                else {
                    placeholder.text = "Incorrect input!";
                    placeholder.color = Color.red;
                }
            }

            ////if the command is a Break command, ex: Reset rotations table_1
            //else if (words.Length == 3 && flag == 13)
            //{
            //    //the parent node
            //    string parent = words[2];

            //    //Add to the list of exempt objects
            //    listOfObjects.Add(parent);

            //    //creating the parent node
            //    Node parentNode = new Node(parent);

            //    //set the objectType
            //    parentNode.setObjectType(parent);

            //    //get the gameObject
            //    GameObject parentObj = parentNode.getObjectTrue();

            //    //create coordinates
            //    float[] parentObj.transform.position

            //    //create a copy of the current Tree
            //    IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

            //    //check that the object to break exists
            //    Boolean checkObjectExists = checkExist(TreeSet, parentNode);

            //    if (checkObjectExists)
            //    {
            //        //iterate through the tree
            //        foreach (KeyValuePair<Node, Node[]> entry in TreeSet)
            //        {
            //            //check the name of each node
            //            if (entry.Key.ToString().Equals(parentNode.ToString()))
            //            {
            //                //change rotations
            //                entry.Key.setRotationX(0f);
            //                entry.Key.setRotationY(0f);
            //                entry.Key.setRotationZ(0f);

            //                //reset the coordinates
            //                entry.Key.setCoordinates();
            //            }
            //        }

            //        TreeSet = tempTree;
            //    }
            //    else
            //    {
            //        placeholder.text = "Incorrect input!";
            //        placeholder.color = Color.red;
            //    }
            //}

            //if the command is not a valid input
            else
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
        if ((tree.Keys.Any(x => x.ToString().Equals(node.ToString()))))
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

    //method used to add new nodes to the tree
    public Boolean addChildren(IDictionary<Node, Node[]> tree, Node parent, Node child)
    {
        Node[] temp = new Node[] { }; //utility array

        Node nodeReference = null;

        //parent exists, i.e. to add children
        if (tree.Keys.Any(x => x.ToString().Equals(parent.ToString())))
        {
            //Check if there already exists an object with the same preposition, ex: book is *left_of table*, man is *left_of table*

            string preposition = child.getPreposition();//getting the preposition of the child

            //iterate through the tree to find the parentNode
            foreach (KeyValuePair<Node, Node[]> entry in tree)
            {
                //if they are equal
                if (entry.Key.ToString().Equals(parent.ToString())){
                    //initialise the reference
                    nodeReference = entry.Key;

                    //get the current children
                    temp = tree[entry.Key];
                }
            }

            //add the new child
            temp = temp.Append(child).ToArray();

            //overwrite the current children
            tree[nodeReference] = temp;

            //in the case of the between preposition
            if (!tree.Keys.Any(x => x.ToString().Equals(child.ToString())))
            {
                //create new record with null children
                tree[child] = new Node[] { };
            }

            return true; //successful creation
        }
        else
        {//parent does not exist, i.e. to add parents
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

    //method used to make a node no longer a child of a parent
    public IDictionary<Node, Node[]> removeNodeAsChild(IDictionary<Node, Node[]> tree, Node child)
    {
        //create a copy of the temporary tree
        IDictionary<Node, Node[]> treeCopy = tree.ToDictionary(entry => entry.Key, entry => entry.Value);

        Node[] temp = new Node[] { }; //utility array

        //Checking if the child exists
        if (tree.ContainsKey(child))
        {
            //iterate through the tree
            foreach (KeyValuePair<Node, Node[]> entry in tree)
            {
                //get the current children
                temp = tree[entry.Key];

                //create a list to keep all the children apart from the one to be deleted
                List<Node> listToKeep = new List<Node>();

                //iterate through the array
                foreach (Node childNode in temp) {

                    //if our node is a child of the current parent node
                    if (!childNode.ToString().Equals(child.ToString())){
                        //remove the node from an array
                        listToKeep.Add(childNode);
                    }
                }
                //overwrite the current children
                treeCopy[entry.Key] = listToKeep.ToArray();
            }
        }

        return treeCopy; //successful creation
    }

    //method used to remove the children of a node
    public IDictionary<Node, Node[]> removeChildren(IDictionary<Node, Node[]> tree, Node parent)
    {
        //create a copy of the temporary tree
        IDictionary<Node, Node[]> treeCopy = tree.ToDictionary(entry => entry.Key, entry => entry.Value);

        //Checking if the child exists
        if (tree.ContainsKey(parent))
        {
            //delete the node's relationships with children-----
            treeCopy[parent] = new Node[] { };
        }

        return treeCopy; //successful creation
    }

    //method used to break a node free from all relationships
    public IDictionary<Node, Node[]> breakRelationships(IDictionary<Node, Node[]> tree, Node child)
    {
        //create a copy of the temporary tree
        IDictionary<Node, Node[]> treeCopy = tree.ToDictionary(entry => entry.Key, entry => entry.Value);

        Node[] temp = new Node[] { }; //utility array

        //Checking if the node exists
        if (tree.ContainsKey(child))
        {
            //break the node from its current parent-----

            //iterate through the tree
            foreach (KeyValuePair<Node, Node[]> entry in tree)
            {
                //get the current children of the current parent node
                temp = tree[entry.Key];

                //create a list to keep all the children apart from the one to be deleted
                List<Node> listToKeep = new List<Node>();

                //iterate through the children array
                foreach (Node childNode in temp)
                {

                    //if our node is a child of the current parent node
                    if (!childNode.ToString().Equals(child.ToString()))
                    {
                        //remove the node from an array
                        listToKeep.Add(childNode);
                    }
                }
                //overwrite the current children
                treeCopy[entry.Key] = listToKeep.ToArray();
            }

            //delete the node's relationships with children-----
            treeCopy[child] = new Node[] { };
        }

        return treeCopy; //successful creation
    }

    public void createScene()
    {
        //calling the create method
        StartCoroutine(createScene2());
    }

    public IEnumerator createScene2()
    {
        //check if the tree has objects in it
        if (Tree.Count() == 0)
        {
            placeholder.text = "No objects have been created";
            placeholder.color = Color.red;
        }
        else
        {
            //in the case that the scene has been loaded

            //iterate through the Tree
            foreach (KeyValuePair<Node, Node[]> entry in Tree)
            {
                //first thing to do is spawn the parent

                //get name of the parent
                string name1 = entry.Key.ToString();

                //Check if the object has already been created
                Boolean existsFlag1 = checkScene(name1 + "(Clone)");

                //object does not already exist if false
                if (existsFlag1 == false)
                {
                    //create the collision boxes 
                    StartCoroutine(checkSceneCollidersParent(entry.Key));

                    yield return new WaitForSeconds(creationDelay);

                    if (found == true)
                    {
                        found = false;//reset the found flag
                    }

                    //create the object in real time to check for later
                    StartCoroutine(checkSceneCollidersParentTrue(entry.Key));

                    yield return new WaitForSeconds(creationDelay);

                    if (found == true)
                    {
                        found = false;//reset the found flag
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

    public IEnumerator checkSceneCollidersParentTrue(Node node)
    {
        canCreateTrue = true;

        //passing the attributes to the global temporary node
        temporaryNode = node;

        //giving permission to the update method
        yield return new WaitForSeconds(creationDelay);

        //object is created in the update method

        //get name of the parent
        string name = node.ToString();

        //here we should check
        if (GameObject.Find(name + "(Clone)") != null)
        {
            //found
            found = true;

            if (GameObject.FindWithTag("Enemy"))
            {
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

    public void changeTreeCoordinates(IDictionary<Node, Node[]> tree, Node node) {
        //iterate through the tree
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //first thing to do is spawn the parent
            if (entry.Key.ToString().Equals(node.ToString())) {
                float x = node.returnX();
                float y = node.returnY();
                float z = node.returnZ();

                //adding the coordinates
                float[] coordinates = new float[3] {x,y,z};

                entry.Key.setCoordinates(coordinates);
            }
        }
    }

    //used to add an already created node as a child of another
    public IDictionary<Node, Node[]> changeTreeRelations(IDictionary<Node, Node[]> tree, Node newParentNode, Node futureChildNode)
    {
        //runner object to use methods
        Test graphObject = new Test();

        //create a copy of the temporary tree
        IDictionary<Node, Node[]> tempTree = tree.ToDictionary(entry => entry.Key, entry => entry.Value);

        //iterate through the tree to find the newParentNode
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //find the new parent node
            if (entry.Key.ToString().Equals(newParentNode.ToString()))
            {
                //iterate through the tree to find the parent node
                foreach (KeyValuePair<Node, Node[]> entry2 in tree)
                {
                    //find the parent node
                    if (entry2.Key.ToString().Equals(futureChildNode.ToString()))
                    {
                        //change the preposition
                        entry2.Key.setPreposition(futureChildNode.getPreposition());

                        //change the spacing
                        entry2.Key.setSpacing(futureChildNode.getSpacing());

                        //Add the parentNode as a child of the newParentNode
                        graphObject.addChildren(tempTree, entry.Key, entry2.Key);

                        //change the coordinates of the node
                        float[] coordinates = calculatePrepositionCoordinates(entry.Key, entry2.Key);

                        //iterate through the tree to find the child node
                        foreach (KeyValuePair<Node, Node[]> entry3 in tree)
                        {
                            //find the child node
                            if (entry3.Key.ToString().Equals(futureChildNode.ToString()))
                            {
                                entry3.Key.setCoordinates(coordinates);
                            }
                        }
                    }
                }
            }
        }

        //set the temporary tree to the copy
        return tempTree;
    }

    //used to change the rotation of the objects
    public void changeTreeRotationsY(IDictionary<Node, Node[]> tree, Node node)
    {
        //iterate through the tree
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //check if the current node is equal to the node needed
            if (entry.Key.ToString().Equals(node.ToString()))
            {
                float rotation = node.getRotationY();

                //update the rotation of the object
                entry.Key.setRotationY(rotation);

                //pass it to every instance 
                foreach (KeyValuePair<Node, Node[]> entry2 in tree)
                {
                    //get the children
                    Node[] children = tree[entry2.Key];

                    //iterate through the children
                    foreach (Node entry3 in children)
                    {
                        //if it is equal to the required node
                        if (entry3.ToString().Equals(node.ToString())) {
                            entry3.setRotationY(rotation);
                        }
                    }
                }
            }
        }
    }

    //used to change the rotation and coordinates of objects
    public void changeTreeRotationsX(IDictionary<Node, Node[]> tree, Node node)
    {
        //iterate through the tree
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //check if the current node is equal to the node needed
            if (entry.Key.ToString().Equals(node.ToString()))
            {
                //get the rotation
                float rotation = node.getRotationX();

                //get the coordinates
                float x = node.returnX();
                float y = node.returnY();
                float z = node.returnZ();

                //update the coordinates of the object
                entry.Key.setCoordinates(new float[] { x, y, z });

                //update the rotation of the object
                entry.Key.setRotationX(rotation);

                //pass it to every instance 
                foreach (KeyValuePair<Node, Node[]> entry2 in tree)
                {
                    //get the children
                    Node[] children = tree[entry2.Key];

                    //iterate through the children
                    foreach (Node entry3 in children)
                    {
                        //if it is equal to the required node
                        if (entry3.ToString().Equals(node.ToString()))
                        {
                            entry3.setRotationX(rotation);
                        }
                    }
                }
            }
        }
    }

    //used to change the rotation and coordinates of objects
    public void changeTreeRotationsZ(IDictionary<Node, Node[]> tree, Node node)
    {
        //iterate through the tree
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //check if the current node is equal to the node needed
            if (entry.Key.ToString().Equals(node.ToString()))
            {
                //get the rotation
                float rotation = node.getRotationZ();

                //get the coordinates
                float x = node.returnX();
                float y = node.returnY();
                float z = node.returnZ();

                //update the coordinates of the object
                entry.Key.setCoordinates(new float[] { x, y, z });

                //update the rotation of the object
                entry.Key.setRotationZ(rotation);

                //pass it to every instance 
                foreach (KeyValuePair<Node, Node[]> entry2 in tree)
                {
                    //get the children
                    Node[] children = tree[entry2.Key];

                    //iterate through the children
                    foreach (Node entry3 in children)
                    {
                        //if it is equal to the required node
                        if (entry3.ToString().Equals(node.ToString()))
                        {
                            entry3.setRotationZ(rotation);
                        }
                    }
                }
            }
        }
    }

    //used for simple parent to child operations
    public void setCoordinatesSimple(IDictionary<Node, Node[]> tree, Node parentNode, Node childNode) {
        //check if the tree has objects in it
        if (tree.Count() == 0)
        {
            placeholder.text = "No objects have been created";
            placeholder.color = Color.red;
        }
        else
        {
            //check if 'node' is null or not
            if (parentNode != null && childNode != null)
            {
                //iterate through the Tree to find the parentNode
                foreach (KeyValuePair<Node, Node[]> entry in tree)
                {
                    //check if the current node is equal to the parent node
                    if (entry.Key.ToString().Equals(parentNode.ToString()))
                    {
                        //iterate through the children
                        foreach (Node child in entry.Value)
                        {
                            //check if the current node is equal to the child node
                            if (child.ToString().Equals(childNode.ToString()))
                            {
                                //get the coordinates
                                string preposition = child.getPreposition();

                                float[] coordinates = null;

                                //the preposition must not be equal to between
                                if (!preposition.Equals("between"))
                                {
                                    //calculate the coordinates
                                    coordinates = calculatePrepositionCoordinates(entry.Key, child);

                                    //apply to all instances of the child node
                                    foreach (KeyValuePair<Node, Node[]> entry2 in tree)
                                    {
                                        //check if the current node is equal to the parent node
                                        if (entry2.Key.ToString().Equals(childNode.ToString()))
                                        {
                                            //setting the coordinates
                                            entry2.Key.setCoordinates(coordinates);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    //used for compound parent to children operations
    public List<Node> setCoordinatesCompound(IDictionary<Node, Node[]> tree, List<Node> nodes)
    {
        //create a copy of the list
        List<Node> updatedList = new List<Node>();

        //check if the tree has objects in it
        if (tree.Count() == 0)
        {
            placeholder.text = "No objects have been created";
            placeholder.color = Color.red;
        }
        else
        {
            //check if 'nodes' is null or not
            if (nodes != null)
            {
                //iterate through the list
                foreach (Node node in nodes)
                {
                    foreach (KeyValuePair<Node, Node[]> entry in tree)
                    {
                        //check if the current node is equal to the required node
                        if (entry.Key.ToString().Equals(node.ToString()))
                        {
                            //iterate through the children
                            foreach (Node child in entry.Value)
                            {
                                float[] coordinates = null;

                                //calculate the coordinates
                                coordinates = calculatePrepositionCoordinates(entry.Key, child);

                                //apply to all instances of the child node
                                foreach (KeyValuePair<Node, Node[]> entry2 in tree)
                                {
                                    //check if the current node is equal to the parent node
                                    if (entry2.Key.ToString().Equals(child.ToString()))
                                    {
                                        //setting the coordinates
                                        entry2.Key.setCoordinates(coordinates);
                                    }
                                }

                                //update the list
                                updatedList.Add(child);
                            }
                        }
                    }
                }
            }
        }

        //check that all nodes are included in the updatedList
        //iterate through the list
        foreach (Node node in nodes)
        {
            //if the node is not in the list, add it
            if (!updatedList.Exists(x => x.ToString().Equals(node.ToString()))) {
                updatedList.Add(node);
            }
        }

        return updatedList;
    }

    //pass a list instead
    public void setCoordinates(IDictionary<Node, Node[]> tree, Node node)
    {
        //check if the tree has objects in it
        if (tree.Count() == 0)
        {
            placeholder.text = "No objects have been created";
            placeholder.color = Color.red;
        }
        else
        {
            //check if 'node' is null or not
            if (node != null)
            {
                //start by setting the coords for the children of the given node
                //iterate through the Tree
                foreach (KeyValuePair<Node, Node[]> entry in tree)
                {
                    //check if the current node is equal to the required node
                    if (entry.Key.ToString().Equals(node.ToString())) {

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

    //used to recalculate the rotations of the gameobjects
    public void setRotations(IDictionary<Node, Node[]> tree)
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
                //checking if it has children
                if (entry.Value.Length != 0)
                {
                    //iterate through the children
                    foreach (Node child in entry.Value)
                    {
                        //recalculate the rotations
                        calculateRotations(entry.Key, child);
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
        float x = node.returnX();
        float y = node.returnY();
        float z = node.returnZ();

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

    //recalculating rotations
    public void calculateRotations(Node parent, Node child)
    {
        //getting the parent rotation
        float rotationParent = parent.getRotationY();

        //getting the child rotation
        float rotationChild = child.getRotationY();

        //update the child rotation
        rotationChild = rotationChild + rotationParent;

        //set the new child rotation
        child.setRotationY(rotationChild);

        //change the coordinates as well
    }

    public void calculatePrepositionCoordinatesAndRotations(Node parent, Node child, float rotation) {
        //reference to the already rotated gameObject
        GameObject objCreated = GameObject.Find(parent.ToString() + "Copytemp(Clone)");

        //get the current rotation of the child
        float rotationChild = child.getRotationY() % 360;

        //the new rotation on the y axis
        float newRotationChild = rotationChild + rotation;

        //update the child
        child.setRotationY(newRotationChild);

        //spacing between objects
        float spacing;

        //Check if the spacing is string or float
        if (float.TryParse(child.getSpacing(), out float result))//it is a number
        {
            //convert string to float
            spacing = float.Parse(child.getSpacing());
        }
        else //input string is a word
        {
            if (child.getSpacing().Equals("Touching", StringComparison.OrdinalIgnoreCase))
            {
                spacing = 0f;
            }
            else if (child.getSpacing().Equals("near", StringComparison.OrdinalIgnoreCase))
            {
                spacing = 0.5f;
            }
            else
            {//if none set spacing to 0
                spacing = 0f;
            }
        }

        //important that the true gameObject and the enemy gameObject are the same

        //reference to the original cube that we want to create our new cube infront_of, left_of, right_of etc.
        GameObject currentObject = parent.getObjectTrue();

        //reference to the actual instance of the second cube that we create in front of the original cube.
        GameObject childObject = child.getObjectTrue();

        //get the size of each object respectively
        Vector3 sizeCurrentObj = currentObject.transform.localScale;

        Vector3 sizeChildObj = childObject.transform.localScale;

        //get the dimensions of currentObj-------

        //infront, behind
        float widthCurrent = sizeCurrentObj.z;

        //left, right
        float lengthCurrent = sizeCurrentObj.x;

        //on, under
        float heightCurrent = sizeCurrentObj.y;

        //get the dimensions of childObj-------

        //infront, behind
        float widthChild = sizeChildObj.z;

        //left, right
        float lengthChild = sizeChildObj.x;

        //on, under
        float heightChild = sizeChildObj.y;

        //get the preposition for the relationship between the objects
        string preposition = child.getPreposition();

        //object vector3
        Vector3 coords;

        //array to store the new coordinates
        float[] coordinates;

        //depending on the preposition
        if (preposition.Equals("left_of"))
        {
            Vector3 vector = objCreated.transform.right;
            vector.x = Mathf.Abs(vector.x);
            vector.y = Mathf.Abs(vector.y);
            vector.z = Mathf.Abs(vector.z);

            coords = new Vector3(parent.returnX(), parent.returnY(), parent.returnZ()) + ((spacing + (lengthCurrent / 2) + (lengthChild / 2)) * objCreated.transform.right);

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightCurrent / 2)) + (heightChild / 2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("right_of"))
        {
            Vector3 vector = objCreated.transform.right;
            vector.x = Mathf.Abs(vector.x);
            vector.y = Mathf.Abs(vector.y);
            vector.z = Mathf.Abs(vector.z);

            coords = new Vector3(parent.returnX(), parent.returnY(), parent.returnZ()) - ((spacing + (lengthCurrent / 2) + (lengthChild / 2)) * objCreated.transform.right);

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightCurrent / 2)) + (heightChild / 2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("on"))
        {
            coords = new Vector3(parent.returnX(), parent.returnY(), parent.returnZ()) + (((heightCurrent / 2) + (heightChild / 2)) * objCreated.transform.up);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("under"))
        {
            coords = new Vector3(parent.returnX(), parent.returnY(), parent.returnZ());

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightCurrent / 2)) + (heightChild / 2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("infront"))
        {
            Vector3 vector = objCreated.transform.right;
            vector.x = Mathf.Abs(vector.x);
            vector.y = Mathf.Abs(vector.y);
            vector.z = Mathf.Abs(vector.z);

            coords = new Vector3(parent.returnX(), parent.returnY(), parent.returnZ()) + ((spacing + (widthCurrent / 2) + (widthChild / 2)) * objCreated.transform.forward);

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightCurrent / 2)) + (heightChild / 2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("behind"))
        {
            coords = new Vector3(parent.returnX(), parent.returnY(), parent.returnZ()) - ((spacing + (widthCurrent / 2) + (widthChild / 2)) * objCreated.transform.forward);

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightCurrent / 2)) + (heightChild / 2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
    }


    public float[] calculatePrepositionCoordinates(Node parent, Node child)
    {
        //spacing between objects
        float spacing;

        //Check if the spacing is string or float
        if (float.TryParse(child.getSpacing(), out float result))//it is a number
        {
            //convert string to float
            spacing = float.Parse(child.getSpacing());
        }
        else //input string is a word
        {
            if (child.getSpacing().Equals("Touching", StringComparison.OrdinalIgnoreCase)) {
                spacing = 0f;
            }
            else if (child.getSpacing().Equals("near", StringComparison.OrdinalIgnoreCase)) {
                spacing = 0.5f;
            }
            else {//if none set spacing to 0
                spacing = 1f;
            }
        }

        //coordinates of the parentObject
        float X = parent.returnX();
        float Y = parent.returnY();
        float Z = parent.returnZ();

        //important that the true gameObject and the enemy gameObject are the same

        //reference to the original cube that we want to create our new cube infront_of, left_of, right_of etc.
        GameObject parentObject = parent.getObjectTrue();

        //reference to the actual instance of the second cube that we create in front of the original cube.
        GameObject childObject = child.getObjectTrue();

        //get the size of each object respectively----

        Vector3 sizeParentObj = parentObject.transform.localScale;

        Vector3 sizeChildObj = childObject.transform.localScale;

        //get the dimensions of parentObj----

        //infront, behind
        float widthParent = sizeParentObj.z;

        //left, right
        float lengthParent = sizeParentObj.x;

        //on, under
        float heightParent = sizeParentObj.y;

        //get the dimensions of childObj----

        //infront, behind
        float widthChild = sizeChildObj.z;

        //left, right
        float lengthChild = sizeChildObj.x;

        //on, under
        float heightChild = sizeChildObj.y;

        //get the preposition for the relationship between the objects
        string preposition = child.getPreposition();

        //get reference to the parent gameObject so that we have a reference to the faces
        GameObject parentGameObject = GameObject.Find(parent.ToString() + "temp" + "(Clone)");

        //object vector3
        Vector3 coords;

        //array to store the new coordinates
        float[] coordinates = null;

        //depending on the preposition
        if (preposition.Equals("left_of"))
        {
            coords = new Vector3(X, Y, Z) + ((spacing + (lengthParent/ 2) + (lengthChild/ 2)) * parentGameObject.transform.right);

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightParent/2)) + (heightChild/2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("right_of"))
        {
            coords = new Vector3(X, Y, Z) - ((spacing + (lengthParent/ 2) + (lengthChild/ 2)) * parentGameObject.transform.right);

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightParent / 2)) + (heightChild / 2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("on"))
        {
            coords = new Vector3(X, Y, Z) + (((heightParent/ 2) + (heightChild/ 2)) * parentGameObject.transform.up);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("under"))
        {
            coords = new Vector3(X, Y, Z);

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightParent / 2)) + (heightChild / 2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("infront"))
        {
            coords = new Vector3(X, Y, Z) + ((spacing + (widthParent/ 2) + (widthChild/ 2)) * parentGameObject.transform.forward);

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightParent / 2)) + (heightChild / 2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("behind"))
        {
            coords = new Vector3(X, Y, Z) - ((spacing + (widthParent/ 2) + (widthChild/ 2)) * parentGameObject.transform.forward);

            //remember that the height of the objects can be different
            coords.y = (coords.y - (heightParent / 2)) + (heightChild / 2);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }

        return coordinates;
    }

    IEnumerator CheckAllNodes(Node[] nodesArray)
    {
        foreach (Node node in nodesArray)
        {
            yield return StartCoroutine(checkSceneCollidersParent(node));
        }
    }

    public void deleteNode(IDictionary<Node, Node[]> tree, Node node) {
        IDictionary<Node, Node[]> tempTree = tree.ToDictionary(entry => entry.Key, entry => entry.Value);

        //remove the children of the node
        tempTree = removeChildren(tempTree, node);

        //remove the node as a child
        tempTree = removeNodeAsChild(tempTree, node);

        //iterate through the Tree, delete all instances of the node
        tempTree.Remove(node);

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
            float coordX1 = parent1.returnX();
            float coordY1 = parent1.returnY();
            float coordZ1 = parent1.returnZ();

            //get the coordinates of the second parent
            float coordX2 = parent2.returnX();
            float coordY2 = parent2.returnY();
            float coordZ2 = parent2.returnZ();

            //calculating the coordinates of the child
            float coordX3 = (coordX1 + coordX2) / 2;
            float coordY3 = (coordY1 + coordY2) / 2;
            float coordZ3 = (coordZ1 + coordZ2) / 2;

            //array to store the new coordinates
            float[] coordinates = new float[3] { coordX3, coordY3, coordZ3 };

            //create coordinates for the object
            child.setCoordinates(coordinates);
        }
    }

    public Vector3 getSize(GameObject ObjectUnity)
    {
        return ObjectUnity.GetComponent<Collider>().bounds.size;
    }

    public float getWidth(GameObject ObjectUnity)
    {
        Vector3 size = getSize(ObjectUnity);
        return size.x;
    }

    public float getLength(GameObject ObjectUnity)
    {
        Vector3 size = getSize(ObjectUnity);
        return size.y;
    }

    public float getDepth(GameObject ObjectUnity)
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

    //method used to turn the dictionary datastructure into a string
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
        //Just in case
        //Tree.Keys.Any(x => x.ToString().Equals(parent.ToString());

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

                Node newNodeCopyParent = null;

                //iterate through the keys and values
                for (int i = 0; i < KeyAndValue.Length; i++)
                {
                    //if it is the first node it is the parent node, i.e. the key
                    if (i == 0)
                    {
                        //split the key/value into name, coordinates and preposition
                        string[] NameCoordinatesPrepositionRotation = KeyAndValue[i].Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

                        //setting objectType from the name, Ex: cube_1
                        string[] objectTypeWithNo = NameCoordinatesPrepositionRotation[0].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                        //setting preposition, Ex: on, null etc
                        string preposition = NameCoordinatesPrepositionRotation[2];

                        //setting rotation, Ex: 90, 180, 270
                        string rotationString = NameCoordinatesPrepositionRotation[3];
                        string[] rotation = rotationString.Split(new[] { 'v' }, StringSplitOptions.RemoveEmptyEntries);
                        float[] intRotation = Array.ConvertAll(rotation, float.Parse);

                        //setting coordinatesString
                        string coordinatesString = NameCoordinatesPrepositionRotation[1];
                        string[] coordinates = coordinatesString.Split(new[] { 'v' }, StringSplitOptions.RemoveEmptyEntries);
                        float[] floatCoordinates = Array.ConvertAll(coordinates, float.Parse);

                        //setting the spacing
                        string spacing = NameCoordinatesPrepositionRotation[4];

                        //setting value
                        parent.setValue(NameCoordinatesPrepositionRotation[0]);
                        parent.setObjectType(objectTypeWithNo[0]);
                        parent.setSpacing(spacing);

                        //setting the preposition
                        parent.setPreposition(preposition);

                        //setting the coordinates
                        parent.setCoordinates(floatCoordinates);

                        //setting the rotations
                        parent.setRotationX(intRotation[0]);
                        parent.setRotationY(intRotation[1]);
                        parent.setRotationZ(intRotation[2]);

                        //create a deep copy of the node
                        newNodeCopyParent = parent.CopyDeep();

                        //the parent node should not already exist
                        if (!Tree.Keys.Any(x => x.ToString().Equals(parent.ToString())))
                        {
                            //Add the node to the Tree
                            Boolean resultAddChildren = graphObject.addChildren(Tree, newNodeCopyParent, null); //adding the first node to the tree - it has no children

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

                        foreach (string value in values)
                        {
                            //split the key/value into name, coordinates and preposition
                            string[] NameCoordinatesPrepositionRotation = value.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

                            //setting objectType from the name, Ex: cube_1
                            string[] objectTypeWithNo = NameCoordinatesPrepositionRotation[0].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                            //setting preposition, Ex: on, null etc
                            string preposition = NameCoordinatesPrepositionRotation[2];

                            //setting rotation, Ex: 90, 180, 270
                            string rotationString = NameCoordinatesPrepositionRotation[3];
                            string[] rotation = rotationString.Split(new[] { 'v' }, StringSplitOptions.RemoveEmptyEntries);
                            float[] intRotation = Array.ConvertAll(rotation, float.Parse);

                            //setting coordinatesString
                            string coordinatesString = NameCoordinatesPrepositionRotation[1];
                            string[] coordinates = coordinatesString.Split(new[] { 'v' }, StringSplitOptions.RemoveEmptyEntries);
                            float[] floatCoordinates = Array.ConvertAll(coordinates, float.Parse);

                            //setting the spacing
                            string spacing = NameCoordinatesPrepositionRotation[4];

                            //setting value
                            child.setValue(NameCoordinatesPrepositionRotation[0]);
                            child.setObjectType(objectTypeWithNo[0]);
                            child.setSpacing(spacing);

                            child.setPreposition(preposition);
                            child.setCoordinates(floatCoordinates);

                            //setting the rotations
                            child.setRotationX(intRotation[0]);
                            child.setRotationY(intRotation[1]);
                            child.setRotationZ(intRotation[2]);

                            //create a deep copy of the child
                            Node newNodeCopy = child.CopyDeep();

                            //to add the child node, the parent node must exist
                            if (Tree.Keys.Any(x => x.ToString().Equals(parent.ToString())))
                            {
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

    //to minimise the Main UI
    public void minimiseUI()
    {
        minButton.SetActive(false);
        Menu.SetActive(false);
        maxButton.SetActive(true);
    }

    //to maximise the Main UI
    public void maximiseUI()
    {
        maxButton.SetActive(false);
        CameraMenu.SetActive(false);
        Menu.SetActive(true);
        minButton.SetActive(true);
    }

    //to open the Camera Menu
    public void getCameras()
    {
        minButton.SetActive(false);
        Menu.SetActive(false);
        CameraMenu.SetActive(true);
    }

    //to change the status of the boolean flag
    public void ChangeCollisionBoxStatus() {
        //change the flag when method is called
        if (collisionBoxFlag)
        {
            collisionBoxFlag = false;
        }
        else
        {
            collisionBoxFlag = true;
        }

        CollisionBoxStatus();
    }

    //to call the required methods depending on the status of the boolean flag
    public void CollisionBoxStatus() {
        //reference to the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        //get a list of all the gameobjects
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy")
            .Concat(GameObject.FindGameObjectsWithTag("Final"))
            .ToArray();

        //show the collision boxes
        if (collisionBoxFlag)
        {
            EnableCollisionBoxes(gameObjects);
        }
        //hide the collision boxes
        else
        {
            DisableCollisionBoxes(gameObjects);
        }
    }

    public void DisableCollisionBoxes(GameObject[] gameObjects)
    {
        //get the name of the material to be compared to
        String materialName = materialCollisionBox.name;

        //iterate through the list
        foreach (GameObject obj in gameObjects)
        {
            if (obj.GetComponent<Renderer>() != null)//the object has a renderer
            {
                //get the renderer component of the object
                Renderer renderer = obj.GetComponent<Renderer>();

                //check if it is already inactive
                if (renderer.enabled)
                {
                    //get the material name
                    Material material = renderer.material;
                    String materialObjectName = material.name;

                    //remove the '(Instance)' string from the material name
                    materialObjectName = materialObjectName.Replace(" (Instance)", "");

                    //check if it has the required material
                    if (renderer != null && materialObjectName.Equals(materialName))
                    {
                        //set the renderer to false
                        renderer.enabled = false;
                    }
                }
            }
        }
    }

    public void EnableCollisionBoxes(GameObject[] gameObjects)
    {
        //get the name of the material to be compared to
        String materialName = materialCollisionBox.name;

        //iterate through the list
        foreach (GameObject obj in gameObjects)
        {
            if (obj.GetComponent<Renderer>() != null)//the object has a renderer
            {
                //get the renderer component of the object
                Renderer renderer = obj.GetComponent<Renderer>();

                //check if it is already inactive
                if (!renderer.enabled)
                {
                    //get the material name
                    Material material = renderer.material;
                    String materialObjectName = material.name;

                    //remove the '(Instance)' string from the material name
                    materialObjectName = materialObjectName.Replace(" (Instance)", "");

                    //check if it has the required material
                    if (renderer != null && materialObjectName.Equals(materialName))
                    {
                        //set the renderer to false
                        renderer.enabled = true;
                    }
                }
            }
        }
    }

    public String getObjectCoordinates(String objectName) {
        foreach (KeyValuePair<Node, Node[]> entry in TreeSet)
        {
            if (entry.Key.ToString().Equals(objectName)) {
                return entry.Key.getCoordinatesUI();
            }
        }

        return "not found";
    }

    public Boolean getNameAndCoordsStatus() {
        return nameAndCoordinates;
    }

    public Boolean getFacesStatus()
    {
        return faces;
    }

    public Boolean getRotationStatus()
    {
        return rotationAxis;
    }

    ////a utility method used to set the parents of a gameObject to inactive
    //public void InactiveRecursive(GameObject obj)
    //{
    //    // set the gameObject to inactive
    //    obj.SetActive(false);

    //    // Check if the object has a parent
    //    if (obj.transform.parent != null)
    //    {
    //        // Recursively destroy the parent object
    //        InactiveRecursive(obj.transform.parent.gameObject);
    //    }
    //}

    ////a utility method used to set the parents of a gameObject to active
    //public void ActiveRecursive(GameObject obj)
    //{
    //    // set the gameObject to inactive
    //    obj.SetActive(true);

    //    // Check if the object has a parent
    //    if (obj.transform.parent != null)
    //    {
    //        // Recursively destroy the parent object
    //        InactiveRecursive(obj.transform.parent.gameObject);
    //    }
    //}
}