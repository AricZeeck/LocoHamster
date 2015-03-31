using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{    
    // Gain access to
    // internal var gameManager : GameManager;
    // internal var fPCamera : GameObject;
    // internal var fPController : GameObject;

    //arrays
    //public string[] playerSettingsArray = new string[];
    public List<string> playerSettings = new List<string>();

    // Settings menu variables
    float walkSpeed = 4.0f;                    // element 0
    float turnSpeed;                    // element 1
    bool useText;                       // element 2
    bool objectDescriptions;            // element 3
    float fxVolume = 1;                 // element 4
    float ambVolume = 1;                // element 5
    float musicVolume = 1;              // element 6
    float voiceVolume = 1;              // element 7
    int colorElement;                   // element 8

    // Cursor color
    Texture colorSwatch = new Texture();
    Color moColor = new Color();        // current mouseover color

    // Misc Menu Text
    string introText = "Welcome to Sphere";
    string infoText = "Interactive objects- cursor changes color on \nmouse over, click to activate\nInventory- i key or pick on icon at lower left to \naccess\nGeneral- objects can be combined in scene or \nin inventory";
    string navText = "Navigation:\nP/Down, W/S to move forward?Backward\n A/D to strife left and right \nLeft and Right arrowkeys to turn/look around, \nor <shift> or right mouse button \nand move mouse to turn and look around";

    int groupWidth = 750;                   // width of the main GUI group   
    Rect buttnRect = new Rect(0, 120, 130, 30);    

    // Menu Managment
    bool mainMenu = false;                  // flag for main menu
    bool confirmDialog = false;             // flag for yes/no on quit dialog
    bool creditsScreen = false;             // flag for credits dialog
    bool end = false;                       // flag for end sequence

    /// START METHOD
    // Use this for initialization
    void Start()
    {
        
        //playerSettingsArray = new string[8];

        Color[] cursorColors;
        cursorColors = new Color[8];
        UpdateSettings();
        // gameManager = GameObject.Find("Control Center").GetComponent(GameManager);
        // fPCamera = GameObject.Find("Main Camera");
        // fpController = GameObject.Find("First Person Controller");

    // Initialize GUI
        // walkSpeed = fPController.GetComponent(CharacterMotor).movement.maxForwardSpeed;
        // turnSpeed = fPController.GetComponent(FPAdventureInputController).rotationSpeed;
        // useText = gameManager.useText;
        // objectDescription = gameManager.useLongDesc;
        // moColor = gameManager.mouseOverColor;        // get color, because there is no element
        // UpdateSettings();                            // update the array that holds the settings
    }

    // UPDATE METHOD
    // Update is called once per frame
    void Update()
    {
        // toggle the main menu off and on
        if (Input.GetKeyDown("f1"))
        {
            if (end) Application.Quit(); // end now
            if (mainMenu) mainMenu = false;
            else mainMenu = true;
        } 
    }


    // ON GUI METHOD    
    void OnGUI()
    {
        // **** MAIN MENU ****
        if (mainMenu)
        {
            // Make master group on the center of trhe screen
            GUI.BeginGroup (new Rect(Screen.width / 2 - 375, Screen.height / 2 - 270, 750, 500));

            //*** Title and Intro
            GUI.Box(new Rect(0, 0, 750, 80), "Main Menu");
            GUI.Label(new Rect(30, 0, 650, 100), introText);       
            
            //*** Navigation and Instructiuons
            GUI.BeginGroup (new Rect (0, 90, 370, 340));
            GUI.Box(new Rect(0, 0, 370, 340), "General Information and Navigation");
            GUI.Label(new Rect (20, 50, 350, 120), infoText);
            GUI.Label(new Rect(20, 160, 350, 350), navText);
            if (GUI.Button(new Rect(20, 270, 150, 40), "Credits"))
            {
                mainMenu = false;           // turn off the main menu
                creditsScreen = true;       // turn on the credits dialog
            }
            GUI.EndGroup();     // end navigation & instructions group

            //*** Settings
            GUI.BeginGroup(new Rect(380, 90, 370, 340));
            GUI.Box(new Rect(0, 0, 370, 340), "Settings");

            //*** FPC speeds
            GUI.Label(new Rect(25, 35, 100, 30), "Walk Speed");
            walkSpeed = GUI.HorizontalSlider(new Rect(150, 40, 100, 20), walkSpeed, 0.0f, 20.0f);

            GUI.Label(new Rect(25, 60, 100, 30), "Turn Speed");
            turnSpeed = GUI.HorizontalSlider(new Rect(150, 65, 100, 20), turnSpeed, 0.0f, 40.0f);

            //*** Text
            int textY = 90;
            useText = GUI.Toggle(new Rect(30, textY, 120, 30), useText, " Use Text");
            objectDescriptions = GUI.Toggle(new Rect(30, textY + 30, 120, 30), objectDescriptions, " Use Descriptionas");

            //*** Audio
            int audioY = 120;
            audioY += 30;
            GUI.Label(new Rect(25, audioY, 100, 30), "FX Volume");
            audioY += 10;
            fxVolume = GUI.HorizontalSlider(new Rect(150, audioY, 100, 20), fxVolume, 0.0f, 1.0f);
            audioY += 14;
            GUI.Label(new Rect(25, audioY, 100, 30), "Ambient Volume");
            audioY += 10;
            ambVolume = GUI.HorizontalSlider(new Rect(150, audioY, 100, 20), ambVolume, 0.0f, 1.0f);
            audioY += 14;
            GUI.Label(new Rect(25, audioY, 100, 30), "Music Volume");
            audioY += 10;
            musicVolume = GUI.HorizontalSlider(new Rect(150, audioY, 100, 20), musicVolume, 0.0f, 1.0f);
            audioY += 14;
            GUI.Label(new Rect(25, audioY, 100, 30), "Dialog Volume");
            audioY += 10;
            voiceVolume = GUI.HorizontalSlider(new Rect(150, audioY, 100, 20), voiceVolume, 0.0f, 1.0f);       

            GUI.EndGroup();     // end settings group

            //*** Buttons options
            GUI.Box(new Rect(0, 440, 750, 40), "");

            //this is a local variable that gets changed after each button is added
            Rect buttnRectTemp = new Rect(20, 445, buttnRect.width, buttnRect.height);

            if (GUI.Button( buttnRectTemp, "New Game"))
            {
                //Application.LoadLevel("MainLevel");     // Start the main level
            }

            buttnRectTemp.x += buttnRect.width + 15;      // shift the starting position over for the next one
            if (GUI.Button (buttnRectTemp, "Save Game"))
            {
                // save the current game
                //MenuMode(false);          // turn off menu mode
                //SaveGame(true);           // save current game
            }

            buttnRectTemp.x += buttnRect.width + 15;      // shift the starting position over for the next one
            if (GUI.Button(buttnRectTemp, "Load Game"))
            {
                //LoadGame();               // load the saved game
                //MenuMode(false);          // turn off menu mode
            }

            buttnRectTemp.x += buttnRect.width + 15;      // shift the starting position over for the next one
            if (GUI.Button(buttnRectTemp, "Quit"))
            {
                confirmDialog = true;          // turn on confirm mneu
               // mainMenu = false;               // turn off menu
            }

            buttnRectTemp.x += buttnRect.width + 15;      // shift the starting position over for the next one
            if (GUI.Button(buttnRectTemp, "Resume"))
            {
                mainMenu = false;           // turn off the menu
                // MenuMode(false);         // turn off menu mode
            }

        //track changes
            if (GUI.changed)
            {
                //UpdateSettings();           // process the changes
            }
            // End the main group
            GUI.EndGroup();
            
        } // end the main menu if conditional

    }// end the OnGui function

    void UpdateSettings()
    {
        /*playerSettingsArray[0] = walkSpeed.ToString();
        playerSettingsArray[1] = turnSpeed.ToString();
        playerSettingsArray[2] = useText.ToString();
        playerSettingsArray[3] = objectDescriptions.ToString();
        playerSettingsArray[4] = fxVolume.ToString();
        playerSettingsArray[5] = ambVolume.ToString();
        playerSettingsArray[6] = musicVolume.ToString();
        playerSettingsArray[7] = voiceVolume.ToString();*/

        playerSettings.Add(walkSpeed.ToString());

        foreach (string s in playerSettings)
        {

        }
        
    //    // update the settings in the GameManager
    //    gameManager.NewSettings(playerSettings); 
    }

    void UpdateControls() // update GUI from the settings array
    {
        //walkspeed = float.Parse (playerSettings[0]);
        //turnSpeed = float.Parse(playerSettings[1]);
        //useText = bool.Parse (playerSettings[2]);
        //objectDescription = bool.Parse(playerSettings[3]);
        //fxVolume = float.Parse(playerSetting[4]);
        //ambVolume = float.Parse(playerSettings[5]);
        //musicVolume = float.Parse(playerSettings[6]);
        //voiceVolume = float.Parse(playerSettings[7]);
        //colorElement = float.Parse(playerSettings[8]);
        //moColor = cursorColors[colorElement];   // update current swatch
    }

    //void BoolParse (string psValue)
    //{
    //    if (psValue == "true") return true;
    //    else return false;
    //}

} // end MenuManager