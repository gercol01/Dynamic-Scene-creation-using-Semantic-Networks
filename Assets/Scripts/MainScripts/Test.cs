using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    int x = 0;

    public Button addButton; //add button object
    public Button createButton; //create button object
    public Button viewButton; //view button object
    public Button resetButton; //view button object
    public Button minimiseButton; //minimise button object
    public Button maximiseButton; //maximise button object
    public Button saveSceneButton; //save scene object
    public Button loadSceneButton; //load scene button
    public Button undoButton; //undo action button
    public Button redoButton; //redo action button
    public Button deleteSceneButton; //delete scene button
    public Button FAQButton; //FAQ button
    public Button quitButton; //quit button

    //camera buttons
    public Button topCameraButton; //change to top camera button
    public Button frontCameraButton; //change to top camera button
    public Button backCameraButton; //change to top camera button
    public Button leftCameraButton; //change to top camera button
    public Button rightCameraButton; //change to top camera button
    public Button worldCameraButton; //change to top camera button
    public Button cameraButton; //to access the camera menu
    public Button minCameraButton; //to minimise the camera menu

    //label buttons
    public Button minLabelButton; //to minimise the label menu
    public Button labelButton; //to maximise the label menu
    public Dropdown facesDropdown; //stores the current gameobjects that can have their faces shown
    public Button registerFaces; //a button used to register the value of the dropdown

    //collision box button
    public Button collisionBoxButton; //button used to enable/disable the collision boxes
    public Button nameButton; //button used to enable/disable names

    //rotation axis button
    public Button axisButton; //button used to enable/disable the rotation axis

    //guide button
    public Button guideButton; 
    public Button axisButtonX;
    public Button axisButtonY;
    public Button axisButtonZ;
    public Button front;
    public Button behind;
    public Button left;
    public Button right;
    public Button minimiseGuides;

    //cameras
    public GameObject guideMenu;
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

    //global axis guidelines
    public GameObject leftGuide;
    public GameObject rightGuide;
    public GameObject frontGuide;
    public GameObject behindGuide;

    //GUI elements
    public Canvas Menu;
    GameObject CameraMenu;
    GameObject LabelMenu;
    GameObject maxButton;
    GameObject minButton;

    //flags to indicate if certain GUI elements show or not
    protected Boolean nameAndCoordinates;
    protected Boolean faces;

    protected Boolean rotationAxisX = false;
    protected Boolean rotationAxisY = false;
    protected Boolean rotationAxisZ = false;

    protected Boolean axisFront = false;
    protected Boolean axisBehind = false;
    protected Boolean axisLeft = false;
    protected Boolean axisRight = false;

    //materials
    public Material Fabric;
    public Material MetalDark;
    public Material MetalLight;
    public Material WoodDark;
    public Material WoodLight;
    public Material UtilityMaterial;
    public Material Brick;

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
    List<string> colorList = new List<string> { "wood_dark", "wood_light", "metal_dark", "metal_light", "fabric", "brick"};

    //list of all the objects
    List<string> objectList = new List<string> { "table", "chair", "sofa", "fridge", "armchair" , "vase", "oven", "lamp", "bed", "cup", "nightstand", "carpet", "walls"};

    //list of all the objects
    List<string> objectListNoWalls = new List<string> { "table", "chair", "sofa", "fridge", "armchair", "vase", "oven", "lamp", "bed", "cup", "nightstand", "carpet"};

    //list of the allowed directions for move local global
    List<string> directionList = new List<string> { "forward", "back", "left", "right" };

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
    public GameObject enemyWalls; //wall object
    public GameObject enemyWall1; //wall object
    public GameObject enemyWall2; //wall object

    private GameObject[] gameObjects; //a list of all the current Object GameObjects in the scene
    public Material materialCollisionBox; //the collision box material

    private Boolean collisionBoxFlag = true; //the collision box flag
    private Boolean nameFlag = false; //the collision box flag
    public IDictionary<string, Boolean> faceTree = new Dictionary<string, Boolean>();


    //Placeholder field
    public Text placeholder;

    //Canvas UI
    //public Canvas MainUI;

    //Input field
    public InputField commandBox;

    //Display canvas
    public Text outputCommands;

    public Dropdown dropDown1;
    public Dropdown dropDown2;
    public Dropdown dropDown3;
    public Dropdown dropDown4;
    public Dropdown dropDown5;

    public TMP_InputField inputField3x;
    public TMP_InputField inputField3y;
    public TMP_InputField inputField3z;
    public InputField inputField4;
    public InputField inputField5;
    public TMP_InputField inputField5x;
    public TMP_InputField inputField5y;
    public TMP_InputField inputField5z;
    public InputField inputField6;

    public TMP_InputField test;

    private int inputFlag3 = -1;
    private int inputFlag5 = -1;
    private Boolean selectedFlag3 = false;
    private Boolean selectedFlag5 = false;

    private List<string> commandOptions = new List<string>{ "Add", "Relate", "Delete", "Move", "MoveBy", "Texture", "RotateX", "RotateY", "RotateZ", "Break"};
    private List<string> prepositionOptions = new List<string> { "on", "under", "left_of", "right_of", "infront", "behind" };
    private List<string> prepositionOptionsExtended = new List<string> { "to", "on", "under", "left_of", "right_of", "infront", "behind" };
    private List<string> MoveByOptionsList = new List<string> { "left", "right", "front", "behind"};
    private List<string> MoveByList = new List<string> { "global_axis", "object_axis" };
    private List<string> RotateYList = new List<string> { "simple", "compound" };
    private List<string> RotateXZList = new List<string> { "simple" };
    private List<string> DegreesAllowed = new List<string> { "90", "180", "270", "-90", "-180", "-270"};
    private List<string> currentObjects = new List<string>();
    private List<string> SpacingList = new List<string> { "near", "touching", "far", "moderate" };
    List<int> objectCounters = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // initialize the counters with zeros for the objects

    private List<object> uiElements = new List<object>();

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

    //stack used for undo action
    Stack<IDictionary<Node, Node[]>> undoStack = new Stack<IDictionary<Node, Node[]>>();

    //stack used for redo action
    Stack<IDictionary<Node, Node[]>> redoStack = new Stack<IDictionary<Node, Node[]>>();

    //Update method parameters
    public float creationDelay = 2.0f; // delay between object creation
    private bool canCreate = false; // flag to check if the object can be created
    private bool canCreateTrue = false; // flag to check if the true object can be created
    private bool found = false;// flag used to check if the object has been destroyed or not
    public Node temporaryNode;//used in the update method

    //create a deep copy of the tree
    private void DeepCopyTree(IDictionary<Node, Node[]> treeCopy, IDictionary<Node, Node[]> tree) {
        Test graphObject = new Test();

        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            Node key = entry.Key.CopyDeep();

            Boolean resultExist = graphObject.checkExist(treeCopy, key);

            if (!resultExist)
            {
                graphObject.AddObjectsToDatastructure(treeCopy, key, null);
            }

            Node[] values = entry.Value;

            foreach (Node value in values)
            {
                Node nodeValue = value.CopyDeep();

                graphObject.AddObjectsToDatastructure(treeCopy, key, nodeValue);
            }
        }
    }

    //returns the index of the object
    private int GetIndexForObject(string objectType)
    {
        if (objectType.Equals("table")) {
            objectCounters[0]++;
            return objectCounters[0];
        }
        else if (objectType.Equals("chair"))
        {
            objectCounters[1]++;
            return objectCounters[1];
        }
        else if (objectType.Equals("sofa"))
        {
            objectCounters[2]++;
            return objectCounters[2];
        }
        else if (objectType.Equals("fridge"))
        {
            objectCounters[3]++;
            return objectCounters[3];
        }
        else if (objectType.Equals("armchair"))
        {
            objectCounters[4]++;
            return objectCounters[4];
        }
        else if (objectType.Equals("vase"))
        {
            objectCounters[5]++;
            return objectCounters[5];
        }
        else if (objectType.Equals("oven"))
        {
            objectCounters[6]++;
            return objectCounters[6];
        }
        else if (objectType.Equals("lamp"))
        {
            objectCounters[7]++;
            return objectCounters[7];
        }
        else if (objectType.Equals("bed"))
        {
            objectCounters[8]++;
            return objectCounters[8];
        }
        else if (objectType.Equals("cup"))
        {
            objectCounters[9]++;
            return objectCounters[9];
        }
        else if (objectType.Equals("nightstand"))
        {
            objectCounters[10]++;
            return objectCounters[10];
        }
        else if (objectType.Equals("carpet"))
        {
            objectCounters[11]++;
            return objectCounters[11];
        }
        else if (objectType.Equals("wall1"))
        {
            objectCounters[12]++;
            return objectCounters[12];
        }
        else if (objectType.Equals("wall2"))
        {
            objectCounters[13]++;
            return objectCounters[13];
        }

        return -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        minButton = GameObject.Find("MinimiseButton");
        maxButton = GameObject.Find("MaximiseButton");
        maxButton.SetActive(false);

        //setting the camera menu to inactive
        CameraMenu = GameObject.Find("CameraCanvas");
        CameraMenu.SetActive(false);

        //setting the label menu to inactive
        LabelMenu = GameObject.Find("LabelCanvas");
        LabelMenu.SetActive(false);

        //setting the guide menu to inactive
        guideMenu.SetActive(false);

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

        //Adding the ui elements to the list
        uiElements.Add(dropDown1);
        uiElements.Add(dropDown2);
        uiElements.Add(dropDown3);
        uiElements.Add(inputField3x);
        uiElements.Add(inputField3y);
        uiElements.Add(inputField3z);
        uiElements.Add(inputField4);
        uiElements.Add(dropDown4);
        uiElements.Add(inputField5);
        uiElements.Add(dropDown5);
        uiElements.Add(inputField5x);
        uiElements.Add(inputField5y);
        uiElements.Add(inputField5z);
        uiElements.Add(inputField6);

        //command system setup -- set to Add
        dropDown1.gameObject.SetActive(true);
        SetOptions(commandOptions, dropDown1); //set the option
        dropDown1.value = 0;//set to add option

        dropDown2.gameObject.SetActive(true);
        SetOptions(objectList, dropDown2); //set the option
        dropDown2.value = 0;//set to add option

        inputField3x.gameObject.SetActive(true);
        inputField3y.gameObject.SetActive(true);
        inputField3z.gameObject.SetActive(true);

        //disable the other UI elements
        inputField4.gameObject.SetActive(false);
        inputField5x.gameObject.SetActive(false);
        inputField5y.gameObject.SetActive(false);
        inputField5z.gameObject.SetActive(false);
        inputField5.gameObject.SetActive(false);
        inputField6.gameObject.SetActive(false);
        dropDown3.gameObject.SetActive(false);
        dropDown4.gameObject.SetActive(false);
        dropDown5.gameObject.SetActive(false);

        //add listeners required
        dropDown1.onValueChanged.AddListener(OnDropdownValueChangedCommand); //because of the various commands
        dropDown2.onValueChanged.AddListener(OnDropdownValueChangedSimpleOrCompound); //because of the 'simple'
        dropDown3.onValueChanged.AddListener(OnDropdownValueChangedPreposition); //because of 'between'
        dropDown4.onValueChanged.AddListener(OnDropdownValueChangedPrepositionExtended); //because of 'to'

        //call method
        inputField3x.onSelect.AddListener(OnInputField3x);
        inputField3y.onSelect.AddListener(OnInputField3y);
        inputField3z.onSelect.AddListener(OnInputField3z);

        inputField5x.onSelect.AddListener(OnInputField5x);
        inputField5y.onSelect.AddListener(OnInputField5y);
        inputField5z.onSelect.AddListener(OnInputField5z);

        //after everything
        inputField3x.onDeselect.AddListener(selectedFlagFalse);
        inputField3x.onDeselect.AddListener(selectedFlagFalse);
        inputField3x.onDeselect.AddListener(selectedFlagFalse);

        inputField5x.onDeselect.AddListener(selectedFlagFalse);
        inputField5x.onDeselect.AddListener(selectedFlagFalse);
        inputField5x.onDeselect.AddListener(selectedFlagFalse);
    }

    private void selectedFlagFalse(string value)
    {
        selectedFlag3 = false;
        selectedFlag5 = false;
    }

    private void OnInputField3x(string value)
    {
        inputFlag3 = 0;
        selectedFlag3 = true;
    }
    private void OnInputField3y(string value)
    {
        inputFlag3 = 1;
        selectedFlag3 = true;
    }
    private void OnInputField3z(string value)
    {
        inputFlag3 = 2;
        selectedFlag3 = true;
    }
    private void OnInputField5x(string value)
    {
        inputFlag5 = 0;
        selectedFlag5 = true;
    }
    private void OnInputField5y(string value)
    {
        inputFlag5 = 1;
        selectedFlag5 = true;
    }
    private void OnInputField5z(string value)
    {
        inputFlag5 = 2;
        selectedFlag5 = true;
    }

    private void selectInputField3(int InputFieldNo) {
        if (InputFieldNo == 0) {
            inputField3x.Select();
        }
        else if (InputFieldNo == 1) {
            inputField3y.Select();
        }
        else if (InputFieldNo == 2)
        {
            inputField3z.Select();
        }
    }

    private void selectInputField5(int InputFieldNo)
    {
        if (InputFieldNo == 0)
        {
            inputField5x.Select();
        }
        else if (InputFieldNo == 1)
        {
            inputField5y.Select();
        }
        else if (InputFieldNo == 2)
        {
            inputField5z.Select();
        }
    }

    private void SetOptions(List<string> options, Dropdown dropdown)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    private void OnDropdownValueChangedCommand(int optionIndex)
    {
        switch (optionIndex)
        {
            case 0: //Add
                dropDown1.gameObject.SetActive(true);

                dropDown2.gameObject.SetActive(true);
                SetOptions(objectList, dropDown2); //set the option
                dropDown2.value = 0;//set to add option

                inputField3x.gameObject.SetActive(true);
                inputField3y.gameObject.SetActive(true);
                inputField3z.gameObject.SetActive(true);

                //disable the others
                inputField4.gameObject.SetActive(false);
                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
                inputField5.gameObject.SetActive(false);
                dropDown3.gameObject.SetActive(false);
                dropDown4.gameObject.SetActive(false);
                dropDown5.gameObject.SetActive(false);
                break;
            case 1: //Create
                dropDown1.gameObject.SetActive(true);

                dropDown2.gameObject.SetActive(true);
                SetOptions(objectListNoWalls, dropDown2); //set the option
                dropDown2.value = 0;//set to add option

                dropDown3.gameObject.SetActive(true);
                SetOptions(prepositionOptions, dropDown3); //set the option
                dropDown3.value = 0;//set to first option

                dropDown4.gameObject.SetActive(true);
                currentObjects = getCurrentObjects();
                //if the currentObjects list is empty
                if (currentObjects.Count == 0)
                {
                    currentObjects.Add("null");
                }
                SetOptions(currentObjects, dropDown4); //set the option
                dropDown4.value = 0;//set to first option

                inputField5.gameObject.SetActive(true); //distance

                //disable the other elements
                inputField3x.gameObject.SetActive(false);
                inputField3y.gameObject.SetActive(false);
                inputField3z.gameObject.SetActive(false);
                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
                inputField4.gameObject.SetActive(false);
                dropDown5.gameObject.SetActive(false);
                inputField6.gameObject.SetActive(false);
                break;
            case 2: //Delete
                dropDown1.gameObject.SetActive(true);
                dropDown2.gameObject.SetActive(true); //remember to change
                currentObjects = getCurrentObjects();
                //if the currentObjects list is empty
                if (currentObjects.Count == 0)
                {
                    currentObjects.Add("null");
                }
                SetOptions(currentObjects, dropDown2); //set the option
                dropDown2.value = 0;//set to add option

                //disable the others
                dropDown3.gameObject.SetActive(false);
                dropDown4.gameObject.SetActive(false);
                inputField5.gameObject.SetActive(false);
                inputField3x.gameObject.SetActive(false);
                inputField3y.gameObject.SetActive(false);
                inputField3z.gameObject.SetActive(false);
                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
                inputField4.gameObject.SetActive(false);
                dropDown5.gameObject.SetActive(false);
                inputField6.gameObject.SetActive(false);
                break;
            case 3: //Move
                dropDown1.gameObject.SetActive(true);

                dropDown2.gameObject.SetActive(true);
                SetOptions(RotateYList, dropDown2); //set the option
                dropDown2.value = 0;//set to simple option

                dropDown3.gameObject.SetActive(true);
                currentObjects = getCurrentObjectsNoWalls();
                //if the currentObjects list is empty
                if (currentObjects.Count == 0)
                {
                    currentObjects.Add("null");
                }
                SetOptions(currentObjects, dropDown3); //set the option -- remember to change
                dropDown3.value = 0;//set to simple option

                dropDown4.gameObject.SetActive(true);
                SetOptions(prepositionOptionsExtended, dropDown4); //set the option -- remember to change
                dropDown4.value = 0;//set to simple option

                inputField5x.gameObject.SetActive(true);
                inputField5y.gameObject.SetActive(true);
                inputField5z.gameObject.SetActive(true);

                //disable the others
                inputField5.gameObject.SetActive(false);
                inputField3x.gameObject.SetActive(false);
                inputField3y.gameObject.SetActive(false);
                inputField3z.gameObject.SetActive(false);
                inputField4.gameObject.SetActive(false);
                dropDown5.gameObject.SetActive(false);
                inputField6.gameObject.SetActive(false);
                break;
            case 4: //MoveBy
                dropDown1.gameObject.SetActive(true);

                dropDown2.gameObject.SetActive(true);
                SetOptions(MoveByList, dropDown2); //set the option, global_axis, object_axis
                dropDown2.value = 0;//set to simple option

                dropDown3.gameObject.SetActive(true);
                SetOptions(RotateYList, dropDown3); //set the option, simple, compound
                dropDown3.value = 0;//set to simple option

                dropDown4.gameObject.SetActive(true);
                currentObjects = getCurrentObjectsNoWalls();
                //if the currentObjects list is empty
                if (currentObjects.Count == 0)
                {
                    currentObjects.Add("null");
                }
                SetOptions(currentObjects, dropDown4); //set the option -- remember to change
                dropDown4.value = 0;//set to simple option

                dropDown5.gameObject.SetActive(true);
                SetOptions(MoveByOptionsList, dropDown5); //set the option -- remember to change
                dropDown5.value = 0;//set to simple option

                inputField6.gameObject.SetActive(true);

                //disable the others
                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
                inputField5.gameObject.SetActive(false);
                inputField3x.gameObject.SetActive(false);
                inputField3y.gameObject.SetActive(false);
                inputField3z.gameObject.SetActive(false);
                inputField4.gameObject.SetActive(false);
                break;
            case 5: //Texture
                dropDown1.gameObject.SetActive(true);

                dropDown2.gameObject.SetActive(true);
                currentObjects = getCurrentObjects();
                //if the currentObjects list is empty
                if (currentObjects.Count == 0)
                {
                    currentObjects.Add("null");
                }
                SetOptions(currentObjects, dropDown2); //set the option
                dropDown2.value = 0;//set to simple option

                dropDown3.gameObject.SetActive(true);
                SetOptions(colorList, dropDown3); //set the option -- remember to change
                dropDown3.value = 0;//set to simple option

                //disable the others
                dropDown4.gameObject.SetActive(false);
                inputField5.gameObject.SetActive(false);
                inputField3x.gameObject.SetActive(false);
                inputField3y.gameObject.SetActive(false);
                inputField3z.gameObject.SetActive(false);
                inputField4.gameObject.SetActive(false);
                dropDown5.gameObject.SetActive(false);
                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
                break;
            case 6: //RotateX
                dropDown1.gameObject.SetActive(true);

                dropDown2.gameObject.SetActive(true);
                SetOptions(RotateXZList, dropDown2); //set the option -- simple
                dropDown2.value = 0;//set to simple option

                dropDown3.gameObject.SetActive(true);
                currentObjects = getCurrentObjectsNoWalls();
                //if the currentObjects list is empty
                if (currentObjects.Count == 0)
                {
                    currentObjects.Add("null");
                }
                SetOptions(currentObjects, dropDown3);//set the option -- remember to change
                dropDown3.value = 0;//set to simple option

                dropDown4.gameObject.SetActive(true);
                SetOptions(DegreesAllowed, dropDown4);//90, 180, 270 etc...
                dropDown4.value = 0;//set to simple option

                //disable the others
                inputField5.gameObject.SetActive(false);
                inputField3x.gameObject.SetActive(false);
                inputField3y.gameObject.SetActive(false);
                inputField3z.gameObject.SetActive(false);
                inputField4.gameObject.SetActive(false);
                dropDown5.gameObject.SetActive(false);
                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
                inputField6.gameObject.SetActive(false);
                break;
            case 7: //RotateY
                dropDown1.gameObject.SetActive(true);

                dropDown2.gameObject.SetActive(true);
                SetOptions(RotateYList, dropDown2);//set the option -- simple or compound
                dropDown2.value = 0;//set to simple option

                dropDown3.gameObject.SetActive(true);
                currentObjects = getCurrentObjectsNoWalls();
                //if the currentObjects list is empty
                if (currentObjects.Count == 0)
                {
                    currentObjects.Add("null");
                }
                SetOptions(currentObjects, dropDown3);//set the option -- remember to change
                dropDown3.value = 0;//set to simple option

                inputField4.gameObject.SetActive(true);//to input the custom rotation angle

                //disable the others
                inputField5.gameObject.SetActive(false);
                inputField3x.gameObject.SetActive(false);
                inputField3y.gameObject.SetActive(false);
                inputField3z.gameObject.SetActive(false);
                dropDown4.gameObject.SetActive(false);
                dropDown5.gameObject.SetActive(false);
                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
                inputField6.gameObject.SetActive(false);
                break;
            case 8: //RotateZ
                dropDown1.gameObject.SetActive(true);

                dropDown2.gameObject.SetActive(true);
                SetOptions(RotateXZList, dropDown2);//set the option -- simple
                dropDown2.value = 0;//set to simple option

                dropDown3.gameObject.SetActive(true);
                currentObjects = getCurrentObjectsNoWalls();
                //if the currentObjects list is empty
                if (currentObjects.Count == 0)
                {
                    currentObjects.Add("null");
                }
                SetOptions(currentObjects, dropDown3);//set the option -- remember to change
                dropDown3.value = 0;//set to simple option

                dropDown4.gameObject.SetActive(true);
                SetOptions(DegreesAllowed, dropDown4); //90, 180, 270 etc...
                dropDown4.value = 0;//set to simple option

                //disable the others
                inputField5.gameObject.SetActive(false);
                inputField3x.gameObject.SetActive(false);
                inputField3y.gameObject.SetActive(false);
                inputField3z.gameObject.SetActive(false);
                inputField4.gameObject.SetActive(false);
                dropDown5.gameObject.SetActive(false);
                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
                inputField6.gameObject.SetActive(false);
                break;
            case 9: //Break
                dropDown1.gameObject.SetActive(true);
                dropDown2.gameObject.SetActive(true); //remember to change
                currentObjects = getCurrentObjects();
                //if the currentObjects list is empty
                if (currentObjects.Count == 0)
                {
                    currentObjects.Add("null");
                }
                SetOptions(currentObjects, dropDown2); //set the option
                dropDown2.value = 0;//set to add option

                //disable the others
                dropDown3.gameObject.SetActive(false);
                dropDown4.gameObject.SetActive(false);
                inputField5.gameObject.SetActive(false);
                inputField3x.gameObject.SetActive(false);
                inputField3y.gameObject.SetActive(false);
                inputField3z.gameObject.SetActive(false);
                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
                inputField4.gameObject.SetActive(false);
                dropDown5.gameObject.SetActive(false);
                inputField6.gameObject.SetActive(false);
                break;
            default: //Error
                // do nothing
                break;
        }
    }

    private void OnDropdownValueChangedSimpleOrCompound(int optionIndex)
    {
        //if RotateY
        if (dropDown1.value.ToString().Equals("RotateY")) { 
        
        }
    }

    //used to differentiate Create Book_1 under Tree_1 1, Create Book_1 between Tree_1 and Tree_2
    private void OnDropdownValueChangedPreposition(int optionIndex)
    {
        if (commandOptions[dropDown1.value].Equals("Relate")) { 
            if (dropDown3.options[optionIndex].text == "between")
            {
                dropDown5.gameObject.SetActive(true);

                currentObjects = getCurrentObjects();
                //check if list is empty
                if (currentObjects.Count != 0)
                {
                    //cannot relate between the same 2 objects
                    currentObjects.Remove(currentObjects[dropDown4.value]);
                    //if the currentObjects list is empty
                    if (currentObjects.Count == 0)
                    {
                        currentObjects.Add("null");
                    }
                }
                else
                {
                    currentObjects.Add("null");
                }

                SetOptions(currentObjects, dropDown5); //set the option -- remember to change
                dropDown5.value = 0;//set to first option

                inputField5.gameObject.SetActive(false);
            }
            else {
                inputField5.gameObject.SetActive(true);

                dropDown5.gameObject.SetActive(false);
            }
        }
    }

    //used to differentiate Move simple cube_1 to 2,0,0 from Move simple cube_2 on cube_1
    private void OnDropdownValueChangedPrepositionExtended(int optionIndex)
    {
        if (commandOptions[dropDown1.value].Equals("Move")) {
            if (dropDown4.options[optionIndex].text == "to")
            {
                inputField5x.gameObject.SetActive(true);
                inputField5y.gameObject.SetActive(true);
                inputField5z.gameObject.SetActive(true);

                dropDown5.gameObject.SetActive(false);
                inputField6.gameObject.SetActive(false);
            }
            else
            {
                dropDown5.gameObject.SetActive(true);
                currentObjects = getCurrentObjects();
                //check if list is empty
                if (currentObjects.Count != 0)
                {
                    //cannot move object on itself
                    currentObjects.Remove(currentObjects[dropDown3.value]);
                    //if the currentObjects list is empty
                    if (currentObjects.Count == 0)
                    {
                        currentObjects.Add("null");
                    }
                }
                else {
                    currentObjects.Add("null");
                }
                
                SetOptions(currentObjects, dropDown5); //set the option -- remember to change
                dropDown5.value = 0;//set to first option
                inputField6.gameObject.SetActive(true);

                inputField5x.gameObject.SetActive(false);
                inputField5y.gameObject.SetActive(false);
                inputField5z.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //inputfield is selected
        if ((selectedFlag3 == true && Input.GetKeyDown(KeyCode.Tab)) || (selectedFlag5 == true && Input.GetKeyDown(KeyCode.Tab)))
        {
            if (selectedFlag3) {
                inputFlag3 = inputFlag3 + 1;
                if (inputFlag3 > 2) inputFlag3 = 0;

                //call method to select next inputfield
                selectInputField3(inputFlag3);
            }
            if (selectedFlag5)
            {
                inputFlag5 = inputFlag5 + 1;
                if (inputFlag5 > 2) inputFlag5 = 0;

                //call method to select next inputfield
                selectInputField5(inputFlag5);
            }
        }

        if (x==1) {
            UnityEngine.Debug.Log("Update");
            x = 0;
        }

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
        minLabelButton.onClick.AddListener(() => maximiseUI());
        viewButton.onClick.AddListener(() => ViewSemanticNetwork());
        undoButton.onClick.AddListener(() => undo());
        redoButton.onClick.AddListener(() => redo());
        deleteSceneButton.onClick.AddListener(() => deleteScene());
        quitButton.onClick.AddListener(() => Quit());

        //Save & Load Buttons
        saveSceneButton.onClick.AddListener(() => saveScene());
        loadSceneButton.onClick.AddListener(() => loadScene());

        //Label Menu
        labelButton.onClick.AddListener(() => getLabels());

        //label UI
        nameButton.onClick.AddListener(() => ChangeNameStatus());

        //face UI
        registerFaces.onClick.AddListener(() => getFaces());

        //Camera Buttons
        topCameraButton.onClick.AddListener(() => SwitchTopCamera());
        frontCameraButton.onClick.AddListener(() => SwitchFrontCamera());
        backCameraButton.onClick.AddListener(() => SwitchBackCamera());
        leftCameraButton.onClick.AddListener(() => SwitchLeftCamera());
        rightCameraButton.onClick.AddListener(() => SwitchRightCamera());
        worldCameraButton.onClick.AddListener(() => SwitchWorldCamera());

        //Camera Menu
        cameraButton.onClick.AddListener(() => getCameras());

        //Axis Menu
        guideButton.onClick.AddListener(() => getGuideMenu());
        minimiseGuides.onClick.AddListener(() => maximiseUI());

        //Rotation Axis Buttons
        axisButtonX.onClick.AddListener(() => ChangeRotationAxisStatusX());
        axisButtonY.onClick.AddListener(() => ChangeRotationAxisStatusY());
        axisButtonZ.onClick.AddListener(() => ChangeRotationAxisStatusZ());

        //Positional Axis Buttons
        front.onClick.AddListener(() => ChangePositionAxisStatusFront());
        behind.onClick.AddListener(() => ChangePositionAxisStatusBehind());
        left.onClick.AddListener(() => ChangePositionAxisStatusLeft());
        right.onClick.AddListener(() => ChangePositionAxisStatusRight());

        //Collision Box Button
        collisionBoxButton.onClick.AddListener(() => ChangeCollisionBoxStatus());

        //FAQ button
        FAQButton.onClick.AddListener(() => FAQMethod());
    }

    //used for FAQ
    private void FAQMethod()
    {
        Application.OpenURL("https://docs.google.com/document/d/1SkV4OrfCyLmZe8Qw9xZSK_QcrXPQO7WEd0RTMAPfvPA/edit?usp=sharing");
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

    public void ChangePositionAxisStatusFront()
    {
        axisFront = !axisFront;

        //get reference to the image component
        Image buttonImage = front.GetComponent<Image>();

        if (axisFront)
        {
            frontGuide.SetActive(true);

            //green color
            Color newColor = new Color(0.6953385f, 1f, 0.654717f);

            //set the color of the image
            buttonImage.color = newColor;
        }
        else
        {
            frontGuide.SetActive(false);

            //red color
            Color newColor = new Color(1f, 0.7215686f, 0.7215686f);

            //set the color of the image
            buttonImage.color = newColor;
        }
    }

    public void ChangePositionAxisStatusBehind()
    {
        axisBehind = !axisBehind;

        //get reference to the image component
        Image buttonImage = behind.GetComponent<Image>();

        if (axisBehind)
        {
            behindGuide.SetActive(true);

            //green color
            Color newColor = new Color(0.6953385f, 1f, 0.654717f);

            //set the color of the image
            buttonImage.color = newColor;
        }
        else
        {
            behindGuide.SetActive(false);

            //red color
            Color newColor = new Color(1f, 0.7215686f, 0.7215686f);

            //set the color of the image
            buttonImage.color = newColor;
        }
    }

    public void ChangePositionAxisStatusLeft()
    {
        axisLeft = !axisLeft;

        //get reference to the image component
        Image buttonImage = left.GetComponent<Image>();

        if (axisLeft)
        {
            leftGuide.SetActive(true);

            //green color
            Color newColor = new Color(0.6953385f, 1f, 0.654717f);

            //set the color of the image
            buttonImage.color = newColor;
        }
        else
        {
            leftGuide.SetActive(false);

            //red color
            Color newColor = new Color(1f, 0.7215686f, 0.7215686f);

            //set the color of the image
            buttonImage.color = newColor;
        }
    }

    public void ChangePositionAxisStatusRight()
    {
        axisRight = !axisRight;

        //get reference to the image component
        Image buttonImage = right.GetComponent<Image>();

        if (axisRight)
        {
            rightGuide.SetActive(true);

            //green color
            Color newColor = new Color(0.6953385f, 1f, 0.654717f);

            //set the color of the image
            buttonImage.color = newColor;
        }
        else
        {
            rightGuide.SetActive(false);

            //red color
            Color newColor = new Color(1f, 0.7215686f, 0.7215686f);

            //set the color of the image
            buttonImage.color = newColor;
        }
    }

    public void ChangeRotationAxisStatusX()
    {
        rotationAxisX = !rotationAxisX;

        //get reference to the image component
        Image buttonImage = axisButtonX.GetComponent<Image>();

        if (rotationAxisX)
        {
            ringX.SetActive(true);

            //green color
            Color newColor = new Color(0.6953385f, 1f, 0.654717f);

            //set the color of the image
            buttonImage.color = newColor;
        }
        else {
            ringX.SetActive(false);

            //red color
            Color newColor = new Color(1f, 0.7215686f, 0.7215686f);

            //set the color of the image
            buttonImage.color = newColor;
        }
    }

    public void ChangeRotationAxisStatusY()
    {
        rotationAxisY = !rotationAxisY;

        //get reference to the image component
        Image buttonImage = axisButtonY.GetComponent<Image>();

        if (rotationAxisY)
        {
            ringY.SetActive(true);

            //green color
            Color newColor = new Color(0.6953385f, 1f, 0.654717f);

            //set the color of the image
            buttonImage.color = newColor;
        }
        else
        {
            ringY.SetActive(false);

            //red color
            Color newColor = new Color(1f, 0.7215686f, 0.7215686f);

            //set the color of the image
            buttonImage.color = newColor;
        }
    }

    public void ChangeRotationAxisStatusZ()
    {
        rotationAxisZ = !rotationAxisZ;

        //get reference to the image component
        Image buttonImage = axisButtonZ.GetComponent<Image>();

        if (rotationAxisZ)
        {
            ringZ.SetActive(true);

            //green color
            Color newColor = new Color(0.6953385f, 1f, 0.654717f);

            //set the color of the image
            buttonImage.color = newColor;
        }
        else
        {
            ringZ.SetActive(false);

            //red color
            Color newColor = new Color(1f, 0.7215686f, 0.7215686f);

            //set the color of the image
            buttonImage.color = newColor;
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

    public List<string> getCurrentObjects()
    {
        List<string> list = new List<string>();

        //iterate through the tree
        foreach (KeyValuePair<Node, Node[]> entry in Tree)
        {
            //add the object string to the list
            list.Add(entry.Key.ToString());
        }

        return list;
    }

    //no walls
    public List<string> getCurrentObjectsNoWalls()
    {
        List<string> list = new List<string>();

        //iterate through the tree
        foreach (KeyValuePair<Node, Node[]> entry in Tree)
        {
            if (!entry.Key.getObjectType().Equals("wall1") && !entry.Key.getObjectType().Equals("wall2")) {
                //add the object string to the list
                list.Add(entry.Key.ToString());
            }
        }

        return list;
    }

    public void addCommand() {
        //calling the method
        StartCoroutine(addCommand2());
    }

    public IEnumerator addCommand2()
    {
        //runner object to use methods
        Test graphObject = new Test();

        //used to check if the action is recorded or not
        Boolean errorFlag = false;

        //create a copy of the current Tree
        IDictionary<Node, Node[]> currentTree = new Dictionary<Node, Node[]>();

        graphObject.DeepCopyTree(currentTree, Tree);

        //used to check if any of the words are null
        Boolean nullFlag = false;

        //current nodes
        Node[] currentNodes = Tree.Keys.ToArray();

        //array used to split the sentence into words
        string[] words = { };

        //array used to split the sentence into words
        string words2 = null;

        GameObject gameobjectUI =  null;

        //get the current active UI elements
        foreach (object element in uiElements) {
            //if the element is a Dropdown
            if (element is Dropdown)
            {
                Dropdown dropdown = element as Dropdown;
                gameobjectUI = dropdown.gameObject;
                
                if (gameobjectUI.activeSelf)
                {
                    List<Dropdown.OptionData> options = dropdown.options;

                    // Get the selected option's label
                    string selectedOption = options[dropdown.value].text;

                    words2 = words2 + selectedOption + " ";
                }
            }
            //if the element is an InputField
            else if (element is InputField)
            {
                InputField inputField = element as InputField;
                gameobjectUI = inputField.gameObject;

                //only get input from active command elements
                if (gameobjectUI.activeSelf)
                {
                    //if the input is not null
                    if (!inputField.text.Equals(""))
                    {
                        bool isNumber = float.TryParse(inputField.text, out float intValue);

                        //text is a number
                        if (isNumber)
                        {
                            //check that the inputed data is reasonable, i.e. not greater than +/- 150
                            if (float.Parse(inputField.text) <= 400 && float.Parse(inputField.text) >= -400)
                            {
                                words2 = words2 + inputField.text + " ";
                            }
                            else
                            {
                                errorFlag = true;
                            }
                        }
                        else {
                            bool spacingFlag = false;

                            //check if the input word is in the list
                            foreach (string spacingWord in SpacingList) {
                                if (String.Equals(spacingWord, inputField.text, StringComparison.OrdinalIgnoreCase)) {
                                    words2 = words2 + inputField.text + " ";
                                    spacingFlag = true;
                                }
                            }

                            //if the word does not exist
                            if (!spacingFlag) {
                                errorFlag = true;
                            }
                        }
                    }
                    else {
                        nullFlag = true;
                    }
                }
            }
            //if the element is an InputField
            else if (element is TMP_InputField)
            {
                TMP_InputField inputField = element as TMP_InputField;
                gameobjectUI = inputField.gameObject;

                //only get input from active command elements
                if (gameobjectUI.activeSelf)
                {
                    //if the input is not null
                    if (!inputField.text.Equals(""))
                    {
                        bool isNumber = float.TryParse(inputField.text, out float intValue);

                        //text is a number
                        if (isNumber)
                        {
                            //check that the inputed data is reasonable, i.e. not greater than +/- 150
                            if (float.Parse(inputField.text) <= 400 && float.Parse(inputField.text) >= -400)
                            {
                                words2 = words2 + inputField.text + " ";
                            }
                            else
                            {
                                errorFlag = true;
                            }
                        }
                        else
                        {
                            bool spacingFlag = false;

                            //check if the input word is in the list
                            foreach (string spacingWord in SpacingList)
                            {
                                if (String.Equals(spacingWord, inputField.text, StringComparison.OrdinalIgnoreCase) ){
                                    words2 = words2 + inputField.text + " ";
                                    spacingFlag = true;
                                }
                            }

                            //if the word does not exist
                            if (!spacingFlag)
                            {
                                errorFlag = true;
                            }
                        }
                    }
                    else
                    {
                        nullFlag = true;
                    }
                }
            }
        }

        //if the input is beyond the bounds of +400 & -400
        if (errorFlag){
            outputCommands.text = outputCommands.text + "Invalid input, numbers should be between 400 and -400" + "\n";
        }

        //the sentence is split into words by the spaces
        words = words2.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        //iterate through the words
        foreach (string wordCheck in words){
            if (wordCheck.Equals("null")) {
                nullFlag = true;
            }
        }

        //if the nullFlag is true
        if (nullFlag) {
            outputCommands.text = outputCommands.text + "Null input, correct input and retry command" + "\n";
        }

        //if the input is empty
        if (words2 == null) {
            outputCommands.text = outputCommands.text + "Empty input" + "\n";
        }

        //check that the input is not empty, that no word in the command is null and incorrect.
        //if (commandBox.text != "")
        if (words2 != null && nullFlag == false && errorFlag == false)
        {
            //flag to identify the type of command
            int commandFlag = -1;

            //reset the null flag
            nullFlag = false;

            //if the command is Add, flag to 0
            if (String.Equals(words[0], "Add", StringComparison.OrdinalIgnoreCase))
            {
                commandFlag = 0;
            }

            //if the command is Create, flag to 1
            else if (String.Equals(words[0], "Relate", StringComparison.OrdinalIgnoreCase))
            {
                if (String.Equals(words[2], "between", StringComparison.OrdinalIgnoreCase))
                { 
                    commandFlag = 13;
                }
                else {
                    commandFlag = 1;
                }
            }

            //if the command is Delete, flag to 2
            else if (String.Equals(words[0], "Delete", StringComparison.OrdinalIgnoreCase))
            {
                commandFlag = 2;
            }

            //if the command is Move, flag to 3, 4, 5, 6
            else if (String.Equals(words[0], "Move", StringComparison.OrdinalIgnoreCase))
            {
                //check which type of move command it is
                //Move compound cube_1 to 2,0,0
                if (String.Equals(words[1], "compound", StringComparison.OrdinalIgnoreCase) && 
                    String.Equals(words[3], "to", StringComparison.OrdinalIgnoreCase))
                {
                    commandFlag = 3;
                }
                //Move compound cube_1 on cube_2 or Move compound cube_1 under cube_2
                else if (String.Equals(words[1], "compound", StringComparison.OrdinalIgnoreCase))
                {
                    commandFlag = 4;
                }
                //Move simple cube_1 to 2,0,0
                else if (String.Equals(words[1], "simple", StringComparison.OrdinalIgnoreCase) && 
                    String.Equals(words[3], "to", StringComparison.OrdinalIgnoreCase))
                {
                    commandFlag = 5;
                }
                //Move simple cube_1 on cube_2 or Move simple cube_1 under cube_2
                else if (String.Equals(words[1], "simple", StringComparison.OrdinalIgnoreCase))
                { 
                    commandFlag = 6;
                }
                else //if it is none of them there is an error
                { 
                    commandFlag = -1;
                }
            }

            //if the command is Change, flag to 7
            else if (String.Equals(words[0], "Texture", StringComparison.OrdinalIgnoreCase))
            {
                commandFlag = 7;
            }

            //if the command is RotateY, flag to 8, 9
            else if (String.Equals(words[0], "RotateY", StringComparison.OrdinalIgnoreCase))
            {
                //check which type of move command it is
                if (String.Equals(words[1], "simple", StringComparison.OrdinalIgnoreCase))//RotateY simple table_1 by 90
                {
                    commandFlag = 8;
                }
                else if (String.Equals(words[1], "compound", StringComparison.OrdinalIgnoreCase))//RotateY compound table_1 by 90
                {
                    commandFlag = 9;
                }
                else {
                    //if it is none of them there is an error
                    commandFlag = -1;
                }
            }

            //if the command is RotateY, flag to 10
            else if (String.Equals(words[0], "RotateX", StringComparison.OrdinalIgnoreCase))
            {
                commandFlag = 10;
            }

            //if the command is RotateY, flag to 11
            else if (String.Equals(words[0], "RotateZ", StringComparison.OrdinalIgnoreCase))
            {
                commandFlag = 11;
            }

            //if the command is Break, flag to 12
            else if (String.Equals(words[0], "Break", StringComparison.OrdinalIgnoreCase))
            {
                commandFlag = 12;
            }

            //if the command is Reset, flag to 13
            else if (String.Equals(words[0], "Reset", StringComparison.OrdinalIgnoreCase))
            {
                commandFlag = 13;
            }

            //if the command is Move, flag to 14, 15, 16, 17
            else if (String.Equals(words[0], "MoveBy", StringComparison.OrdinalIgnoreCase))
            {
                //check which type of move command it is
                //MoveBy global_axis simple table_1 left by 1
                if (String.Equals(words[1], "global_axis", StringComparison.OrdinalIgnoreCase) &&
                    String.Equals(words[2], "simple", StringComparison.OrdinalIgnoreCase))
                {
                    commandFlag = 14;
                }
                //MoveBy global_axis compound table_1 left by 1
                else if (String.Equals(words[1], "global_axis", StringComparison.OrdinalIgnoreCase) &&
                    String.Equals(words[2], "compound", StringComparison.OrdinalIgnoreCase))
                {
                    commandFlag = 15;
                }
                //MoveBy object_axis simple table_1 left by 1
                else if (String.Equals(words[1], "object_axis", StringComparison.OrdinalIgnoreCase) &&
                    String.Equals(words[2], "simple", StringComparison.OrdinalIgnoreCase))
                {
                    commandFlag = 16;
                }
                //MoveBy object_axis compound table_1 left by 1
                else if (String.Equals(words[1], "object_axis", StringComparison.OrdinalIgnoreCase) &&
                    String.Equals(words[2], "compound", StringComparison.OrdinalIgnoreCase))
                {
                    commandFlag = 17;
                }
                else //if it is none of them there is an error
                {
                    commandFlag = -1;
                }
            }

            //if the command is none of the above, flag to -1
            else
            {
                commandFlag = -1;
            }

            //CHECK - it outputting invalid commands
            //display the command in the output commands box
            //outputCommands.text = outputCommands.text + commandBox.text + "\n";

            //only called for the start nodes
            //if the command is an Add command, that means that it is a start node, ex: Add cube_1 2 0 0
            if (words.Length == 5 && commandFlag == 0)
            {
                //check if the object is walls
                if (words[1].Equals("walls"))
                {
                    //place the parent object with all the walls at the place ------

                    //getting the name of the object
                    string parent = words[1];

                    //contains the coordinates 
                    string[] coordinatesXYZ = { words[2], words[3], words[4] };

                    //float coordinates array
                    float[] coords = coordinatesXYZ.Select(float.Parse).ToArray();

                    //creating the parent node
                    Node parentNode = new Node(parent);

                    //given that the unity coordinates are calculated on the centre of the object,
                    //we need to add half of the object's height to the 'y' coordinate such that
                    //the bottom of the object touches the ground
                    coords[1] = coords[1] + ((4) / 2);

                    //set the initial rotation to 0
                    parentNode.setRotationX(0);
                    parentNode.setRotationY(0);
                    parentNode.setRotationZ(0);

                    //setting the coordinates
                    parentNode.setCoordinates(coords);

                    //setting the object type 
                    parentNode.setObjectType(words[1]);

                    //update the list of objects that are exempt from collisions, names from prefab
                    listOfObjects.Add("EnemyWall1_1");
                    listOfObjects.Add("EnemyWall2_1");
                    listOfObjects.Add("EnemyWall1_2");
                    listOfObjects.Add("EnemyWall2_2");

                    //check if it collides with any other object
                    yield return StartCoroutine(checkSceneCollidersParent(parentNode));

                    //yield return new WaitForSeconds(creationDelay);

                    //if it does not collide
                    if (found == true)
                    {
                        //reset the flag
                        found = false;

                        //when adding a new object, collision boxes are shown
                        collisionBoxFlag = true;
                        CollisionBoxStatus();

                        //split the parent -------

                        //from the scene we find the walls
                        GameObject walls = GameObject.Find("wallstemp(Clone)");

                        //get reference to the transform component
                        Transform wallsTransform = walls.GetComponent<Transform>();

                        //empty list to hold the child GameObjects
                        List<GameObject> children = new List<GameObject>();

                        //loop through all the walls of the parent wall and add them to a list
                        foreach (Transform child in wallsTransform)
                        {
                            //add the child GameObject to the list
                            children.Add(child.gameObject);
                        }

                        //loop through all the walls and split them from the parent wall
                        foreach (GameObject child in children)
                        {
                            //set the parent of each child GameObject to null
                            child.transform.SetParent(null);
                        }

                        //hand over to update
                        //yield return new WaitForSeconds(creationDelay);

                        yield return null;

                        //update the coords and other -----

                        Boolean type = true;

                        //loop through all the walls
                        foreach (GameObject child in children)
                        {
                            //creating the parent node
                            Node wallNode = new Node("");

                            //enemyWall1
                            if (type)
                            {
                                //getting the name of the object
                                string name = "wall1" + "_" + GetIndexForObject("wall1");

                                //update the name in the node object
                                wallNode.setValue(name);

                                //update the name in unity
                                child.name = name + "temp(Clone)";

                                //setting the object type, depending on wall type
                                wallNode.setObjectType("wall1");
                                
                                type = false;
                            }
                            //enemywall2
                            else {
                                //getting the name of the object
                                string name = "wall2" + "_" + GetIndexForObject("wall2");

                                wallNode.setValue(name);

                                //update the name in unity
                                child.name = name + "temp(Clone)";

                                //setting the object type, depending on wall type
                                wallNode.setObjectType("wall2");
                                
                                type = true;
                            }

                            //get the current position of the wall
                            Vector3 position = child.transform.position;

                            float[] coordsWalls = new float[3];

                            //vector3 to float
                            coordsWalls[0] = position.x;
                            coordsWalls[1] = position.y;
                            coordsWalls[2] = position.z;

                            //setting the coordinates
                            wallNode.setCoordinates(coordsWalls);

                            //get the dimensions of the gameObject
                            Vector3 size = child.transform.localScale;

                            //the user entered coords
                            float[] fakeCoords = coordsWalls.ToArray();

                            //given that the unity coordinates are calculated on the centre of the object,
                            //we need to add half of the object's height to the 'y' coordinate such that
                            //the bottom of the object touches the ground
                            fakeCoords[1] = coordsWalls[1] - ((4) / 2);

                            //set the fake coordinates
                            wallNode.setFakeCoordinates(fakeCoords);

                            //set the initial rotation to 0
                            wallNode.setRotationX(0);
                            wallNode.setRotationY(0);
                            wallNode.setRotationZ(0);

                            Node deepCopy = wallNode.CopyDeep();

                            //add the objects to the tree --------
                            Boolean resultAddChildren = graphObject.AddObjectsToDatastructure(TreeSet, deepCopy, null); //adding the node to the tree - it has no children
                        }

                        //remove the empty gameObject
                        Destroy(walls);

                        //display the command in the output commands box
                        outputCommands.text = outputCommands.text + words[0] + " " + parent + " " + "at " + words[2] + ", " + words[3] + ", " + words[4] + "\n";

                        //reset the list of objects
                        listOfObjects.Clear();

                        errorFlag = false;
                    }
                    else
                    {
                        outputCommands.text = outputCommands.text + "Could not add walls at given location" + "\n";
                        errorFlag = true;
                    }
                }
                else {
                    //getting the name of the object
                    string parent = words[1] + "_" + GetIndexForObject(words[1]);

                    //getting the object and removing the number from it, Tree_1 -> Tree, 1
                    string[] objectTypeWithNo = words[1].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                    //check that the object to be created exists
                    if (objectList.Any(x => x.Equals(objectTypeWithNo[0], StringComparison.OrdinalIgnoreCase)))
                    {
                        ////getting the string of the coordinates, ex: 2,0,0
                        //string coordinates = words[3];

                        //contains the coordinates 
                        string[] coordinatesXYZ = { words[2], words[3], words[4] };

                        ////split the string into x, y and z coordnates
                        //coordinatesXYZ = words[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

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

                        float[] fakeCoords = coords.ToArray();

                        //set the fake coordinates
                        parentNode.setFakeCoordinates(fakeCoords);

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
                            yield return StartCoroutine(checkSceneCollidersParent(parentNode));

                            //yield return new WaitForSeconds(creationDelay);

                            //if it does not collide
                            if (found == true)
                            {
                                //reset the flag
                                found = false;

                                Boolean resultAddChildren = graphObject.AddObjectsToDatastructure(TreeSet, parentNode, null); //adding the first node to the tree - it has no children

                                //when adding a new object collisions are set to true
                                collisionBoxFlag = true;
                                CollisionBoxStatus();

                                //display the command in the output commands box
                                outputCommands.text = outputCommands.text + words[0] + " " + parent + " " + "at " + words[2] + ", " + words[3] + ", " + words[4] + "\n";

                                if (resultAddChildren == false)
                                {
                                    outputCommands.text = outputCommands.text + "Could not add node!" + "\n";
                                    errorFlag = true;
                                }
                            }
                            else
                            {
                                outputCommands.text = outputCommands.text + "Cannot add object at given coordinates" + "\n";
                                errorFlag = true;
                            }
                        }
                        else
                        {
                            outputCommands.text = outputCommands.text + "Object already exists" + "\n";
                            errorFlag = true;
                        }
                    }
                    else
                    {
                        outputCommands.text = outputCommands.text + "Not a valid object" + "\n";
                        errorFlag = true;
                    }
                }
            }

            //LIMITATION: CANNOT CREATE OBJECT ON OBJECT WITH ROTATION ON X AND Z AXIS

            //if the command is a Create command and consists of 5 words, that means that it is a single relationship, ex: Relate Book_1 under Tree_1 1
            else if (words.Length == 5 && commandFlag == 1)
            {
                //the parent node
                string parent = words[3];

                //creating the parent node
                Node parentNode = new Node(parent);

                //the child node
                string child = words[1] + "_" + GetIndexForObject(words[1]);

                //creating the child node
                Node childNode = new Node(child);

                //the preposition
                string preposition = words[2];

                string[] objectTypeWithNo = child.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                
                float spacing = 0f;

                //check the spacing from here
                if (words[4].Equals("Touching", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 0f;
                }
                else if (words[4].Equals("near", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 1f;
                }
                else if (words[4].Equals("moderate", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 2.5f;
                }
                else if (words[4].Equals("far", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 4f;
                }
                else { //if it is not a word
                    spacing = float.Parse(words[4]);
                }

                //setting the spacing for the child
                childNode.setSpacing(spacing.ToString());

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

                                //check if the object has any rotation on x and z axis
                                if (parentObj.transform.rotation.eulerAngles.z == 0 && parentObj.transform.rotation.eulerAngles.x == 0)
                                {
                                    //set the initial rotation on the Y axis to be the same as the parent object
                                    childNode.setRotationY(parentObj.transform.rotation.eulerAngles.y);

                                    //initialise other rotations to 0
                                    childNode.setRotationX(0);
                                    childNode.setRotationZ(0);

                                    //create a copy of the Tree
                                    IDictionary<Node, Node[]> tempTree = new Dictionary<Node, Node[]>();

                                    graphObject.DeepCopyTree(tempTree, Tree);

                                    //add the new records
                                    graphObject.AddObjectsToDatastructure(tempTree, parentNode, childNode);

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
                                            yield return StartCoroutine(checkSceneCollidersParent(entry.Key));

                                            //yield return new WaitForSeconds(creationDelay);

                                            if (found == true)
                                            {
                                                found = false;//reset the found flag

                                                //if correct make current tree equal to the new tree
                                                Tree = tempTree;

                                                //when adding a new object collisions are set to true
                                                collisionBoxFlag = true;
                                                CollisionBoxStatus();

                                                //update the dropdown with the new object added
                                                currentObjects = getCurrentObjects();

                                                //no need to reset the dropdown selected option
                                                SetOptions(currentObjects, dropDown4); //set the options

                                                //keep the option selected
                                                for (int i = 0; i < currentObjects.Count; i++)
                                                {
                                                    if (dropDown4.options[i].text == parent)
                                                    {
                                                        dropDown4.value = i; //set to the same previous option
                                                    }
                                                }

                                                //display the command in the output commands box
                                                outputCommands.text = outputCommands.text + words[0] + " " + child + " " + preposition + " " + parent + " " + words[4] + "\n";
                                            }
                                            else
                                            {
                                                outputCommands.text = outputCommands.text + "Object collides with other object" + "\n";
                                                errorFlag = true;
                                            }
                                        }
                                    }
                                }
                                else {
                                    outputCommands.text = outputCommands.text + "Cannot relate to objects with rotations on x and z axis" + "\n";
                                    errorFlag = true;
                                }
                            }
                            else
                            {
                                outputCommands.text = outputCommands.text + "A gameobject exists with the same name" + "\n";
                                errorFlag = true;
                            }
                        }
                        else
                        {
                            outputCommands.text = outputCommands.text + "A gameobject exists with the same relationship to the parent" + "\n";
                            errorFlag = true;
                        }
                    }
                    else
                    {
                        outputCommands.text = outputCommands.text + "Parent game object does not exist" + "\n";
                        errorFlag = true;
                    }
                }
                else
                {
                    outputCommands.text = outputCommands.text + "Parent game object does not exist" + "\n";
                    errorFlag = true;
                }
            }

            //if the command is a Create command and consists of 6 words, that means that it is a double relationship, ex: Relate Book_1 between chair_1 table_1
            else if (words.Length == 5 && commandFlag == 13)
            {
                //if the objects are not equal
                if (!words[3].Equals(words[4]))
                {
                    //the first parent node
                    string parent1 = words[3];

                    //creating the first parent node
                    Node parentNode1 = new Node(parent1);

                    //the second parent node
                    string parent2 = words[4];

                    //creating the second parent node
                    Node parentNode2 = new Node(parent2);

                    //check that both parent objects exist

                    //create child node

                    //the child node
                    string child = words[1] + "_" + GetIndexForObject(words[1]);

                    //creating the child node
                    Node childNode = new Node(child);

                    string preposition = words[2];//the preposition
                    string[] objectTypeWithNo = child.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                    childNode.setPreposition(preposition);
                    childNode.setObjectType(objectTypeWithNo[0]);

                    //initialise other rotations to 0
                    childNode.setRotationX(0);
                    childNode.setRotationZ(0);

                    //let us calculate the rotation on Y---

                    //get reference to the parent1 object
                    GameObject parentObj1 = GameObject.Find(parentNode1.ToString() + "temp(Clone)");

                    //get reference to the parent2 object
                    GameObject parentObj2 = GameObject.Find(parentNode2.ToString() + "temp(Clone)");

                    float rotationY1 = parentObj1.transform.rotation.eulerAngles.y;
                    float rotationY2 = parentObj2.transform.rotation.eulerAngles.y;

                    //set the initial rotation on the Y axis to be the same as the parent object
                    childNode.setRotationY(((rotationY1 + rotationY2)/2) % 360);

                    //let us calculate the coordinates---

                    float x1 = parentObj1.transform.position.x;
                    float x2 = parentObj2.transform.position.x;

                    float z1 = parentObj1.transform.position.z;
                    float z2 = parentObj1.transform.position.z;

                    GameObject childObj = childNode.getObjectTrue();

                    Vector3 size = childObj.transform.localScale;

                    float y = (size.y) / 2;

                    float[] coords = {(x1 + x2) / 2, y, (z1 + z2) / 2};

                    childNode.setCoordinates(coords);

                    float[] fakeCoords = { coords[0], 0, coords[2] };

                    childNode.setFakeCoordinates(fakeCoords);

                    //let us calculate the spacing ---

                    //LIMITATION
                    //calculate distance between parent coords
                    float x1_2 = x1 - x2; //distance along x axis

                    //if negative turn positive, spacing cannot be negative
                    if (x1_2 < 0)
                    {
                        x1_2 = x1_2 * -1;
                    }

                    //calculate distance between parent coords
                    float z1_2 = z1 - z2;

                    //if negative turn positive, spacing cannot be negative
                    if (z1_2 < 0)
                    {
                        z1_2 = z1_2 * -1;
                    }

                    //distance from an object to another
                    double xz = 0;

                    //check that none of the distances is 0
                    if (z1_2 > 0 && x1_2 > 0 )
                    {
                        //calculate hypothenus using pythagoras theorem
                        //square of x
                        double squareX = Math.Pow(x1_2, 2);

                        //square of y
                        double squareZ = Math.Pow(z1_2, 2);

                        //hypothenus
                        xz = Math.Sqrt(squareX + squareZ);
                    }
                    else {
                        //check which length has 0
                        if (z1_2 == 0) {
                            xz = x1_2;
                        }
                        else{
                            xz = z1_2;
                        }
                    }

                    //half width of both parents, width of child, take the average
                    //float widthChild = size



                    //create a copy of the Tree
                    IDictionary<Node, Node[]> tempTree = new Dictionary<Node, Node[]>();

                    graphObject.DeepCopyTree(tempTree, Tree);

                    //since it is a double relationship, we add two records
                    graphObject.AddObjectsToDatastructure(Tree, parentNode1, childNode);
                    graphObject.AddObjectsToDatastructure(Tree, parentNode2, childNode);
                }
                else {
                    outputCommands.text = outputCommands.text + "Cannot relate new object between the same object" + "\n";
                    errorFlag = true;
                }
            }

            //if the command is a Delete command and consists of 2 words, ex: Delete cube_1
            else if (words.Length == 2 && commandFlag == 2)
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
                    //yield return new WaitForSeconds(creationDelay);

                    yield return null;

                    //update the dropdown
                    currentObjects = getCurrentObjects();
                    //if the currentObjects list is empty
                    if (currentObjects.Count == 0)
                    {
                        currentObjects.Add("null");
                    }
                    SetOptions(currentObjects, dropDown2); //set the option
                    dropDown2.value = 0;//set to add option

                    //display the command in the output commands box
                    outputCommands.text = outputCommands.text + words[0] + " " + delete + "\n";
                }
                else
                {
                    outputCommands.text = outputCommands.text + "Could not find node!" + "\n";
                    errorFlag = true;
                }
            }

            //if the command is a Move command and consists of 5 words, ex: Move compound cube_1 to 2,0,0
            else if (words.Length == 7 && commandFlag == 3)
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

                    ////getting the string of the coordinates, ex: 2,0,0
                    //string coordinates = words[4];

                    //contains the coordinates 
                    string[] coordinatesXYZ = { words[4], words[5], words[6] };

                    ////split the string into x, y and z coordnates
                    //coordinatesXYZ = words[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    //float coordinates array
                    float[] coords = coordinatesXYZ.Select(float.Parse).ToArray();

                    //get reference to the gameObject of the node
                    GameObject obj = parentNode.getObjectTrue();

                    //get the dimensions of the gameObject
                    Vector3 size = obj.transform.localScale;

                    float[] fakeCoords = coords.ToArray();

                    //set the fake coordinates
                    parentNode.setFakeCoordinates(fakeCoords);

                    //given that the unity coordinates are calculated on the centre of the object,
                    //we need to add half of the object's height to the 'y' coordinate such that
                    //the bottom of the object touches the ground
                    coords[1] = coords[1] + ((size.y) / 2);

                    //setting the new coordinates of the object
                    parentNode.setCoordinates(coords);
                    //create a temporary tree and recalculate the coordinates
                    //create a copy of the Tree
                    IDictionary<Node, Node[]> tempTree = new Dictionary<Node, Node[]>();

                    graphObject.DeepCopyTree(tempTree, Tree);

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

                    //check rotations
                    if (objOld.transform.rotation.eulerAngles.z == 0 && objOld.transform.rotation.eulerAngles.x == 0)
                    {
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
                                    yield return StartCoroutine(checkSceneCollidersParent(nodeCopy));

                                    //yield return new WaitForSeconds(creationDelay);//handing control to Update()

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

                                        //yield return new WaitForSeconds(creationDelay);

                                        yield return null;

                                        ////find the object
                                        //String originalObject = node.ToString().Replace("Copy", "");

                                        GameObject objToUpdate = GameObject.Find(node.ToString() + "temp(Clone)");
                                        objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                        //yield return new WaitForSeconds(creationDelay);

                                        yield return null;

                                        //find the created
                                        if (GameObject.Find(node.ToString() + "(Clone)"))
                                        {
                                            GameObject objToMove = GameObject.Find(node.ToString() + "(Clone)");
                                            objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                            //yield return new WaitForSeconds(creationDelay);

                                            yield return null;
                                        }
                                    }

                                    //update the Tree
                                    TreeSet = tempTree;

                                    //display the command in the output commands box
                                    outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4] + " " + words[5] + " " + words[6] + "\n";
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

                                        //yield return new WaitForSeconds(creationDelay);
                                        yield return null;
                                    }


                                    outputCommands.text = outputCommands.text + "Gameobject(s) collide(s) with other unrelated gameobjects" + "\n";
                                    errorFlag = true;
                                }
                            }
                            else
                            {
                                outputCommands.text = outputCommands.text + "Game object does not exist not exist" + "\n";
                                errorFlag = true;
                            }
                        }
                        
                    }
                    else
                    {
                        outputCommands.text = outputCommands.text + "Cannot move objects that are rotated on x and z axis" + "\n";
                        errorFlag = true;
                    }
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    outputCommands.text = outputCommands.text + "Gameobject does not exist" + "\n";
                    errorFlag = true;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Move command and consists of 6 words, ex: Move compound cube_1 on cube_3 0.5
            else if (words.Length == 6 && commandFlag == 4)
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

                float spacing = 0f;

                //check the spacing from here
                if (words[5].Equals("Touching", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 0f;
                }
                else if (words[5].Equals("near", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 1f;
                }
                else if (words[5].Equals("moderate", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 2.5f;
                }
                else if (words[5].Equals("far", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 4f;
                }
                else
                { //if it is not a word
                    spacing = float.Parse(words[5]);
                }

                //setting the spacing
                parentNode.setSpacing(spacing.ToString());

                string[] objectTypeWithNo = parent.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the object type
                parentNode.setObjectType(objectTypeWithNo[0]);

                //the name of the node that will be the new parent
                string newParent = words[4];

                //get reference to the parent object
                GameObject newParentObj = GameObject.Find(newParent + "temp(Clone)");

                //check if the object has any rotation on x and z axis
                if (newParentObj.transform.rotation.eulerAngles.z == 0 && newParentObj.transform.rotation.eulerAngles.x == 0)
                {
                    //creating the new parent node
                    Node newParentNode = new Node(newParent);

                    string[] objectTypeWithNo1 = parent.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                    //setting the object type
                    newParentNode.setObjectType(objectTypeWithNo1[0]);

                    //create a temporary tree and recalculate the coordinates
                    //create a copy of the Tree
                    IDictionary<Node, Node[]> tempTree = new Dictionary<Node, Node[]>();

                    graphObject.DeepCopyTree(tempTree, Tree);

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

                            //update the list of objects that are exempt from collisions, check for children
                            foreach (Node node in nodesToMove)
                            {
                                if (node.ToString().Equals(newParent.ToString()))
                                {
                                    errorFlag = true;
                                }
                                listOfObjects.Add(node.ToString());
                            }


                            //calculate the coordinates of the gameobjects
                            nodesToMove = setCoordinatesCompound(tempTree, nodesToMove);

                            //check that the new coordinates are not the same as the old coordinates
                            GameObject objOld = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //check rotations
                            if (objOld.transform.rotation.eulerAngles.z == 0 && objOld.transform.rotation.eulerAngles.x == 0)
                            {
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
                                            yield return StartCoroutine(checkSceneCollidersParent(nodeCopy));

                                            //yield return new WaitForSeconds(creationDelay);//handing control to Update()

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

                                                //yield return new WaitForSeconds(creationDelay);

                                                yield return null;

                                                ////find the object
                                                //String originalObject = node.ToString().Replace("Copy", "");

                                                GameObject objToUpdate = GameObject.Find(node.ToString() + "temp(Clone)");
                                                objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                                //yield return new WaitForSeconds(creationDelay);
                                                yield return null;

                                                //find the created
                                                if (GameObject.Find(node.ToString() + "(Clone)"))
                                                {
                                                    GameObject objToMove = GameObject.Find(node.ToString() + "(Clone)");
                                                    objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                                    //yield return new WaitForSeconds(creationDelay);
                                                    yield return null;
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

                                                yield return null;
                                            }

                                            outputCommands.text = outputCommands.text + "Gameobject(s) collide(s) with other unrelated gameobjects" + "\n";
                                            errorFlag = true;
                                        }
                                    }
                                    else
                                    {
                                        outputCommands.text = outputCommands.text + "New parent gameobject does not exist" + "\n";
                                        errorFlag = true;
                                    }
                                }
                                else
                                {
                                    outputCommands.text = outputCommands.text + "New coordinates are same as current coordinates" + "\n";
                                    errorFlag = true;
                                }
                            }
                            else
                            {
                                outputCommands.text = outputCommands.text + "Cannot move objects that are rotated on x and z axis" + "\n";
                                errorFlag = true;
                            }
                        }
                        else
                        {
                            outputCommands.text = outputCommands.text + "Gameobject to be moved does not exist" + "\n";
                            errorFlag = true;
                        }
                    }
                    ////create a copy of the old parent node for later
                    //Node parentNodeCopy = parentNode.Copy();
                    else
                    {
                        outputCommands.text = outputCommands.text + "Gameobject does not exist" + "\n";
                        errorFlag = true;
                    }
                }
                else
                {
                    outputCommands.text = outputCommands.text + "Cannot relate to objects with rotations on x and z axis" + "\n";
                    errorFlag = true;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Move simple command and consists of 5 words, ex: Move simple cube_1 to 2,0,0
            else if (words.Length == 7 && commandFlag == 5)
            {
                //the parent node
                string parent = words[2];

                //creating the parent node
                Node parentNode = new Node(parent);

                //getting the object and removing the number from it, Tree_1 -> Tree, 1
                string[] objectTypeWithNo = words[2].Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                //setting the object type 
                parentNode.setObjectType(objectTypeWithNo[0]);

                ////getting the string of the coordinates, ex: 2,0,0
                //string coordinates = words[4];

                //contains the coordinates 
                string[] coordinatesXYZ = { words[4], words[5], words[6] };

                ////split the string into x, y and z coordnates
                //coordinatesXYZ = words[3].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //float coordinates array
                float[] coords = coordinatesXYZ.Select(float.Parse).ToArray();

                float[] fakeCoords = coords.ToArray();

                //set the fake coordinates
                parentNode.setFakeCoordinates(fakeCoords);

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
                
                //keep coords for x and y rotated objects
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
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = new Dictionary<Node, Node[]>();

                graphObject.DeepCopyTree(tempTree, Tree);

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
                            nodeReference = entry.Key.CopyDeep();
                        }
                    }

                    //update the list of objects that are exempt from collisions
                    listOfObjects.Add(nodeReference.ToString());

                    //check that the new coordinates are not the same as the old coordinates
                    GameObject objOld = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //check rotations
                    if (objOld.transform.rotation.eulerAngles.z == 0 && objOld.transform.rotation.eulerAngles.x == 0)
                    {
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
                                yield return StartCoroutine(checkSceneCollidersParent(nodeReference));

                                //yield return new WaitForSeconds(creationDelay);//handing control to Update()

                                if (found == true)//the node can be moved
                                {
                                    found = false;//reset the found flag

                                    create = true;//boolean to see if there are collisions in all children
                                }
                                else
                                {
                                    //the objects will not be moved as there is a collision
                                    create = false;

                                }

                                //if there are no collisions at all
                                if (create == true)
                                {
                                    //the objects can be moved

                                    GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                    //destroying the object
                                    Destroy(objToDelete);

                                    yield return null;

                                    //find the object
                                    String originalObject = nodeReference.ToString().Replace("Copy", "");

                                    GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                    objToUpdate.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                    yield return null;

                                    //find the created
                                    if (GameObject.Find(originalObject + "(Clone)"))
                                    {
                                        GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                        objToMove.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                        yield return null;
                                    }

                                    //update the Tree
                                    TreeSet = tempTree;

                                    //display the command in the output commands box
                                    outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4] + " " + words[5] + " " + words[6] + "\n";
                                }
                                else
                                {
                                    //remember to destroy the nodes
                                    GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                    //destroying the object
                                    Destroy(objToDelete);

                                    yield return new WaitForSeconds(creationDelay);

                                    //output error message
                                    outputCommands.text = outputCommands.text + "Game object collides with other unrelated gameobjects" + "\n";
                                    errorFlag = true;
                                }
                            }
                            else
                            {
                                placeholder.text = "Could not add node!";
                                placeholder.color = Color.red;
                                errorFlag = true;
                            }
                        }
                        else
                        {
                            outputCommands.text = outputCommands.text + "New coordinates are same as old coordinates" + "\n";
                            errorFlag = true;
                        }
                    }
                    else
                    {
                        outputCommands.text = outputCommands.text + "Cannot move objects that are rotated on x and z axis" + "\n";
                        errorFlag = true;
                    }
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    outputCommands.text = outputCommands.text + "Parent gameobject does not exist" + "\n";
                    errorFlag = true;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Move simple command and consists of 5 words, ex: Move simple cube_1 on cube_3 5
            else if (words.Length == 6 && commandFlag == 6)
            {
                //the parent node
                string parent = words[2];

                //creating the parent node
                Node parentNode = new Node(parent);

                float spacing = 0f;

                //check the spacing from here
                if (words[5].Equals("Touching", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 0f;
                }
                else if (words[5].Equals("near", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 1f;
                }
                else if (words[5].Equals("moderate", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 2.5f;
                }
                else if (words[5].Equals("far", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 4f;
                }
                else
                { //if it is not a word
                    spacing = float.Parse(words[5]);
                }

                //setting the spacing
                parentNode.setSpacing(spacing.ToString());

                //storing the preposition
                string preposition = words[3];

                //setting the preposition
                parentNode.setPreposition(preposition);

                //the name of the node that will be the new parent
                string newParent = words[4];

                //get reference to the parent object
                GameObject newParentObj = GameObject.Find(newParent + "temp(Clone)");

                //check if the object has any rotation on x and z axis
                if (newParentObj.transform.rotation.eulerAngles.z == 0 && newParentObj.transform.rotation.eulerAngles.x == 0)
                {
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
                                    nodeReference = entry.Key.CopyDeep();
                                }
                            }

                            //update the list of objects that are exempt from collisions
                            listOfObjects.Add(nodeReference.ToString());

                            //check that the new coordinates are not the same as the old coordinates
                            GameObject objOld = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //check rotations
                            if (objOld.transform.rotation.eulerAngles.z == 0 && objOld.transform.rotation.eulerAngles.x == 0)
                            {
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
                                        yield return StartCoroutine(checkSceneCollidersParent(nodeReference));

                                        //yield return new WaitForSeconds(creationDelay);//handing control to Update()

                                        if (found == true)//the node can be moved
                                        {
                                            found = false;//reset the found flag

                                            create = true;//boolean to see if there are collisions in all children
                                        }
                                        else
                                        {
                                            //the objects will not be moved as there is a collision
                                            create = false;
                                        }

                                        //if there are no collisions at all
                                        if (create == true)
                                        {
                                            //the objects can be moved

                                            GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                            //destroying the object
                                            Destroy(objToDelete);

                                            yield return null;

                                            //find the object
                                            String originalObject = nodeReference.ToString().Replace("Copy", "");

                                            GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                            objToUpdate.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                            yield return null;

                                            //find the created
                                            if (GameObject.Find(originalObject + "(Clone)"))
                                            {
                                                GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                                objToMove.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                                yield return null;
                                            }

                                            outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4] + " " + words[5] + "\n";

                                            //update the Tree
                                            TreeSet = tempTree;
                                        }
                                        else
                                        {
                                            //remember to destroy the nodes
                                            GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                            //destroying the object
                                            Destroy(objToDelete);

                                            yield return null;

                                            outputCommands.text = outputCommands.text + "Game object collides with other unrelated gameobjects" + "\n";
                                            errorFlag = true;
                                        }
                                    }
                                }
                                else
                                {
                                    outputCommands.text = outputCommands.text + "New coordinates are same as old coordinates" + "\n";
                                    errorFlag = true;
                                }
                            }
                            else {
                                outputCommands.text = outputCommands.text + "Cannot move objects that are rotated on x and z axis" + "\n";
                                errorFlag = true;
                            }
                        }
                        else
                        {
                            outputCommands.text = outputCommands.text + "New parent object does not exist" + "\n";
                            errorFlag = true;
                        }
                    }

                    ////create a copy of the old parent node for later
                    //Node parentNodeCopy = parentNode.Copy();
                    else
                    {
                        outputCommands.text = outputCommands.text + "The gameobject to be moved does not exist" + "\n";
                        errorFlag = true;
                    }
                }
                else {
                    outputCommands.text = outputCommands.text + "Cannot relate to objects with rotations on x and z axis" + "\n";
                    errorFlag = true;
                }
                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a MoveBy gloabl_axis simple command and consists of 6 words, ex: MoveBy global_axis simple table_1 left 1
            else if (words.Length == 6 && commandFlag == 14)
            {
                //the parent node
                string parent = words[3];

                //direction
                string direction = words[4];

                float spacing = 0f;

                //check the spacing from here
                if (words[5].Equals("Touching", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 0f;
                }
                else if (words[5].Equals("near", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 1f;
                }
                else if (words[5].Equals("moderate", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 2.5f;
                }
                else if (words[5].Equals("far", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 4f;
                }
                else
                { //if it is not a word
                    spacing = float.Parse(words[5]);
                }

                //displacement
                string displacement = spacing.ToString();

                float displacementNo = float.Parse(displacement);

                //creating the parent node
                Node parentNode = new Node(parent);

                //create a temporary tree and recalculate the coordinates
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //check that the object to be moved exists
                Boolean resultCheckExistParent = checkExist(TreeSet, parentNode);

                if (resultCheckExistParent)//if the parent object exists
                {
                    //break relations with other nodes for the parentNode
                    tempTree = removeNodeAsChild(tempTree, parentNode);

                    //remove children since it is a simple
                    tempTree = removeChildren(tempTree, parentNode);

                    //add the new records and change coordinates for the parent
                    tempTree = changeTreeRelationsGlobal(tempTree, parentNode, direction, displacementNo);

                    Node nodeReference = null;

                    //add the parentNode as the start
                    foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                    {
                        //first thing to do is spawn the parent
                        if (entry.Key.ToString().Equals(parentNode.ToString()))
                        {
                            nodeReference = entry.Key.CopyDeep();
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
                            yield return StartCoroutine(checkSceneCollidersParent(nodeReference));

                            //yield return new WaitForSeconds(creationDelay);//handing control to Update()

                            if (found == true)//the node can be moved
                            {
                                found = false;//reset the found flag

                                create = true;//boolean to see if there are collisions in all children
                            }
                            else
                            {
                                //the objects will not be moved as there is a collision
                                create = false;
                            }

                            //if there are no collisions at all
                            if (create == true)
                            {
                                //the objects can be moved

                                GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                //destroying the object
                                Destroy(objToDelete);

                                yield return null;

                                //find the object
                                String originalObject = nodeReference.ToString().Replace("Copy", "");

                                GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                objToUpdate.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                yield return null;

                                //find the created
                                if (GameObject.Find(originalObject + "(Clone)"))
                                {
                                    GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                    objToMove.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                    yield return null;
                                }

                                outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4] + " " + words[5] + "\n";

                                //update the Tree
                                TreeSet = tempTree;
                            }
                            else
                            {
                                //remember to destroy the nodes
                                GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                //destroying the object
                                Destroy(objToDelete);

                                yield return null;

                                outputCommands.text = outputCommands.text + "Game object collides with other unrelated gameobjects" + "\n";
                                errorFlag = true;
                            }
                        }
                    }
                    else
                    {
                        outputCommands.text = outputCommands.text + "New coordinates are same as old coordinates" + "\n";
                        errorFlag = true;
                    }
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    outputCommands.text = outputCommands.text + "The gameobject to be moved does not exist" + "\n";
                    errorFlag = true;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a MoveBy gloabl_axis simple command and consists of 6 words, ex: MoveBy global_axis compound table_1 left 1
            else if (words.Length == 6 && commandFlag == 15)
            {
                //the parent node
                string parent = words[3];

                //direction
                string direction = words[4];

                float spacing = 0f;

                //check the spacing from here
                if (words[5].Equals("Touching", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 0f;
                }
                else if (words[5].Equals("near", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 1f;
                }
                else if (words[5].Equals("moderate", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 2.5f;
                }
                else if (words[5].Equals("far", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 4f;
                }
                else
                { //if it is not a word
                    spacing = float.Parse(words[5]);
                }

                //displacement
                string displacement = spacing.ToString();

                float displacementNo = float.Parse(displacement);

                //creating the parent node
                Node parentNode = new Node(parent);

                //create a temporary tree and recalculate the coordinates
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //check that the object to be moved exists
                Boolean resultCheckExistParent = checkExist(TreeSet, parentNode);

                if (resultCheckExistParent)//if the parent object exists
                {
                    //break relations with other nodes for the parentNode
                    tempTree = removeNodeAsChild(tempTree, parentNode);

                    //add the new records and change coordinates for the parent
                    tempTree = changeTreeRelationsGlobal(tempTree, parentNode, direction, displacementNo);

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
                      //Boolean resultCheckExist = checkExist(TreeSet, parentNode);
                        
                        Boolean create = true;

                        //loop through the nodes to be moved
                        foreach (Node node in nodesToMove)
                        {
                            Node nodeCopy = node.CopyDeep();

                            //change name as it is a temporary duplicate
                            nodeCopy.setValue(node.ToString() + "Copy");

                            //check that the nodes can be moved
                            yield return StartCoroutine(checkSceneCollidersParent(nodeCopy));

                            //yield return new WaitForSeconds(creationDelay);//handing control to Update()

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

                                yield return null;

                                ////find the object
                                //String originalObject = node.ToString().Replace("Copy", "");

                                GameObject objToUpdate = GameObject.Find(node.ToString() + "temp(Clone)");
                                objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                yield return null;

                                //find the created
                                if (GameObject.Find(node.ToString() + "(Clone)"))
                                {
                                    GameObject objToMove = GameObject.Find(node.ToString() + "(Clone)");
                                    objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                    yield return null;
                                }
                            }

                            //update the Tree
                            TreeSet = tempTree;

                            //display the command in the output commands box
                            outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4] + " " + words[5] + "\n";
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

                                yield return null;
                            }


                            outputCommands.text = outputCommands.text + "Gameobject(s) collide(s) with other unrelated gameobjects" + "\n";
                            errorFlag = true;
                        }
                    }
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    outputCommands.text = outputCommands.text + "The gameobject to be moved does not exist" + "\n";
                    errorFlag = true;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a MoveBy object_axis simple command and consists of 6 words, ex: MoveBy object_axis simple table_1 left 1
            else if (words.Length == 6 && commandFlag == 16)
            {
                //the parent node
                string parent = words[3];

                //direction
                string direction = words[4];

                float spacing = 0f;

                //check the spacing from here
                if (words[5].Equals("Touching", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 0f;
                }
                else if (words[5].Equals("near", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 1f;
                }
                else if (words[5].Equals("moderate", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 2.5f;
                }
                else if (words[5].Equals("far", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 4f;
                }
                else
                { //if it is not a word
                    spacing = float.Parse(words[5]);
                }

                //displacement
                string displacement = spacing.ToString();

                float displacementNo = float.Parse(displacement);

                //creating the parent node
                Node parentNode = new Node(parent);

                //create a temporary tree and recalculate the coordinates
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //check that the object to be moved exists
                Boolean resultCheckExistParent = checkExist(TreeSet, parentNode);

                if (resultCheckExistParent)//if the parent object exists
                {
                    //break relations with other nodes for the parentNode
                    tempTree = removeNodeAsChild(tempTree, parentNode);

                    //remove children since it is a simple
                    tempTree = removeChildren(tempTree, parentNode);

                    //add the new records and change coordinates for the parent
                    tempTree = changeTreeRelationsObject(tempTree, parentNode, direction, displacementNo);

                    Node nodeReference = null;

                    //add the parentNode as the start
                    foreach (KeyValuePair<Node, Node[]> entry in tempTree)
                    {
                        //first thing to do is spawn the parent
                        if (entry.Key.ToString().Equals(parentNode.ToString()))
                        {
                            nodeReference = entry.Key.CopyDeep();
                        }
                    }

                    //update the list of objects that are exempt from collisions
                    listOfObjects.Add(nodeReference.ToString());

                    //check that the new coordinates are not the same as the old coordinates
                    GameObject objOld = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                    //check if the object has any rotation on x and z axis
                    if (objOld.transform.rotation.eulerAngles.z == 0 && objOld.transform.rotation.eulerAngles.x == 0)
                    {
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
                                yield return StartCoroutine(checkSceneCollidersParent(nodeReference));

                                //yield return new WaitForSeconds(creationDelay);//handing control to Update()

                                if (found == true)//the node can be moved
                                {
                                    found = false;//reset the found flag

                                    create = true;//boolean to see if there are collisions in all children
                                }
                                else
                                {
                                    //the objects will not be moved as there is a collision
                                    create = false;
                                }

                                //if there are no collisions at all
                                if (create == true)
                                {
                                    //the objects can be moved

                                    GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                    //destroying the object
                                    Destroy(objToDelete);

                                    yield return null;

                                    //find the object
                                    String originalObject = nodeReference.ToString().Replace("Copy", "");

                                    GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                    objToUpdate.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                    yield return null;

                                    //find the created
                                    if (GameObject.Find(originalObject + "(Clone)"))
                                    {
                                        GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                        objToMove.transform.position = new Vector3(nodeReference.returnX(), nodeReference.returnY(), nodeReference.returnZ());

                                        yield return null;
                                    }

                                    outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4] + " " + words[5] + "\n";

                                    //update the Tree
                                    TreeSet = tempTree;
                                }
                                else
                                {
                                    //remember to destroy the nodes
                                    GameObject objToDelete = GameObject.Find(nodeReference.ToString() + "temp(Clone)");

                                    //destroying the object
                                    Destroy(objToDelete);

                                    yield return null;

                                    outputCommands.text = outputCommands.text + "Game object collides with other unrelated gameobjects" + "\n";
                                    errorFlag = true;
                                }
                            }
                        }
                        else
                        {
                            outputCommands.text = outputCommands.text + "New coordinates are same as old coordinates" + "\n";
                            errorFlag = true;
                        }
                    }
                    else {
                        outputCommands.text = outputCommands.text + "Cannot move objects with rotations on x and z axis, on object axis" + "\n";
                        errorFlag = true;
                    }
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    outputCommands.text = outputCommands.text + "The gameobject to be moved does not exist" + "\n";
                    errorFlag = true;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a MoveBy gloabl_axis simple command and consists of 6 words, ex: MoveBy object_axis compound table_1 left 1
            else if (words.Length == 6 && commandFlag == 17)
            {
                //the parent node
                string parent = words[3];

                //direction
                string direction = words[4];

                float spacing = 0f;

                //check the spacing from here
                if (words[5].Equals("Touching", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 0f;
                }
                else if (words[5].Equals("near", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 1f;
                }
                else if (words[5].Equals("moderate", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 2.5f;
                }
                else if (words[5].Equals("far", StringComparison.OrdinalIgnoreCase))
                {
                    spacing = 4f;
                }
                else
                { //if it is not a word
                    spacing = float.Parse(words[5]);
                }

                //displacement
                string displacement = spacing.ToString();

                float displacementNo = float.Parse(displacement);

                //creating the parent node
                Node parentNode = new Node(parent);

                //create a temporary tree and recalculate the coordinates
                //create a copy of the current Tree
                IDictionary<Node, Node[]> tempTree = TreeSet.ToDictionary(entry => entry.Key, entry => entry.Value);

                //check that the object to be moved exists
                Boolean resultCheckExistParent = checkExist(TreeSet, parentNode);

                if (resultCheckExistParent)//if the parent object exists
                {
                    //break relations with other nodes for the parentNode
                    tempTree = removeNodeAsChild(tempTree, parentNode);

                    //add the new records and change coordinates for the parent
                    tempTree = changeTreeRelationsObject(tempTree, parentNode, direction, displacementNo);

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

                    //check if the object has any rotation on x and z axis
                    if (objOld.transform.rotation.eulerAngles.z == 0 && objOld.transform.rotation.eulerAngles.x == 0)
                    {
                        //current position of the object
                        Vector3 currentPosition = objOld.transform.position;

                        float x = parentNode.returnX();
                        float y = parentNode.returnY();
                        float z = parentNode.returnZ();

                        if (currentPosition != new Vector3(x, y, z))
                        { //if they are not equal
                          //check that the object to be moved exists
                          //Boolean resultCheckExist = checkExist(TreeSet, parentNode);

                            Boolean create = true;

                            //loop through the nodes to be moved
                            foreach (Node node in nodesToMove)
                            {
                                Node nodeCopy = node.CopyDeep();

                                //change name as it is a temporary duplicate
                                nodeCopy.setValue(node.ToString() + "Copy");

                                //check that the nodes can be moved
                                yield return StartCoroutine(checkSceneCollidersParent(nodeCopy));

                                //yield return new WaitForSeconds(creationDelay);//handing control to Update()

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

                                    yield return null;

                                    ////find the object
                                    //String originalObject = node.ToString().Replace("Copy", "");

                                    GameObject objToUpdate = GameObject.Find(node.ToString() + "temp(Clone)");
                                    objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                    yield return null;

                                    //find the created
                                    if (GameObject.Find(node.ToString() + "(Clone)"))
                                    {
                                        GameObject objToMove = GameObject.Find(node.ToString() + "(Clone)");
                                        objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                        yield return null;
                                    }
                                }

                                //update the Tree
                                TreeSet = tempTree;

                                //display the command in the output commands box
                                outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4] + " " + words[5] + "\n";
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

                                    yield return null;
                                }


                                outputCommands.text = outputCommands.text + "Gameobject(s) collide(s) with other unrelated gameobjects" + "\n";
                                errorFlag = true;
                            }
                        }
                    }
                    else {
                        outputCommands.text = outputCommands.text + "Cannot move objects with rotations on x and z axis, on object axis" + "\n";
                        errorFlag = true;
                    }
                }

                ////create a copy of the old parent node for later
                //Node parentNodeCopy = parentNode.Copy();
                else
                {
                    outputCommands.text = outputCommands.text + "The gameobject to be moved does not exist" + "\n";
                    errorFlag = true;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Change color command and consists of 4 words, ex: Texture cube_1 red
            else if (words.Length == 3 && commandFlag == 7)
            {
                //the object to change
                string objectToChange = words[1];

                //the new color of the object
                string color = words[2];

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
                        if (color.Equals("brick", StringComparison.OrdinalIgnoreCase))
                        {
                            //changing the material
                            myRenderer.material = Brick;
                        }

                        //output message
                        outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + "\n";
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
                        outputCommands.text = outputCommands.text + "Unable to find the color" + "\n";
                        errorFlag = true;
                    }
                }
                else
                {
                    outputCommands.text = outputCommands.text + "Game object does not exist" + "\n";
                    errorFlag = true;
                }
            }

            //if the command is a (Simple) Rotate command, ex: RotateY simple table_1 by 90
            else if (words.Length == 4 && commandFlag == 8)
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
                    string rotation = words[3];

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

                        //keep the other rotations the same
                        parentNode.setRotationX(currentRotationQuaternion.eulerAngles.x);
                        parentNode.setRotationZ(currentRotationQuaternion.eulerAngles.z);

                        //create a temporary tree and recalculate the rotation
                        //create a copy of the Tree
                        IDictionary<Node, Node[]> tempTree = new Dictionary<Node, Node[]>();

                        graphObject.DeepCopyTree(tempTree, Tree);

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
                        yield return StartCoroutine(checkSceneCollidersParent(parentNode));

                        //yield return new WaitForSeconds(creationDelay);//handing control to Update()

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

                            yield return null;

                            //find the object
                            String originalObject = parentNode.ToString().Replace("Copy", "");

                            GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                            objToUpdate.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());

                            yield return null;

                            //find the created object
                            if (GameObject.Find(originalObject + "(Clone)"))
                            {
                                GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                objToMove.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());

                                yield return null;
                            }

                            //update the Tree
                            Tree = tempTree;

                            //output message
                            outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + "\n";
                        }
                        else
                        {
                            //destroy the copy gameobject
                            GameObject objToDelete = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //destroying the object
                            Destroy(objToDelete);

                            yield return null;

                            //output command
                            outputCommands.text = outputCommands.text + "Game object collided with other unrelated gameobjects" + "\n";
                            errorFlag = true;
                        }
                    }
                    else {
                        //output command
                        outputCommands.text = outputCommands.text + "New rotation and old rotation are the same" + "\n";
                        errorFlag = true;
                    }
                }
                else
                {
                    //output command
                    outputCommands.text = outputCommands.text + "Object does not exist" + "\n";
                    errorFlag = true;
                }

                //reset the object list
                listOfObjects.Clear();
            }

            //if the command is a (Compound) Rotate command, ex: RotateY compound table_1 by 180
            else if (words.Length == 4 && commandFlag == 9)
            {
                //check on which axis it is being rotated

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
                    string rotation = words[3];

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
                            yield return StartCoroutine(checkSceneCollidersParent(node));

                            //handing control to Update()
                            //yield return new WaitForSeconds(creationDelay);

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

                                yield return null;

                                //find the object
                                String originalObject = node.ToString().Replace("Copy", "");

                                GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                                objToUpdate.transform.rotation = Quaternion.Euler(node.getRotationX(), node.getRotationY(), node.getRotationZ());
                                objToUpdate.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                yield return null;

                                //find the created
                                if (GameObject.Find(originalObject + "(Clone)"))
                                {
                                    GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                    objToMove.transform.rotation = Quaternion.Euler(node.getRotationX(), node.getRotationY(), node.getRotationZ());
                                    objToMove.transform.position = new Vector3(node.returnX(), node.returnY(), node.returnZ());

                                    yield return null;
                                }
                            }

                            //update the Tree
                            TreeSet = tempTree;

                            //output command
                            outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + "\n";
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

                                yield return null;
                            }

                            //output command
                            outputCommands.text = outputCommands.text + "Gameobject collides with other unrelated gameobjects" + "\n";
                            errorFlag = true;
                        }


                    }
                    else
                    {
                        //output command
                        outputCommands.text = outputCommands.text + "Old and new rotations are the same" + "\n";
                        errorFlag = true;
                    }
                }
                else
                {
                    //output command
                    outputCommands.text = outputCommands.text + "Object does not exist" + "\n";
                    errorFlag = true;
                }

                //reset the list of objects
                listOfObjects.Clear();
            }

            //if the command is a Rotate command, ex: RotateX simple table_1 90
            else if (words.Length == 4 && commandFlag == 10)
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
                    string rotation = words[3];

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
                        
                        //create a copy of the Tree
                        IDictionary<Node, Node[]> tempTree = new Dictionary<Node, Node[]>();

                        graphObject.DeepCopyTree(tempTree, Tree);

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
                        yield return StartCoroutine(checkSceneCollidersParent(parentNode));

                        //yield return new WaitForSeconds(creationDelay);//handing control to Update()

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

                            yield return null;

                            //find the object
                            String originalObject = parentNode.ToString().Replace("Copy", "");

                            GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                            objToUpdate.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());
                            objToUpdate.transform.position = new Vector3(parentNode.returnX(), parentNode.returnY(), parentNode.returnZ());

                            yield return null;

                            //find the created object
                            if (GameObject.Find(originalObject + "(Clone)"))
                            {
                                GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                objToMove.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());
                                objToMove.transform.position = new Vector3(parentNode.returnX(), parentNode.returnY(), parentNode.returnZ());

                                yield return null;
                            }



                            //update the Tree
                            TreeSet = tempTree;

                            //output command
                            outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + "\n";
                        }
                        else
                        {
                            //destroy the copy gameobject
                            GameObject objToDelete = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //destroying the object
                            Destroy(objToDelete);

                            yield return null;

                            //output command
                            outputCommands.text = outputCommands.text + "Gameobject collides with other unrelated gameobjects" + "\n";

                            errorFlag = true;
                        }
                    }
                    else
                    {
                        //output command
                        outputCommands.text = outputCommands.text + "Old and new rotations are the same" + "\n";
                        errorFlag = true;
                    }
                }
                else
                {
                    //output command
                    outputCommands.text = outputCommands.text + "Object does not exist" + "\n";

                    errorFlag = true;
                }

                //reset the object list
                listOfObjects.Clear();
            }

            //if the command is a Rotate command, ex: RotateZ simple table_1 90
            else if (words.Length == 4 && commandFlag == 11)
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
                    string rotation = words[3];

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
                        //create a copy of the Tree
                        IDictionary<Node, Node[]> tempTree = new Dictionary<Node, Node[]>();

                        graphObject.DeepCopyTree(tempTree, Tree);

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
                        yield return StartCoroutine(checkSceneCollidersParent(parentNode));

                        //yield return new WaitForSeconds(creationDelay);//handing control to Update()

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

                            yield return null;

                            //find the object
                            String originalObject = parentNode.ToString().Replace("Copy", "");

                            GameObject objToUpdate = GameObject.Find(originalObject + "temp(Clone)");
                            objToUpdate.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());
                            objToUpdate.transform.position = new Vector3(parentNode.returnX(), parentNode.returnY(), parentNode.returnZ());

                            yield return null;

                            //find the created object
                            if (GameObject.Find(originalObject + "(Clone)"))
                            {
                                GameObject objToMove = GameObject.Find(originalObject + "(Clone)");
                                objToMove.transform.rotation = Quaternion.Euler(parentNode.getRotationX(), parentNode.getRotationY(), parentNode.getRotationZ());
                                objToMove.transform.position = new Vector3(parentNode.returnX(), parentNode.returnY(), parentNode.returnZ());

                                yield return null;
                            }

                            //update the Tree
                            TreeSet = tempTree;

                            //output command
                            outputCommands.text = outputCommands.text + words[0] + " " + words[1] + " " + words[2] + " " + words[3] + "\n";
                        }
                        else
                        {
                            //destroy the copy gameobject
                            GameObject objToDelete = GameObject.Find(parentNode.ToString() + "temp(Clone)");

                            //destroying the object
                            Destroy(objToDelete);

                            yield return null;

                            //output command
                            outputCommands.text = outputCommands.text + "Gameobject collides with other unrelated gameobjects" + "\n";
                            errorFlag = true;
                        }
                    }
                    else
                    {
                        //output command
                        outputCommands.text = outputCommands.text + "Old and new rotations are the same" + "\n";
                        errorFlag = true;
                    }
                }
                else
                {
                    //output command
                    outputCommands.text = outputCommands.text + "Object does not exist" + "\n";
                    errorFlag = true;
                }

                //reset the object list
                listOfObjects.Clear();
            }

            //if the command is a Break command, ex: Break table_1
            else if (words.Length == 2 && commandFlag == 12)
            {
                //the parent node
                string parent = words[1];

                //creating the parent node
                Node parentNode = new Node(parent);

                //create a copy of the Tree
                IDictionary<Node, Node[]> tempTree = new Dictionary<Node, Node[]>();

                graphObject.DeepCopyTree(tempTree, Tree);

                //check that the object to break exists
                Boolean checkObjectExists = checkExist(TreeSet, parentNode);

                if (checkObjectExists) {
                    //call the break method
                    tempTree = breakRelationships(tempTree, parentNode);

                    TreeSet = tempTree;

                    //output command
                    outputCommands.text = outputCommands.text + words[0] + " " + words[1] + "\n";
                }
                else {
                    //output command
                    outputCommands.text = outputCommands.text + "Object does not exist" + "\n";
                    errorFlag = true;
                }
            }

            ////if the command is a Break command, ex: Reset rotations table_1 -- LIMITATION
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
                outputCommands.text = outputCommands.text + "Invalid input" + "\n";

                //mark an error
                errorFlag = true;
            }

            //if there is not an error
            if (!errorFlag) {
                //update the undo stack
                undoStack.Push(currentTree);

                //reset the redo stack
                redoStack = new Stack<IDictionary<Node, Node[]>>();
            }

            ////reset the box after adding the command
            //commandBox.text = "";
            //placeholder.text = "Enter commands...";
            //placeholder.color = Color.black;
        }
    }

    //function used to undo an action
    public void undo() {
        StartCoroutine(undo2());
    }

    public IEnumerator undo2()
    {
        Test graphObject = new Test();

        //check if the stack is empty
        if (undoStack.Count != 0)
        {
            //the previous tree
            IDictionary<Node, Node[]> newCurrentTree = undoStack.Pop();

            //create a copy of the Tree
            IDictionary<Node, Node[]> currentTree = new Dictionary<Node, Node[]>();

            graphObject.DeepCopyTree(currentTree, Tree);

            //add the action to the redo action
            redoStack.Push(currentTree);

            //delete current objects
            yield return StartCoroutine(deleteScene2WithoutStack());

            //update the current Tree
            Tree = newCurrentTree;

            //create scene
            yield return StartCoroutine(createSceneLoad2());

            outputCommands.text = outputCommands.text + "Undo" + "\n";
        }
        else
        {
            outputCommands.text = outputCommands.text + "No actions to undo" + "\n";
        }
    }

    //quits the application
    public void Quit()
    {
        Application.Quit();
    }

    //function used to redo an action
    public void redo()
    {
        StartCoroutine(redo2());
    }

    public IEnumerator redo2() {
        Test graphObject = new Test();

        //check if the stack is empty
        if (redoStack.Count != 0)
        {
            //the previous tree
            IDictionary<Node, Node[]> newCurrentTree = redoStack.Pop();

            //create a copy of the Tree
            IDictionary<Node, Node[]> currentTree = new Dictionary<Node, Node[]>();

            graphObject.DeepCopyTree(currentTree, Tree);

            //add the action to the redo action
            undoStack.Push(currentTree);

            //delete current objects
            yield return StartCoroutine(deleteScene2WithoutStack());

            //update the current Tree
            Tree = newCurrentTree;

            //create scene
            yield return StartCoroutine(createSceneLoad2());

            outputCommands.text = outputCommands.text + "Redo" + "\n";
        }
        else
        {
            outputCommands.text = outputCommands.text + "No actions to redo" + "\n";
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
    public Boolean AddObjectsToDatastructure(IDictionary<Node, Node[]> tree, Node parent, Node child)
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
        Test graphObject = new Test();

        //create a copy of the temporary tree
        //create a copy of the Tree
        IDictionary<Node, Node[]> treeCopy = new Dictionary<Node, Node[]>();

        graphObject.DeepCopyTree(treeCopy, tree);

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

                Node changeNode = new Node("test");

                //iterate through the tree
                foreach (KeyValuePair<Node, Node[]> entry2 in treeCopy)
                {
                    if (entry2.Key.ToString().Equals(entry.Key.ToString()))
                    {
                        //store reference to the node
                        changeNode = entry2.Key;
                    }
                }

                //overwrite the current children
                treeCopy[changeNode] = listToKeep.ToArray();
            }
        }

        return treeCopy; //successful creation
    }

    //method used to remove the children of a node
    public IDictionary<Node, Node[]> removeChildren(IDictionary<Node, Node[]> tree, Node parent)
    {
        Test graphObject = new Test();

        //create a copy of the Tree
        IDictionary<Node, Node[]> treeCopy = new Dictionary<Node, Node[]>();

        graphObject.DeepCopyTree(treeCopy, tree);

        //Checking if the child exists
        if (tree.ContainsKey(parent))
        {
            foreach (KeyValuePair<Node, Node[]> entry in tree)
            {
                if (entry.Key.ToString().Equals(parent.ToString())) {
                    //delete the node's relationships with children-----
                    treeCopy[entry.Key] = new Node[] { };
                }
            }
        }

        return treeCopy; //successful creation
    }

    //method used to break a node free from all relationships
    public IDictionary<Node, Node[]> breakRelationships(IDictionary<Node, Node[]> tree, Node child)
    {
        Test graphObject = new Test();

        //create a copy of the Tree
        IDictionary<Node, Node[]> treeCopy = new Dictionary<Node, Node[]>();

        graphObject.DeepCopyTree(treeCopy, tree);

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
            outputCommands.text = outputCommands.text + "There are no objects to create" + "\n";
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

                    yield return null;

                    if (found == true)
                    {
                        found = false;//reset the found flag
                    }

                    //create the object in real time to check for later
                    StartCoroutine(checkSceneCollidersParentTrue(entry.Key));

                    yield return null;

                    if (found == true)
                    {
                        found = false;//reset the found flag
                    }
                }
            }

            outputCommands.text = outputCommands.text + "Succesfully created the scene" + "\n";
        }
    }

    public void createSceneLoad()
    {
        //calling the create method
        StartCoroutine(createSceneLoad2());
    }

    public IEnumerator createSceneLoad2()
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
                    yield return StartCoroutine(checkSceneCollidersParentNew(entry.Key));

                    //yield return new WaitForSeconds(creationDelay);

                    if (found == true)
                    {
                        found = false;//reset the found flag
                    }

                    //create the object in real time to check for later
                    yield return StartCoroutine(checkSceneCollidersParentTrueNew(entry.Key));

                    //yield return new WaitForSeconds(creationDelay);

                    if (found == true)
                    {
                        found = false;//reset the found flag
                    }
                }
            }
        }
    }

    //A and B are test methods for the cooroutines
    public IEnumerator A()
    {
        UnityEngine.Debug.Log("Starting coroutine A");

        yield return StartCoroutine(B());
        //yield return new WaitForEndOfFrame(); // Wait for end of frame before continuing to Update()

        UnityEngine.Debug.Log("Back to A");

        yield return StartCoroutine(B());
        //yield return new WaitForEndOfFrame(); // Wait for end of frame before continuing to Update()

        UnityEngine.Debug.Log("Back to A");
    }

    public IEnumerator B()
    {
        x = 1;
        UnityEngine.Debug.Log("Starting coroutine B");
        yield return null;
        UnityEngine.Debug.Log("Coroutine B finished");
    }

    public void deleteSceneWithoutStack()
    {
        StartCoroutine(deleteScene2WithoutStack());
    }

    //delete objects in the scene
    public IEnumerator deleteScene2WithoutStack()
    {
        if (Tree.Count != 0)
        {
            //iterate through the tree
            foreach (KeyValuePair<Node, Node[]> entry in Tree)
            {
                //check if the temp version exists
                String originalObject = entry.Key.ToString();

                //if the temp object exists
                if (GameObject.Find(originalObject + "temp(Clone)"))
                {
                    //find temporary object
                    GameObject objToDelete = GameObject.Find(originalObject + "temp(Clone)");

                    //delete the object
                    Destroy(objToDelete);

                    yield return null;

                    //if the created object exists
                    if (GameObject.Find(originalObject + "(Clone)"))
                    {
                        //find the created obj
                        objToDelete = GameObject.Find(originalObject + "(Clone)");

                        //delete the object
                        Destroy(objToDelete);

                        yield return null;
                    }
                }
                else
                {
                    //outputCommands.text = outputCommands.text + "Error" + "\n";
                }
            }

            //outputCommands.text = outputCommands.text + "Deleted scene" + "\n";

            //reset the tree
            Tree = new Dictionary<Node, Node[]>();
        }
        else
        {
            //outputCommands.text = outputCommands.text + "There are no objects in the scene" + "\n";
        }
    }

    public void deleteScene() {
        StartCoroutine(deleteScene2());
    }

    //delete objects in the scene
    public IEnumerator deleteScene2() {
        if (Tree.Count != 0)
        {
            //create a deep copy of the Tree
            IDictionary<Node, Node[]> tempTree = Tree.ToDictionary(entry => entry.Key, entry => entry.Value);

            //reset the redo stack
            redoStack = new Stack<IDictionary<Node, Node[]>>();

            //Add the current Tree to the undo stack
            undoStack.Push(tempTree);

            //iterate through the tree
            foreach (KeyValuePair<Node, Node[]> entry in Tree)
            {
                //check if the temp version exists
                String originalObject = entry.Key.ToString();

                //if the temp object exists
                if (GameObject.Find(originalObject + "temp(Clone)"))
                {
                    //find temporary object
                    GameObject objToDelete = GameObject.Find(originalObject + "temp(Clone)");

                    //delete the object
                    Destroy(objToDelete);

                    yield return null;

                    //if the created object exists
                    if (GameObject.Find(originalObject + "(Clone)"))
                    {
                        //find the created obj
                        objToDelete = GameObject.Find(originalObject + "(Clone)");

                        //delete the object
                        Destroy(objToDelete);

                        yield return null;
                    }
                }
                else {
                    //outputCommands.text = outputCommands.text + "Error" + "\n";
                }
            }

            outputCommands.text = outputCommands.text + "Deleted scene" + "\n";

            //reset the tree
            Tree = new Dictionary<Node, Node[]>();
        }
        else {
            outputCommands.text = outputCommands.text + "There are no objects in the scene" + "\n";
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
        yield return null;

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

    public IEnumerator checkSceneCollidersParentTrueNew(Node node)
    {
        canCreateTrue = true;

        //passing the attributes to the global temporary node
        temporaryNode = node;

        //giving permission to the update method
        yield return null;

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
    }

    public IEnumerator checkSceneCollidersParent(Node node)
    {
        canCreate = true;

        //passing the attributes to the global temporary node
        temporaryNode = node;

        //giving permission to the update method
        yield return null;

        //object is created in the update method

        //get name of the parent
        string name = node.ToString() + "temp";

        //here we should check
        if (GameObject.Find(name + "(Clone)") != null)
        {
            //found
            found = true;

            //get a list of objects with tag Enemy
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy")
                .ToArray();

            foreach (GameObject obj in gameObjects) {
                obj.tag = "Final";
            }

            //if (GameObject.FindWithTag("Enemy")) {
            //    //if found, change the tag to Final from Enemy
            //    GameObject obj = GameObject.FindWithTag("Enemy");
            //    obj.tag = "Final";
            //}
            
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

    public IEnumerator checkSceneCollidersParentNew(Node node)
    {
        canCreate = true;

        //passing the attributes to the global temporary node
        temporaryNode = node;

        //giving permission to the update method
        yield return null;

        //object is created in the update method

        //get name of the parent
        string name = node.ToString() + "temp";

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

                float[] fakeCoordinates = node.getFakeCoordinates();

                entry.Key.setFakeCoordinates(fakeCoordinates);
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
                        graphObject.AddObjectsToDatastructure(tempTree, entry.Key, entry2.Key);

                        //change the coordinates of the node
                        float[] coordinates = calculatePrepositionCoordinates(entry.Key, entry2.Key);

                        //iterate through the tree to find the child node
                        foreach (KeyValuePair<Node, Node[]> entry3 in tree)
                        {
                            //find the child node
                            if (entry3.Key.ToString().Equals(futureChildNode.ToString()))
                            {
                                entry3.Key.setCoordinates(coordinates);

                                //setting the fake coordinates
                                GameObject entryObject = entry3.Key.getObjectTrue();

                                Vector3 sizeParentObj = entryObject.transform.localScale;

                                //height variable 
                                float height = sizeParentObj.y;

                                //create a copy of the coords array
                                float[] fakeCoords = coordinates.ToArray();

                                //change y axis coord
                                fakeCoords[1] = fakeCoords[1] - height / 2;

                                //set the fake coordinates
                                entry3.Key.setFakeCoordinates(fakeCoords);
                            }
                        }
                    }
                }
            }
        }

        //set the temporary tree to the copy
        return tempTree;
    }

    //used to add an already created node as a child of another
    public IDictionary<Node, Node[]> changeTreeRelationsGlobal(IDictionary<Node, Node[]> tree, Node node, string direction, float displacement)
    {
        //runner object to use methods
        Test graphObject = new Test();

        //create a copy of the temporary tree
        IDictionary<Node, Node[]> tempTree = tree.ToDictionary(entry => entry.Key, entry => entry.Value);

        //iterate through the tree to find the node
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //find the new parent node
            if (entry.Key.ToString().Equals(node.ToString()))
            {
                //change the coordinates of the node
                (float[], float[]) results = calculatePrepositionCoordinatesGlobal(entry.Key, direction, displacement);

                entry.Key.setCoordinates(results.Item1);

                //set the fake coordinates
                entry.Key.setFakeCoordinates(results.Item2);

                //update child instances just in case
                foreach (KeyValuePair<Node, Node[]> entry2 in tree)
                {
                    Node[] temp = entry2.Value;

                    foreach (Node nodeTemp in temp)
                    {
                        //find the node
                        if (nodeTemp.ToString().Equals(node.ToString()))
                        {
                            nodeTemp.setCoordinates(results.Item1);

                            //set the fake coordinates
                            nodeTemp.setFakeCoordinates(results.Item2);
                        }
                    }
                }
            }
        }

        //set the temporary tree to the copy
        return tempTree;
    }

    //used to add an already created node as a child of another
    public IDictionary<Node, Node[]> changeTreeRelationsObject(IDictionary<Node, Node[]> tree, Node node, string direction, float displacement)
    {
        //runner object to use methods
        Test graphObject = new Test();

        //create a copy of the temporary tree
        IDictionary<Node, Node[]> tempTree = tree.ToDictionary(entry => entry.Key, entry => entry.Value);

        //iterate through the tree to find the node
        foreach (KeyValuePair<Node, Node[]> entry in tree)
        {
            //find the new parent node
            if (entry.Key.ToString().Equals(node.ToString()))
            {
                //change the coordinates of the node
                (float[], float[]) results = calculatePrepositionCoordinatesObject(entry.Key, direction, displacement);

                entry.Key.setCoordinates(results.Item1);

                //set the fake coordinates
                entry.Key.setFakeCoordinates(results.Item2);

                //update child instances just in case
                foreach (KeyValuePair<Node, Node[]> entry2 in tree)
                {
                    Node[] temp = entry2.Value;

                    foreach (Node nodeTemp in temp)
                    {
                        //find the node
                        if (nodeTemp.ToString().Equals(node.ToString()))
                        {
                            nodeTemp.setCoordinates(results.Item1);

                            //set the fake coordinates
                            nodeTemp.setFakeCoordinates(results.Item2);
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

                //pass it to every instance of the node
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

                                            //setting the fake coordinates
                                            GameObject entryObject = entry2.Key.getObjectTrue();

                                            Vector3 sizeParentObj = entryObject.transform.localScale;

                                            //height variable 
                                            float height = sizeParentObj.y;

                                            //create a copy of the coords array
                                            float[] fakeCoords = coordinates.ToArray();

                                            //change y axis coord
                                            fakeCoords[1] = fakeCoords[1] - height/2;

                                            //set the fake coordinates
                                            entry2.Key.setFakeCoordinates(fakeCoords);
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
                spacing = 1f;
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

    public (float[], float[]) calculatePrepositionCoordinatesObject(Node parent, String direction, float displacement)
    {
        //reference to gameObject
        GameObject objCreated = GameObject.Find(parent.ToString() + "temp(Clone)");

        //coordinates of the parentObject
        float X = parent.returnX();
        float Y = parent.returnY();
        float Z = parent.returnZ();

        //fake coordinates of the parentObject
        float[] fakeCoordinates = parent.getFakeCoordinates();

        float fX = fakeCoordinates[0];
        float fY = fakeCoordinates[1];
        float fZ = fakeCoordinates[2];

        //object vector3
        Vector3 coords;

        //object vector3
        Vector3 fakeCoords;

        //array to store the new coordinates
        float[] coordinates = null;

        //depending on the preposition
        if (direction.Equals("left"))
        {
            coords = new Vector3(X, Y, Z) + (1 * objCreated.transform.right);

            //stays the same
            coords.y = Y;

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            parent.setCoordinates(coordinates);

            fakeCoords = new Vector3(coords.x, fY, coords.z);

            fakeCoordinates = new float[3] { fakeCoords.x, fakeCoords.y, fakeCoords.z };

            parent.setFakeCoordinates(fakeCoordinates);
        }
        else if (direction.Equals("right"))
        {
            coords = new Vector3(X, Y, Z) - (1 * objCreated.transform.right);

            //stays the same
            coords.y = Y;

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            parent.setCoordinates(coordinates);

            fakeCoords = new Vector3(coords.x, fY, coords.z);

            fakeCoordinates = new float[3] { fakeCoords.x, fakeCoords.y, fakeCoords.z };

            parent.setFakeCoordinates(fakeCoordinates);
        }
        else if (direction.Equals("front"))
        {
            coords = new Vector3(X, Y, Z) + (1 * objCreated.transform.forward);

            //stays the same
            coords.y = Y;

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            parent.setCoordinates(coordinates);

            fakeCoords = new Vector3(coords.x, fY, coords.z);

            fakeCoordinates = new float[3] { fakeCoords.x, fakeCoords.y, fakeCoords.z };

            parent.setFakeCoordinates(fakeCoordinates);
        }
        else if (direction.Equals("behind"))
        {
            coords = new Vector3(X, Y, Z) - (1 * objCreated.transform.forward);

            //stays the same
            coords.y = Y;

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            //setting the calculated coordinates to the child
            parent.setCoordinates(coordinates);

            fakeCoords = new Vector3(coords.x, fY, coords.z);

            fakeCoordinates = new float[3] { fakeCoords.x, fakeCoords.y, fakeCoords.z };

            parent.setFakeCoordinates(fakeCoordinates);
        }

        return (coordinates, fakeCoordinates);
    }

    public (float[], float[]) calculatePrepositionCoordinatesGlobal(Node parent, String direction, float displacement)
    {
        //coordinates of the parentObject
        float X = parent.returnX();
        float Y = parent.returnY();
        float Z = parent.returnZ();

        //fake coordinates of the parentObject
        float[] fakeCoordinates = parent.getFakeCoordinates();

        float fX = fakeCoordinates[0];
        float fY = fakeCoordinates[1];
        float fZ = fakeCoordinates[2];

        //object vector3
        Vector3 coords;

        //object vector3
        Vector3 fakeCoords;

        //array to store the new coordinates
        float[] coordinates = null;

        //depending on the preposition
        if (direction.Equals("left"))
        {
            X = X + (1*(displacement));

            coords = new Vector3(X, Y, Z);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            fX = fX + (1 * (displacement));

            fakeCoords = new Vector3(fX, fY, fZ);

            fakeCoordinates = new float[3] { fakeCoords.x, fakeCoords.y, fakeCoords.z };

            //setting the calculated coordinates to the child
            parent.setCoordinates(coordinates);

            parent.setFakeCoordinates(fakeCoordinates);
        }
        else if (direction.Equals("right"))
        {
            X = X - (1 * (displacement));

            coords = new Vector3(X, Y, Z);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            fX = fX - (1 * (displacement));

            fakeCoords = new Vector3(fX, fY, fZ);

            fakeCoordinates = new float[3] { fakeCoords.x, fakeCoords.y, fakeCoords.z };

            //setting the calculated coordinates to the child
            parent.setCoordinates(coordinates);

            parent.setFakeCoordinates(fakeCoordinates);
        }
        else if (direction.Equals("front"))
        {
            Z = Z + (1 * (displacement));

            coords = new Vector3(X, Y, Z);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            fZ = fZ + (1 * (displacement));

            fakeCoords = new Vector3(fX, fY, fZ);

            fakeCoordinates = new float[3] { fakeCoords.x, fakeCoords.y, fakeCoords.z };

            //setting the calculated coordinates to the child
            parent.setCoordinates(coordinates);

            parent.setFakeCoordinates(fakeCoordinates);
        }
        else if (direction.Equals("behind"))
        {
            Z = Z - (1 * (displacement));

            coords = new Vector3(X, Y, Z);

            coordinates = new float[3] { coords.x, coords.y, coords.z };

            fZ = fZ - (1 * (displacement));

            fakeCoords = new Vector3(fX, fY, fZ);

            fakeCoordinates = new float[3] { fakeCoords.x, fakeCoords.y, fakeCoords.z };

            //setting the calculated coordinates to the child
            parent.setCoordinates(coordinates);

            parent.setFakeCoordinates(fakeCoordinates);
        }

        return (coordinates, fakeCoordinates);
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
                spacing = 1f;
            }
            else if (child.getSpacing().Equals("moderate", StringComparison.OrdinalIgnoreCase))
            {
                spacing = 2.5f;
            }
            else if (child.getSpacing().Equals("far", StringComparison.OrdinalIgnoreCase))
            {
                spacing = 4f;
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

    //method used to turn the dictionary datastructure into a string (Accuracy)
    public void AccuTreeToStringExtended(IDictionary<Node, Node[]> tree)
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
                ValueArrayToString = string.Join("|", Array.ConvertAll(entry.Value, item => item.AccuToStringWithLocationAndPreposition())); //turning the value array into a string divided by '|'
                KeyToString = entry.Key.AccuToStringWithLocationAndPreposition(); //turning the Node to a string
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

    //method used to turn the dictionary datastructure into a string (Aesthethic)
    public void AestTreeToStringExtended(IDictionary<Node, Node[]> tree)
    {
        //check if the tree has objects in it
        if (tree.Count() == 0)
        {
            outputCommands.text = outputCommands.text + "There are no objects in the scene" + "\n";
            TreeStringSave = null;
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
                ValueArrayToString = string.Join("|", Array.ConvertAll(entry.Value, item => item.AestToStringWithLocationAndPreposition())); //turning the value array into a string divided by '|'
                KeyToString = entry.Key.AestToStringWithLocationAndPreposition(); //turning the Node to a string
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
        AestTreeToStringExtended(Tree);

        //check if the TreeString is empty or not

        //Connect to MSAGL
        ProcessStartInfo psi = new ProcessStartInfo(@"C:\Users\User\Downloads\FYPGerard\MSAGL2\MSAGL2\bin\Release\MSAGL2.exe");
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
        AccuTreeToStringExtended(Tree);

        //Add a UI
        //Directory location
        StreamWriter streamWriter = new StreamWriter(@"C:\Users\User\Downloads\FYPGerard\Save.txt");

        //Write the TreeString in the Text file
        streamWriter.WriteLine(TreeStringSave);

        //Close the file
        streamWriter.Close();
    }

    public void loadScene() {
        StartCoroutine(loadScene2());
    }

    public IEnumerator loadScene2()
    {
        //incase of errors
        Boolean errorFlag = false;
        //Just in case
        //Tree.Keys.Any(x => x.ToString().Equals(parent.ToString());

        //runner object to use methods
        Test graphObject = new Test();

        //Directory location
        StreamReader streamReader = new StreamReader(@"C:\Users\User\Downloads\FYPGerard\Save.txt");

        //checking if the notepad is empty
        if ((TreeStringLoad = streamReader.ReadLine()) != null)
        {
            //create a temporary version of the tree to later add unto undo stack
            IDictionary<Node, Node[]> currentTree = Tree.ToDictionary(entry => entry.Key, entry => entry.Value);

            //create a temporary version of the tree for after load function
            IDictionary<Node, Node[]> futureTree = new Dictionary<Node, Node[]>();

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

                        //setting the fake coordinates
                        GameObject entryObject = parent.getObjectTrue();

                        Vector3 sizeParentObj = entryObject.transform.localScale;

                        //height variable 
                        float height = sizeParentObj.y;

                        //create a copy of the coords array
                        float[] fakeCoords = floatCoordinates.ToArray();

                        //change y axis coord
                        fakeCoords[1] = fakeCoords[1] - height / 2;

                        //set the fake coordinates
                        parent.setFakeCoordinates(fakeCoords);

                        //setting the rotations
                        parent.setRotationX(intRotation[0]);
                        parent.setRotationY(intRotation[1]);
                        parent.setRotationZ(intRotation[2]);

                        //create a deep copy of the node
                        newNodeCopyParent = parent.CopyDeep();

                        //the parent node should not already exist
                        if (!futureTree.Keys.Any(x => x.ToString().Equals(parent.ToString())))
                        {
                            //Add the node to the Tree
                            Boolean resultAddChildren = graphObject.AddObjectsToDatastructure(futureTree, newNodeCopyParent, null); //adding the first node to the tree - it has no children

                            if (resultAddChildren == false)
                            {
                                placeholder.text = "Could not add node!";
                                placeholder.color = Color.red;

                                errorFlag = true;
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

                            //setting the fake coordinates
                            GameObject entryObject2 = child.getObjectTrue();

                            Vector3 sizeChildObj = entryObject2.transform.localScale;

                            //height variable 
                            float height2 = sizeChildObj.y;

                            //create a copy of the coords array
                            float[] fakeCoords2 = floatCoordinates.ToArray();

                            //change y axis coord
                            fakeCoords2[1] = fakeCoords2[1] - height2 / 2;

                            //set the fake coordinates
                            child.setFakeCoordinates(fakeCoords2);

                            //setting the rotations
                            child.setRotationX(intRotation[0]);
                            child.setRotationY(intRotation[1]);
                            child.setRotationZ(intRotation[2]);

                            //create a deep copy of the child
                            Node newNodeCopy = child.CopyDeep();

                            //to add the child node, the parent node must exist
                            if (futureTree.Keys.Any(x => x.ToString().Equals(parent.ToString())))
                            {
                                Boolean resultAddChildren = graphObject.AddObjectsToDatastructure(futureTree, newNodeCopyParent, newNodeCopy);

                                if (resultAddChildren == false)
                                {
                                    placeholder.text = "Could not add relationship!";
                                    placeholder.color = Color.red;

                                    errorFlag = true;
                                }
                            }
                        }
                    }
                }
            }

            //if there are no errors
            if (!errorFlag)
            {
                //Update the indexes
                foreach (KeyValuePair<Node, Node[]> entry in futureTree)
                {
                    //increase the counter for each object
                    GetIndexForObject(entry.Key.getObjectType());
                }

                //Delete the current scene
                yield return StartCoroutine(deleteScene2WithoutStack());

                //update undo stack
                undoStack.Push(currentTree);

                //reset redo stack
                redoStack = new Stack<IDictionary<Node, Node[]>>();

                //update the Tree
                Tree = futureTree;

                //Create the scene
                yield return StartCoroutine(createSceneLoad2());

                outputCommands.text = outputCommands.text + "Load successful" + "\n";
            }
            else {
                outputCommands.text = outputCommands.text + "Error during loading" + "\n";
            }
            
        }
        else {
            outputCommands.text = outputCommands.text + "Load file is empty" + "\n";
        }
    }

    //to minimise the Main UI
    public void minimiseUI()
    {
        minButton.SetActive(false);
        Menu.gameObject.SetActive(false);
        maxButton.SetActive(true);
    }

    //to maximise the Main UI
    public void maximiseUI()
    {
        maxButton.SetActive(false);
        CameraMenu.SetActive(false);
        guideMenu.SetActive(false);
        LabelMenu.SetActive(false);
        Menu.gameObject.SetActive(true);
        minButton.SetActive(true);
    }

    //to open the Camera Menu
    public void getCameras()
    {
        minButton.SetActive(false);
        Menu.gameObject.SetActive(false);
        CameraMenu.SetActive(true);
    }

    //to show faces, method attached to the GO button
    public void getFaces()
    {
        //get the name of the object
        String nameOfObject = currentObjects[facesDropdown.value];

        //if the string is not null
        if (!nameOfObject.Equals("null") ){
            //check if the object has been registered in the faceTree
            if (faceTree.ContainsKey(nameOfObject))
            {
                //if true set to false, if false set to true
                Boolean status = faceTree[nameOfObject];
                faceTree[nameOfObject] = !status;
            }
            else
            {
                //initialise the faces to show, i.e. true
                faceTree[nameOfObject] = true;
            }

            //get a list of all the gameobjects
            GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("Enemy")
                .Concat(GameObject.FindGameObjectsWithTag("Final"))
                .ToArray();

            //get a list of all the gameobjects
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("FaceTag").ToArray();

            //if the current state is true
            if (faceTree[nameOfObject])
            {
                //enable the face labels
                EnableCollisionBoxes(gameObjects2);
                EnableFaces(gameObjects, nameOfObject);
            }
            else
            {
                //disable the face labels
                DisableFaces(gameObjects, nameOfObject);
            }
        }
    }

    public void DisableFaces(GameObject[] gameObjects, string NameOfObject)
    {
        //iterate through the list
        foreach (GameObject obj in gameObjects)
        {
            //get reference to the parent object
            string objName = obj.transform.parent.name;

            //the object will be the temporary node
            objName = objName.Replace("temp(Clone)", "");

            if (objName.Equals(NameOfObject)) {
                if (obj.GetComponent<Canvas>() != null)//the object has a canvas
                {
                    //get the renderer component of the object
                    Canvas canvas = obj.GetComponent<Canvas>();

                    //check if it is already inactive
                    if (canvas.enabled)
                    {
                        canvas.enabled = false;
                    }
                }
            }
        }
    }

    public void EnableFaces(GameObject[] gameObjects, string NameOfObject)
    {
        //iterate through the list
        foreach (GameObject obj in gameObjects)
        {
            //get reference to the parent object
            string objName = obj.transform.parent.name;

            //the object will be the temporary node
            objName = objName.Replace("temp(Clone)", "");

            if (objName.Equals(NameOfObject))
            {
                if (obj.GetComponent<Canvas>() != null)//the object has a canvas
                {
                    //get the renderer component of the object
                    Canvas canvas = obj.GetComponent<Canvas>();

                    //check if it is already active
                    if (!canvas.enabled)
                    {
                        canvas.enabled = true;
                    }
                }
            }
        }
    }

    //to open the Guide Menu
    public void getGuideMenu()
    {
        minButton.SetActive(false);
        Menu.gameObject.SetActive(false);
        guideMenu.SetActive(true);
    }

    //to open the Label Menu
    public void getLabels()
    {
        minButton.SetActive(false);
        Menu.gameObject.SetActive(false);
        LabelMenu.SetActive(true);

        //instantiate the dropdown values
        currentObjects = getCurrentObjects();
        //if the currentObjects list is empty
        if (currentObjects.Count == 0)
        {
            currentObjects.Add("null");
        }
        SetOptions(currentObjects, facesDropdown); //set the option
        facesDropdown.value = 0;//set to first option
    }

    //to change the status of the boolean flag
    public void ChangeNameStatus()
    {
        //change the flag when method is called
        if (nameFlag)
        {
            nameFlag = false;
        }
        else
        {
            nameFlag = true;
        }

        NameStatus();
    }

    //to call the required methods depending on the status of the boolean flag
    public void NameStatus()
    {
        //get reference to the image component
        Image buttonImage = nameButton.GetComponent<Image>();

        //reference to the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        //get a list of all the gameobjects
        GameObject[] gameObjects2 = GameObject.FindGameObjectsWithTag("Enemy")
            .Concat(GameObject.FindGameObjectsWithTag("Final"))
            .ToArray();

        //get a list of all the gameobjects
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("NameTag").ToArray();

        //show the names
        if (nameFlag)
        {
            EnableCollisionBoxes(gameObjects2);
            EnableNames(gameObjects);

            //green color for the UI
            Color newColor = new Color(0.6953385f, 1f, 0.654717f);

            //set the color of the image
            buttonImage.color = newColor;
        }
        //hide the names
        else
        {
            DisableNames(gameObjects);

            //red color for the UI
            Color newColor = new Color(1f, 0.7215686f, 0.7215686f);

            //set the color of the image
            buttonImage.color = newColor;
        }
    }

    public void DisableNames(GameObject[] gameObjects)
    {
        //iterate through the list
        foreach (GameObject obj in gameObjects)
        {
            if (obj.GetComponent<Canvas>() != null)//the object has a canvas
            {
                //get the renderer component of the object
                Canvas canvas = obj.GetComponent<Canvas>();

                //check if it is already inactive
                if (canvas.enabled)
                {
                    canvas.enabled = false;
                }
            }
        }
    }

    public void EnableNames(GameObject[] gameObjects)
    {
        //iterate through the list of Canvases
        foreach (GameObject obj in gameObjects)
        {
            //if the object has a canvas
            if (obj.GetComponent<Canvas>() != null)
            {
                //change the names of the objects ------

                //get reference to the transform component
                Transform canvasTransform = obj.GetComponent<Transform>();

                //get reference to the text object, which is the only child of the canvas
                GameObject textObject = canvasTransform.GetChild(0).gameObject;

                //get reference to the textComponent
                TextMeshProUGUI textComponent = textObject.GetComponentInChildren<TextMeshProUGUI>();

                //get reference to the parent of the canvas gameObject
                GameObject parentObject = obj.transform.parent.gameObject;

                //get the name of the parentObject
                string name = parentObject.name;

                //we know that the object will always be a temporary gameObject
                name = name.Replace("temp(Clone)", "");

                //change the label of the object to the name
                textComponent.text = name;

                //show gameObject label ------

                //get the renderer component of the object
                Canvas canvas = obj.GetComponent<Canvas>();

                //check if it is already inactive
                if (!canvas.enabled)
                {
                    canvas.enabled = true;
                }
            }
        }
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
        //get reference to the image component
        Image buttonImage = collisionBoxButton.GetComponent<Image>();

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

            //green color
            Color newColor = new Color(0.6953385f, 1f, 0.654717f);

            //set the color of the image
            buttonImage.color = newColor;
        }
        //hide the collision boxes
        else
        {
            DisableCollisionBoxes(gameObjects);

            //red color
            Color newColor = new Color(1f, 0.7215686f, 0.7215686f);

            //set the color of the image
            buttonImage.color = newColor;
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

    //dont know what this is??
    public Boolean getRotationStatus()
    {
        return rotationAxisX;
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