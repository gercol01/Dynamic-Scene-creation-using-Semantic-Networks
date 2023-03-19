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
    public Button cameraButton; //to access the camera menu
    public Button minCameraButton; //to minimise the camera menu

    //collision box button
    public Button collisionBoxButton; //button used to enable/disable the collision boxes

    //cameras
    public Camera topCamera;
    public Camera frontCamera;
    public Camera backCamera;
    public Camera leftCamera;
    public Camera rightCamera;

    //GUI elements
    GameObject Menu;
    GameObject CameraMenu;
    GameObject maxButton;
    GameObject minButton;

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

        CameraMenu = GameObject.Find("CameraCanvas");
        CameraMenu.SetActive(false);

        //initialising the front camera as the default camera
        frontCamera.enabled = true;

        //setting the cameras to false
        topCamera.enabled = false;
        leftCamera.enabled = false;
        rightCamera.enabled = false;
        backCamera.enabled = false;
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

            Quaternion rotation = Quaternion.Euler(0f, temporaryNode.getRotation(), 0f); // Rotate on the Y-axis

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
                    enemyCube.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyCube, coordinates, rotation);
                }
                //if the object type is a table
                else if (string.Equals(objectType1, "Table", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    table.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(table, coordinates, rotation);
                }
                //if the object type is a table
                else if (string.Equals(objectType1, "Fridge", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    fridge.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(fridge, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Armchair", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    armchair.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(armchair, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Bed", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    bed.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(bed, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Carpet", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    carpet.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(carpet, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Chair", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    chair.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(chair, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Cup", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    cup.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(cup, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Lamp", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    lamp.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(lamp, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Nightstand", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    nightstand.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(nightstand, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Oven", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    oven.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(oven, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Sofa", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    sofa.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(sofa, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Vase", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    vase.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(vase, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "wallhorizontal", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    wallHorizontal.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(wallHorizontal, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "wallvertical", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    wallVertical.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(wallVertical, coordinates, rotation);
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
                    enemyCube.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(cube, coordinates, rotation);
                }
                //if the object type is a table
                else if (string.Equals(objectType1, "Table", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    table.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(table, coordinates, rotation);
                }
                //if the object type is a table
                else if (string.Equals(objectType1, "Fridge", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    fridge.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(fridge, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Armchair", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    armchair.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(armchair, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Bed", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    bed.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(bed, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Carpet", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    carpet.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(carpet, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Chair", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    chair.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(chair, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Cup", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    cup.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(cup, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Lamp", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    lamp.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(lamp, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Nightstand", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    nightstand.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(nightstand, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Oven", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    oven.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(oven, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Sofa", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    sofa.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(sofa, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Vase", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    vase.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(vase, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "wallhorizontal", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    wallHorizontal.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(wallHorizontal, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "wallvertical", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    wallVertical.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(wallVertical, coordinates, rotation);
                }
            }

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

            Quaternion rotation = Quaternion.Euler(0f, temporaryNode.getRotation(), 0f); // Rotate on the Y-axis

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
                    enemyCube.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyCube, coordinates, rotation);
                }
                //if the object type is a table
                else if (string.Equals(objectType1, "Table", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyTable.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyTable, coordinates, rotation);
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
                    enemyCube.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyCube, coordinates, rotation);
                }
                //if the object type is a table
                else if (string.Equals(objectType1, "Table", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyTable.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyTable, coordinates, rotation);
                }
                //if the object type is a table
                else if (string.Equals(objectType1, "Fridge", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyFridge.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyFridge, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Armchair", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyArmchair.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyArmchair, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Bed", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyBed.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyBed, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Carpet", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyCarpet.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyCarpet, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Chair", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyChair.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyChair, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Cup", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyCup.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyCup, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Lamp", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyLamp.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyLamp, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Nightstand", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyNightstand.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyNightstand, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Oven", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyOven.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyOven, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Sofa", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemySofa.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemySofa, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "Vase", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyVase.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyVase, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "wallhorizontal", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyWallHorizontal.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyWallHorizontal, coordinates, rotation);
                }
                else if (string.Equals(objectType1, "wallvertical", StringComparison.CurrentCultureIgnoreCase))
                {
                    //giving the table the name
                    enemyWallVertical.name = name;

                    //spawn the parent object with specified coordinates
                    Instantiate(enemyWallVertical, coordinates, rotation);
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
        minCameraButton.onClick.AddListener(() => maximiseUI());
        viewButton.onClick.AddListener(() => ViewSemanticNetwork());

        saveSceneButton.onClick.AddListener(() => saveScene());
        loadSceneButton.onClick.AddListener(() => loadScene());

        topCameraButton.onClick.AddListener(() => SwitchTopCamera());
        frontCameraButton.onClick.AddListener(() => SwitchFrontCamera());
        backCameraButton.onClick.AddListener(() => SwitchBackCamera());
        leftCameraButton.onClick.AddListener(() => SwitchLeftCamera());
        rightCameraButton.onClick.AddListener(() => SwitchRightCamera());

        cameraButton.onClick.AddListener(() => getCameras());

        collisionBoxButton.onClick.AddListener(() => ChangeCollisionBoxStatus());
    }

    public void SwitchTopCamera()
    {
        // Toggle the enabled state of the cameras
        frontCamera.enabled = false;

        //setting the cameras to false
        topCamera.enabled = true;
        leftCamera.enabled = false;
        rightCamera.enabled = false;
        backCamera.enabled = false;
    }

    public void SwitchFrontCamera()
    {
        // Toggle the enabled state of the cameras
        frontCamera.enabled = true;

        //setting the cameras to false
        topCamera.enabled = false;
        leftCamera.enabled = false;
        rightCamera.enabled = false;
        backCamera.enabled = false;
    }

    public void SwitchBackCamera()
    {
        // Toggle the enabled state of the cameras
        frontCamera.enabled = false;

        //setting the cameras to false
        topCamera.enabled = false;
        leftCamera.enabled = false;
        rightCamera.enabled = false;
        backCamera.enabled = true;
    }

    public void SwitchLeftCamera()
    {
        // Toggle the enabled state of the cameras
        frontCamera.enabled = false;

        //setting the cameras to false
        topCamera.enabled = false;
        leftCamera.enabled = true;
        rightCamera.enabled = false;
        backCamera.enabled = false;
    }

    public void SwitchRightCamera()
    {
        // Toggle the enabled state of the cameras
        frontCamera.enabled = false;

        //setting the cameras to false
        topCamera.enabled = false;
        leftCamera.enabled = false;
        rightCamera.enabled = true;
        backCamera.enabled = false;
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

            //if the command is Rotate, flag to 5
            else if (String.Equals(words[0], "Rotate", StringComparison.OrdinalIgnoreCase) && String.Equals(words[1], "simple", StringComparison.OrdinalIgnoreCase))
            {
                flag = 5;
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

                    //creating the parent node
                    Node parentNode = new Node(parent);

                    //setting the object type 
                    parentNode.setObjectType(objectTypeWithNo[0]);

                    //set the objectGameObject
                    if (objectTypeWithNo[0].Equals("table", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("table");
                    }
                    else if (objectTypeWithNo[0].Equals("sofa", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("sofa");
                    }
                    else if (objectTypeWithNo[0].Equals("cup", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("cup");
                    }
                    else if (objectTypeWithNo[0].Equals("chair", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("chair");
                    }
                    else if (objectTypeWithNo[0].Equals("fridge", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("fridge");
                    }
                    else if (objectTypeWithNo[0].Equals("bed", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("bed");
                    }
                    else if (objectTypeWithNo[0].Equals("armchair", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("armchair");
                    }
                    else if (objectTypeWithNo[0].Equals("carpet", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("carpet");
                    }
                    else if (objectTypeWithNo[0].Equals("lamp", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("lamp");
                    }
                    else if (objectTypeWithNo[0].Equals("vase", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("vase");
                    }
                    else if (objectTypeWithNo[0].Equals("nightstand", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("nightstand");
                    }
                    else if (objectTypeWithNo[0].Equals("oven", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("oven");
                    }
                    else if (objectTypeWithNo[0].Equals("wallvertical", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("wallvertical");
                    }
                    else if (objectTypeWithNo[0].Equals("wallhorizontal", StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode.setObject("wallhorizontal");
                    }

                    //set the initial rotation to 0
                    parentNode.setRotation(0);

                    //setting the coordinates
                    parentNode.setCoordinates(coordinatesXYZ.Select(float.Parse).ToArray());

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

            //if the command is a Create command and consists of 4 words, that means that it is a single relationship, ex: Create Book_1 under Tree_1
            else if (words.Length == 4 && flag == 1)
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

                //set the initial rotation to 0
                childNode.setRotation(0);

                //set the objectGameObject
                if (objectTypeWithNo[0].Equals("table", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("table");
                }
                else if (objectTypeWithNo[0].Equals("sofa", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("sofa");
                }
                else if (objectTypeWithNo[0].Equals("cup", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("cup");
                }
                else if (objectTypeWithNo[0].Equals("chair", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("chair");
                }
                else if (objectTypeWithNo[0].Equals("fridge", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("fridge");
                }
                else if (objectTypeWithNo[0].Equals("bed", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("bed");
                }
                else if (objectTypeWithNo[0].Equals("armchair", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("armchair");
                }
                else if (objectTypeWithNo[0].Equals("carpet", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("carpet");
                }
                else if (objectTypeWithNo[0].Equals("lamp", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("lamp");
                }
                else if (objectTypeWithNo[0].Equals("vase", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("vase");
                }
                else if (objectTypeWithNo[0].Equals("nightstand", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("nightstand");
                }
                else if (objectTypeWithNo[0].Equals("oven", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("oven");
                }
                else if (objectTypeWithNo[0].Equals("wallvertical", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("wallvertical");
                }
                else if (objectTypeWithNo[0].Equals("wallhorizontal", StringComparison.OrdinalIgnoreCase))
                {
                    childNode.setObject("wallhorizontal");
                }

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

            //if the command is a Create command and consists of 6 words, that means that it is a double relationship, ex: Create Book_1 under Tree_1 and Man_1
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

                if (checkResult)
                {
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

            //if the command is a Move command and consists of 4 words, ex: Move cube_1 to 2,0,0, Move cube_1 on cube_3
            else if (words.Length == 4 && flag == 3)
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
                parentNode.setCoordinates(coordinatesXYZ.Select(float.Parse).ToArray());

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
                    foreach (Node tempNode in temp)
                    {
                        Node newNodeCopy = tempNode.Copy();
                        nodesToMove.Add(newNodeCopy);
                    }

                    //list to compare
                    List<Node> nodesToMoveCopy = new List<Node>(nodesToMove);

                    Boolean repeat = true;

                    while (repeat)
                    {
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

                    //check that the new coordinates are not the same as the old coordinates
                    GameObject obj = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //current position of the object
                    Vector3 currentPosition = obj.transform.position;

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
                                foreach (Node node in nodesToMove)
                                {
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
                                    if (GameObject.Find(originalObject + "(Clone)"))
                                    {
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

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Change color command and consists of 4 words, ex: Change cube_1 to red
            else if (words.Length == 4 && flag == 4)
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

            //if the command is a (Simple) Rotate command, ex: Rotate simple table_1 by 180
            else if (words.Length == 5 && flag == 5)
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

                    //current rotation of the object
                    Quaternion currentRotationQuaternion = obj.transform.rotation;

                    //get the rotation on the y-axis only
                    float currentRotation = currentRotationQuaternion.eulerAngles.y;

                    //normalise the current rotation, 450 same as 90
                    currentRotation = currentRotation % 360;

                    //rotation to be
                    float futureRotation = currentRotation + rotationInteger;

                    //check if they are the same
                    if (currentRotation != futureRotation) {

                        //setting the rotation
                        parentNode.setRotation(rotationInteger);

                        //create a temporary tree and recalculate the rotation
                        //create a copy of the current Tree
                        IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                        //change the rotation of the parentNode
                        changeTreeRotations(tempTree, parentNode);

                        //get the node from the temptree with coordinates, object type etc.
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //check the name of each node
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                //create a copy and add it to the list
                                parentNode = entry.Key.Copy();
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
                            objToUpdate.transform.rotation = Quaternion.Euler(0f, parentNode.getRotation(), 0f);

                            yield return new WaitForSeconds(creationDelay);

                            //find the created object
                            if (GameObject.Find(originalObject + "(Clone)"))
                            {
                                GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                objToMove.transform.rotation = Quaternion.Euler(0f, parentNode.getRotation(), 0f);

                                yield return new WaitForSeconds(creationDelay);
                            }

                            //update the Tree
                            TreeSet = tempTree;
                        }
                        else
                        {
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

            //if the command is a (Compound) Rotate command, ex: Rotate compound table_1 by 180
            else if (words.Length == 5 && flag == 6)
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
                    float futureRotation = currentRotation + rotationInteger;

                    //check if they are the same
                    if (currentRotation != futureRotation)
                    {

                        //setting the rotation
                        parentNode.setRotation(rotationInteger);

                        //create a temporary tree and recalculate the rotation
                        //create a copy of the current Tree
                        IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                        //change the rotation of the parentNode
                        changeTreeRotations(tempTree, parentNode);

                        //calculate the new rotations
                        setRotations(tempTree);

                        //List to store all the nodes involved
                        List<Node> nodesToRotate = new List<Node>();

                        //add the parentNode
                        foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                        {
                            //check the name of each node
                            if (entry.Key.ToString().Equals(parentNode.ToString()))
                            {
                                //create a copy and add it to the list
                                Node newNodeCopy = entry.Key.Copy();
                                nodesToRotate.Add(newNodeCopy);
                            }
                        }

                        //get all the objects involved through iteration

                        //start with the children of the parentNode
                        Node[] temp = tempTree[parentNode];

                        //getting the first nodes from the array
                        foreach (Node tempNode in temp)
                        {
                            Node newNodeCopy = tempNode.Copy();
                            nodesToRotate.Add(newNodeCopy);
                        }

                        //list to compare
                        List<Node> nodesToMoveCopy = new List<Node>(nodesToRotate);

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
                                        Node newNodeCopy = tempNode.Copy();
                                        nodesToMoveCopy.Add(newNodeCopy);//add the new children (if any)
                                    }

                                }
                            }

                            if (nodesToRotate.Count == nodesToMoveCopy.Count)
                            {
                                repeat = false;
                            }
                            else
                            {
                                //update the list with the new Nodes
                                nodesToRotate = new List<Node>(nodesToMoveCopy);
                                repeat = true;
                            }
                        }


                        Boolean create = true;

                        //loop through the nodes to be moved

                        foreach (Node node in nodesToRotate)
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
                            foreach (Node node in nodesToRotate)
                            {
                                GameObject objToDelete = GameObject.Find(node.ToString() + "temp(Clone)");

                                //destroying the object
                                Destroy(objToDelete);

                                yield return new WaitForSeconds(creationDelay);

                                //find the object
                                String originalObject = node.ToString().Replace("Copy", "");

                                GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                objToUpdate.transform.rotation = Quaternion.Euler(0f, node.getRotation(), 0f);

                                yield return new WaitForSeconds(creationDelay);

                                //find the created
                                if (GameObject.Find(originalObject + "(Clone)"))
                                {
                                    GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                    objToMove.transform.rotation = Quaternion.Euler(0f, node.getRotation(), 0f);

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


                    }
                }
                else
                {
                    placeholder.text = "Could not add node!";
                    placeholder.color = Color.red;
                }
            }

            //if the command is a Rotate command, ex: Rotate left table_1 by 90
            else if (words.Length == 5 && flag == 5)
            {
                Boolean collisions = false;

                //the parent node
                string parent = words[1];

                //creating the parent node
                Node parentNode = new Node(parent);

                //getting the 

                //getting the string of the coordinates, ex: 2,0,0
                string coordinates = words[3];

                //contains the coordinates 
                string[] coordinatesXYZ = { };

                //split the string into x, y and z coordinates
                coordinatesXYZ = coordinates.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the coordinates
                parentNode.setCoordinates(coordinatesXYZ.Select(float.Parse).ToArray());

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
                    foreach (Node tempNode in temp)
                    {
                        Node newNodeCopy = tempNode.Copy();
                        nodesToMove.Add(newNodeCopy);
                    }

                    //list to compare
                    List<Node> nodesToMoveCopy = new List<Node>(nodesToMove);

                    Boolean repeat = true;

                    while (repeat)
                    {
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

                    //check that the new coordinates are not the same as the old coordinates
                    GameObject obj = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //current position of the object
                    Vector3 currentPosition = obj.transform.position;

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
                                foreach (Node node in nodesToMove)
                                {
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
                                    if (GameObject.Find(originalObject + "(Clone)"))
                                    {
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
                    //get x coordinate
                    float x = entry.Key.returnX();
                    float y = entry.Key.returnY();
                    float z = entry.Key.returnZ();

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
                        enemyCube.name = name1 + "temp";

                        //spawn the parent object with specified coordinates
                        Instantiate(enemyCube, coordinates1, enemyCube.transform.rotation);
                    }
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Table", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Table", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Armchair", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Bed", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Carpet", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Chair", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Cup", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Fridge", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Lamp", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Nightstand", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Oven", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Sofa", StringComparison.CurrentCultureIgnoreCase))
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
                    //if the object type is a table
                    else if (string.Equals(objectType1, "Vase", StringComparison.CurrentCultureIgnoreCase))
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

                        //object does not already exist if false
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
                            float X = child.returnX();
                            float Y = child.returnY();
                            float Z = child.returnZ();

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
                                enemyCube.name = name2 + "temp";

                                //spawn the parent object with specified coordinates
                                Instantiate(enemyCube, coordinates2, enemyCube.transform.rotation);

                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Table", StringComparison.CurrentCultureIgnoreCase))
                            {

                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }


                                ////giving the cube the name
                                //table.name = name2;

                                ////spawn the parent object with specified coordinates
                                //Instantiate(table, coordinates2, table.transform.rotation);

                                ////giving the table the name
                                //enemyTable.name = name2 + "temp";

                                ////spawn the parent object with specified coordinates
                                //Instantiate(enemyTable, coordinates2, enemyTable.transform.rotation);
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Armchair", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Bed", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Carpet", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Chair", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Cup", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Fridge", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Lamp", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Nightstand", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Oven", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Sofa", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
                            }
                            //if the object type is a table
                            else if (string.Equals(objectType2, "Vase", StringComparison.CurrentCultureIgnoreCase))
                            {
                                //create the collision boxes 
                                StartCoroutine(checkSceneCollidersParent(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }

                                //create the object in real time to check for later
                                StartCoroutine(checkSceneCollidersParentTrue(child));

                                yield return new WaitForSeconds(creationDelay);

                                if (found == true)
                                {
                                    found = false;//reset the found flag
                                }
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

            //change enemy tags to 'Final'
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

    public void changeTree(IDictionary<Node, Node[]> tree, Node node) {
        //iterate through the tree
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //first thing to do is spawn the parent
            if (entry.Key.ToString().Equals(node.ToString())) {
                float x = node.returnX();
                float y = node.returnY();
                float z = node.returnZ();

                //adding the coordinates
                float[] coordinates = new float[3] { x,y,z};

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

    //used to change the rotation of the objects
    public void changeTreeRotations(IDictionary<Node, Node[]> tree, Node node)
    {
        //iterate through the tree
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //check if the current node is equal to the node needed
            if (entry.Key.ToString().Equals(node.ToString()))
            {
                float rotation = node.getRotation();

                //update the rotation of the object
                entry.Key.setRotation(rotation);
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
        float rotationParent = parent.getRotation();

        //getting the child rotation
        float rotationChild = child.getRotation();

        //update the child rotation
        rotationChild = rotationChild + rotationParent;

        //set the new child rotation
        child.setRotation(rotationChild);
    }

    public void calculatePrepositionCoordinates(Node parent, Node child)
    {
        //spacing between objects
        float spacing = 0.25f;

        //get the increments depending on the object (How much the child object must be moved irrelevant of the child object's size)
        ObjectDim currentObject = parent.getObject();

        //store the increments for height, length and width
        float height = currentObject.getHeight();
        float length = currentObject.getLength();
        float width = currentObject.getWidth();

        //child object to include the object size in the calculation of the coordinates
        ObjectDim childObject = child.getObject();

        string preposition = child.getPreposition();

        //get the coordinates of the first parent
        float coordX = parent.returnX();
        float coordY = parent.returnY();
        float coordZ = parent.returnZ();

        //creating values to store the coordinates of the child
        float coordx;
        float coordy;
        float coordz;

        //array to store the new coordinates
        float[] coordinates;

        if (preposition.Equals("left_of"))
        {
            coordx = coordX - currentObject.getLength() - childObject.getLength() - spacing; //movement on the x-axis
            coordy = coordY;
            coordz = coordZ;

            //adding the coordinates
            coordinates = new float[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("right_of"))
        {
            coordx = coordX + currentObject.getLength() + childObject.getLength() + spacing; //movement on the x-axis
            coordy = coordY;
            coordz = coordZ;

            //adding the coordinates
            coordinates = new float[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("on"))
        {
            coordx = coordX;
            coordy = coordY + currentObject.getHeight(); //movement on the y-axis
            coordz = coordZ;

            //adding the coordinates
            coordinates = new float[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("under"))
        {
            coordx = coordX;
            coordy = coordY; //movement on the y-axis
            coordz = coordZ;

            //adding the coordinates
            coordinates = new float[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("infront"))
        {
            coordx = coordX;
            coordy = coordY; //movement on the y-axis
            coordz = coordZ - currentObject.getWidth() - childObject.getWidth() - spacing;

            //adding the coordinates
            coordinates = new float[3] { coordx, coordy, coordz };

            //setting the calculated coordinates to the child
            child.setCoordinates(coordinates);
        }
        else if (preposition.Equals("behind"))
        {
            coordx = coordX;
            coordy = coordY;
            coordz = coordZ + currentObject.getWidth() + childObject.getWidth() + spacing;//movement on the z-axis

            //adding the coordinates
            coordinates = new float[3] { coordx, coordy, coordz };

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
                        string[] NameCoordinatesPrepositionRotation = KeyAndValue[i].Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

                        //setting objectType from the name, Ex: cube_1
                        string[] objectTypeWithNo = NameCoordinatesPrepositionRotation[0].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                        //setting preposition, Ex: on, null etc
                        string preposition = NameCoordinatesPrepositionRotation[2];

                        //setting rotation, Ex: 90, 180, 270
                        string rotation = NameCoordinatesPrepositionRotation[3];
                        int intRotation = int.Parse(rotation);

                        //setting coordinatesString
                        string coordinatesString = NameCoordinatesPrepositionRotation[1];
                        string[] coordinates = coordinatesString.Split(new[] { 'v' }, StringSplitOptions.RemoveEmptyEntries);
                        float[] floatCoordinates = Array.ConvertAll(coordinates, float.Parse);

                        //setting value
                        parent.setValue(NameCoordinatesPrepositionRotation[0]);
                        parent.setObjectType(objectTypeWithNo[0]);

                        //set the objectGameObject
                        if (objectTypeWithNo[0].Equals("table", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("table");
                        }
                        else if (objectTypeWithNo[0].Equals("sofa", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("sofa");
                        }
                        else if (objectTypeWithNo[0].Equals("cup", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("cup");
                        }
                        else if (objectTypeWithNo[0].Equals("chair", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("chair");
                        }
                        else if (objectTypeWithNo[0].Equals("fridge", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("fridge");
                        }
                        else if (objectTypeWithNo[0].Equals("bed", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("bed");
                        }
                        else if (objectTypeWithNo[0].Equals("armchair", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("armchair");
                        }
                        else if (objectTypeWithNo[0].Equals("carpet", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("carpet");
                        }
                        else if (objectTypeWithNo[0].Equals("lamp", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("lamp");
                        }
                        else if (objectTypeWithNo[0].Equals("vase", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("vase");
                        }
                        else if (objectTypeWithNo[0].Equals("nightstand", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("nightstand");
                        }
                        else if (objectTypeWithNo[0].Equals("oven", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("oven");
                        }
                        else if (objectTypeWithNo[0].Equals("wallvertical", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("wallvertical");
                        }
                        else if (objectTypeWithNo[0].Equals("wallhorizontal", StringComparison.OrdinalIgnoreCase))
                        {
                            parent.setObject("wallhorizontal");
                        }

                        parent.setPreposition(preposition);
                        parent.setCoordinates(floatCoordinates);
                        parent.setRotation(intRotation);

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
                            string[] NameCoordinatesPrepositionRotation = value.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

                            //setting objectType from the name, Ex: cube_1
                            string[] objectTypeWithNo = NameCoordinatesPrepositionRotation[0].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                            //setting preposition, Ex: on, null etc
                            string preposition = NameCoordinatesPrepositionRotation[2];

                            //setting rotation, Ex: 90, 180, 270
                            string rotation = NameCoordinatesPrepositionRotation[3];
                            int intRotation = int.Parse(rotation);

                            //setting coordinatesString
                            string coordinatesString = NameCoordinatesPrepositionRotation[1];
                            string[] coordinates = coordinatesString.Split(new[] { 'v' }, StringSplitOptions.RemoveEmptyEntries);
                            float[] floatCoordinates = Array.ConvertAll(coordinates, float.Parse);

                            //setting value
                            child.setValue(NameCoordinatesPrepositionRotation[0]);
                            child.setObjectType(objectTypeWithNo[0]);

                            //set the objectGameObject
                            if (objectTypeWithNo[0].Equals("table", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("table");
                            }
                            else if (objectTypeWithNo[0].Equals("sofa", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("sofa");
                            }
                            else if (objectTypeWithNo[0].Equals("cup", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("cup");
                            }
                            else if (objectTypeWithNo[0].Equals("chair", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("chair");
                            }
                            else if (objectTypeWithNo[0].Equals("fridge", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("fridge");
                            }
                            else if (objectTypeWithNo[0].Equals("bed", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("bed");
                            }
                            else if (objectTypeWithNo[0].Equals("armchair", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("armchair");
                            }
                            else if (objectTypeWithNo[0].Equals("carpet", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("carpet");
                            }
                            else if (objectTypeWithNo[0].Equals("lamp", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("lamp");
                            }
                            else if (objectTypeWithNo[0].Equals("vase", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("vase");
                            }
                            else if (objectTypeWithNo[0].Equals("nightstand", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("nightstand");
                            }
                            else if (objectTypeWithNo[0].Equals("oven", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("oven");
                            }
                            else if (objectTypeWithNo[0].Equals("wallvertical", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("wallvertical");
                            }
                            else if (objectTypeWithNo[0].Equals("wallhorizontal", StringComparison.OrdinalIgnoreCase))
                            {
                                child.setObject("wallhorizontal");
                            }

                            child.setPreposition(preposition);
                            child.setCoordinates(floatCoordinates);
                            child.setRotation(intRotation);

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