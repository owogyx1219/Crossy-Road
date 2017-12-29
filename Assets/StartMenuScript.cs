using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    public Canvas startMenu;
    public Canvas tutorialMenu;
    public Canvas exitMenu;
    public Canvas change_charactersMenu;
    public Canvas scoreMenu;

    public Button startText;
    public Button tutorialText;
    public Button exitText;
    public Button characterText;
    public Button scoreText;
    public static bool inMenu;
    public static bool inTutorial;
    public static bool inExit;
    public static bool inChangeCharacter;
    public static bool playFromMenu = false;
    private Image[] images = new Image[7];
    private bool[] inC = new bool[7] { false, false, false, false, false, false, false};
    private string[] charNames = new string[7] { "magician", "hero", "fighter", "ninja","zeus","crazyhat","leprach" };
    private GameObject[] characters = new GameObject[7];
    private Image[] TutorialImages = new Image[6];
    private string[] tutorialNames = new string[6] { "T1", "T2", "T3", "T4", "T5", "T6" };
    private bool[] inT = new bool[6] { false, false, false, false, false, false};
    private GameObject currChar = null;
    private Button prev;
    private Button next;

    RawImage CharacterBackGround;
    RawImage TutorialBackGround;
    // Use this for initialization
    void Start()
    {
        startMenu = GameObject.Find("StartMenu").GetComponent<Canvas>();
        Debug.Log("YES");
        startText = GameObject.Find("button start").GetComponent<Button>();
        characterText = GameObject.Find("button change character").GetComponent<Button>();
        tutorialText = GameObject.Find("button tutorial").GetComponent<Button>();
        exitText = GameObject.Find("button exit").GetComponent<Button>();

        exitMenu = GameObject.Find("ExitMenu").GetComponent<Canvas>();

        tutorialMenu = GameObject.Find("TutorialMenu").GetComponent<Canvas>();
        scoreMenu = GameObject.Find("Canvas").GetComponent<Canvas>();
        scoreText = GameObject.Find("CountText").GetComponent<Button>();

        change_charactersMenu = GameObject.Find("CharacterMenu").GetComponent<Canvas>();
        CharacterBackGround = GameObject.Find("CharBackground").GetComponent<RawImage>();
        TutorialBackGround = GameObject.Find("TutorialBackground").GetComponent<RawImage>();
        prev = GameObject.Find("prev").GetComponent<Button>();
        next = GameObject.Find("next").GetComponent<Button>();
        for (int i = 0; i < 7; i++)
        {
            images[i] = GameObject.Find(charNames[i]).GetComponent<Image>();
            images[i].enabled = false;
            string name = char.ToUpper(charNames[i][0]) + charNames[i].Substring(1);
            characters[i] = GameObject.Find(name);
            characters[i].SetActive(false);
        }

        for (int i = 0; i < 6; i++)
        {
            TutorialImages[i] = GameObject.Find(tutorialNames[i]).GetComponent<Image>();
            TutorialImages[i].enabled = false;
        }

        startMenu.enabled = true;
        scoreMenu.enabled = true;
        exitMenu.enabled = false;
        tutorialMenu.enabled = false;
        change_charactersMenu.enabled = false;
        inMenu = true;
        inTutorial = false;
        inExit = false;
        inChangeCharacter = false;
    }

    void PlayPress()
    {
        //mainPlane.started = true; // allow plane to move
        if (currChar == null)
        {
            currChar = characters[0];
            characters[0].SetActive(true);
        }
        startMenu.enabled = false; // hide start menu
        inMenu = false;
        scoreMenu.enabled = true;
        playFromMenu = true;
        // disable all start menu button
        startText.enabled = false;
        tutorialText.enabled = false;
        exitText.enabled = false;
        characterText.enabled = false;
    }

    public void ExitPress()
    {
        startMenu.enabled = false; // hide start menu
        exitMenu.enabled = true; // show quit menu
        inMenu = false;
        inExit = true;
        scoreMenu.enabled = false;
        change_charactersMenu.enabled = false;

        // disable all start menu button
        startText.enabled = false;
        tutorialText.enabled = false;
        exitText.enabled = false;
        characterText.enabled = false;
    }

    public void NoPress()
    {
        //mainPlane.started = false;
        startMenu.enabled = true; // show start menu
        tutorialMenu.enabled = false;
        exitMenu.enabled = false; // hide quit menu
        scoreMenu.enabled = false;
        change_charactersMenu.enabled = false;
        // enable all start menu button
        startText.enabled = true;
        tutorialText.enabled = true;
        exitText.enabled = true;
        characterText.enabled = true;
    }
    public void showCharacter()
    {
        inMenu = false;
        inChangeCharacter = true;
        change_charactersMenu.enabled = true;
        startMenu.enabled = false;
        tutorialMenu.enabled = false;
        scoreMenu.enabled = false;
        images[0].enabled = true;
        inC[0] = true;
        // disable all start menu button
        startText.enabled = false;
        tutorialText.enabled = false;
        exitText.enabled = false;
        characterText.enabled = false;

    }
    public void showInstr()
    {
        inMenu = false;
        inTutorial = true;
        TutorialImages[0].enabled = true;
        inT[0] = true;
        startMenu.enabled = false; // hide start menu
        tutorialMenu.enabled = true; // show instruction
        scoreMenu.enabled = false;


        // disable all start menu button
        startText.enabled = false;
        tutorialText.enabled = false;
        exitText.enabled = false;
        characterText.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

        // in main menu
        if (inMenu == true)
        {

            if (Input.GetKeyDown(KeyCode.X))
            {
                PlayPress();

                //mainPlane.justStart = true;
                //mainPlane.started = true; // allow plane to move

                // set audio in play
                //AudioSource source = mainPlane.GetComponent<AudioSource>();
                //source.clip = mainPlane.engine_min;
                //source.loop = true;
                //source.Play();
            }

			if (Input.GetKeyDown(KeyCode.Y))
            {
                showInstr();
                inMenu = false;
                scoreMenu.enabled = false;

            }
			if (Input.GetKeyDown(KeyCode.B))
            {
                showCharacter();
            }

			if (Input.GetKeyDown(KeyCode.A))
            {
                ExitPress();
            }
        }

        // in turoial windows
        else if (inTutorial == true)
        {
            if(inT[0] == true)
            {
           
                //To the next page
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inT[0] = false;
                    inT[1] = true;
                    TutorialImages[0].enabled = false;
                    TutorialImages[1].enabled = true;
                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    inT[0] = false;
                    TutorialImages[0].enabled = false;
                    tutorialMenu.enabled = false;
                    inTutorial = false;
                    inMenu = true;
                    NoPress();
                   
                }
            }
            else if (inT[1] == true)
            {
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inT[1] = false;
                    inT[0] = true;
                    TutorialImages[1].enabled = false;
                    TutorialImages[0].enabled = true;
                }
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inT[1] = false;
                    inT[2] = true;
                    TutorialImages[1].enabled = false;
                    TutorialImages[2].enabled = true;
                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    inT[1] = false;
                    TutorialImages[1].enabled = false;
                    tutorialMenu.enabled = false;
                    inTutorial = false;
                    inMenu = true;
                    NoPress();
                }
            }
            else if (inT[2] == true)
            {
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inT[2] = false;
                    inT[1] = true;
                    TutorialImages[2].enabled = false;
                    TutorialImages[1].enabled = true;
                }
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inT[2] = false;
                    inT[3] = true;
                    TutorialImages[2].enabled = false;
                    TutorialImages[3].enabled = true;
                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    inT[2] = false;
                    TutorialImages[2].enabled = false;
                    tutorialMenu.enabled = false;
                    inTutorial = false;
                    inMenu = true;
                    NoPress();
                }
            }
            else if (inT[3] == true)
            {
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inT[3] = false;
                    inT[2] = true;
                    TutorialImages[3].enabled = false;
                    TutorialImages[2].enabled = true;
                }
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inT[3] = false;
                    inT[4] = true;
                    TutorialImages[3].enabled = false;
                    TutorialImages[4].enabled = true;
                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    inT[3] = false;
                    TutorialImages[3].enabled = false;
                    tutorialMenu.enabled = false;
                    inTutorial = false;
                    inMenu = true;
                    NoPress();
                }
            }
            else if (inT[4] == true)
            {
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inT[4] = false;
                    inT[3] = true;
                    TutorialImages[4].enabled = false;
                    TutorialImages[3].enabled = true;
                }
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inT[4] = false;
                    inT[5] = true;
                    TutorialImages[4].enabled = false;
                    TutorialImages[5].enabled = true;
                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    inT[4] = false;
                    TutorialImages[4].enabled = false;
                    tutorialMenu.enabled = false;
                    inTutorial = false;
                    inMenu = true;
                    NoPress();
                }
            }
            else if (inT[5] == true)
            {
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inT[5] = false;
                    inT[4] = true;
                    TutorialImages[5].enabled = false;
                    TutorialImages[4].enabled = true;
                }
				if (Input.GetKeyDown(KeyCode.Y))
                {
                    inT[5] = false;
                    TutorialImages[5].enabled = false;
                    tutorialMenu.enabled = false;
                    inTutorial = false;
                    inMenu = true;
                    NoPress();
                }
            }
        } // end in tutorial

        // in change character windows
        else if (inChangeCharacter == true)
        {
            if (inC[0] == true)
            {
                // prev.enabled = false;
                // next.enabled = true;
                //To the next page
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inC[0] = false;
                    inC[1] = true;
                    images[0].enabled = false;
                    images[1].enabled = true;
                }
				if (Input.GetKeyDown(KeyCode.Y))
                {
                    Debug.Log("sET I");
                    inC[0] = false;
                    images[0].enabled = false;
                    change_charactersMenu.enabled = false;
                    if (currChar != null)
                        currChar.SetActive(false);
                    currChar = characters[0];
                    characters[0].SetActive(true);
                    inChangeCharacter = false;
                }
            }
            else if (inC[1] == true)
            {
                //prev.enabled = false;
                // next.enabled = true;
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inC[1] = false;
                    inC[0] = true;
                    images[1].enabled = false;
                    images[0].enabled = true;

                }
                //To the next page
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inC[1] = false;
                    inC[2] = true;
                    images[1].enabled = false;
                    images[2].enabled = true;

                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    Debug.Log("Set");
                    inC[1] = false;
                    images[1].enabled = false;
                    change_charactersMenu.enabled = false;
                    if (currChar != null)
                        currChar.SetActive(false);
                    currChar = characters[1];
                    characters[1].SetActive(true);
                    inChangeCharacter = false;
                    //NoPress();
                }
            }
            else if (inC[2] == true)
            {
                // prev.enabled = true;
                //next.enabled = true;
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inC[2] = false;
                    inC[1] = true;
                    images[2].enabled = false;
                    images[1].enabled = true;

                }
                //To the next page
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inC[2] = false;
                    inC[3] = true;
                    images[2].enabled = false;
                    images[3].enabled = true;

                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    Debug.Log("sET I");
                    inC[2] = false;
                    images[2].enabled = false;
                    change_charactersMenu.enabled = false;
                    if (currChar != null)
                        currChar.SetActive(false);
                    currChar = characters[2];
                    characters[2].SetActive(true);
                    inChangeCharacter = false;
                    //NoPress();
                }
            }
            else if (inC[3] == true)
            {
                // prev.enabled = true;
                //next.enabled = true;
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inC[3] = false;
                    inC[2] = true;
                    images[3].enabled = false;
                    images[2].enabled = true;

                }
                //To the next page
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inC[3] = false;
                    inC[4] = true;
                    images[3].enabled = false;
                    images[4].enabled = true;

                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    Debug.Log("sET I");
                    inC[3] = false;
                    images[3].enabled = false;
                    change_charactersMenu.enabled = false;
                    if (currChar != null)
                        currChar.SetActive(false);
                    currChar = characters[3];
                    characters[3].SetActive(true);
                    inChangeCharacter = false;
                    //NoPress();
                }
            }
            else if (inC[4] == true)
            {
                // prev.enabled = true;
                //next.enabled = true;
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inC[4] = false;
                    inC[3] = true;
                    images[4].enabled = false;
                    images[3].enabled = true;

                }
                //To the next page
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inC[4] = false;
                    inC[5] = true;
                    images[4].enabled = false;
                    images[5].enabled = true;

                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    Debug.Log("sET I");
                    inC[4] = false;
                    images[4].enabled = false;
                    change_charactersMenu.enabled = false;
                    if (currChar != null)
                        currChar.SetActive(false);
                    currChar = characters[4];
                    characters[4].SetActive(true);
                    inChangeCharacter = false;
                    //NoPress();
                }
            }
            else if (inC[5] == true)
            {
                // prev.enabled = true;
                //next.enabled = true;
				if (Input.GetKeyDown(KeyCode.X))
                {
                    inC[5] = false;
                    inC[4] = true;
                    images[5].enabled = false;
                    images[4].enabled = true;

                }
                //To the next page
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inC[5] = false;
                    inC[6] = true;
                    images[5].enabled = false;
                    images[6].enabled = true;

                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    Debug.Log("sET I");
                    inC[5] = false;
                    images[5].enabled = false;
                    change_charactersMenu.enabled = false;
                    if (currChar != null)
                        currChar.SetActive(false);
                    currChar = characters[5];
                    characters[5].SetActive(true);
                    inChangeCharacter = false;
                    //NoPress();
                }
            }
            else if (inC[6] == true)
            {
                // prev.enabled = true;
                // next.enabled = false;

                //To the prev page
				if (Input.GetKeyDown(KeyCode.B))
                {
                    inC[6] = false;
                    inC[5] = true;
                    images[6].enabled = false;
                    images[5].enabled = true;
                }

				if (Input.GetKeyDown(KeyCode.Y))
                {
                    Debug.Log("sET I");
                    inC[6] = false;
                    images[6].enabled = false;
                    change_charactersMenu.enabled = false;
                    if (currChar != null)
                        currChar.SetActive(false);
                    currChar = characters[6];
                    characters[6].SetActive(true);
                    inChangeCharacter = false;
                    //NoPress();
                }
            }
        }
        // in exit windows
        else if (inExit == true)
        {
			if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("quit");
                Application.Quit();
            }

			if (Input.GetKeyDown(KeyCode.B))
            {
                inExit = false;
                inMenu = true;
                NoPress();
            }
        }
        //if (!inMenu && !inTutorial && !inExit && !inChangeCharacter)
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inMenu = true;
                NoPress();
            }
        }
    }
}