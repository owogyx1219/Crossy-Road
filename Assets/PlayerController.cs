using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public static float GeneratePosDelta = 9.417f;
    public static float forwardstep = 4.7085f;
    public static float jumpstep = 4.7085f, jumpUp = 2.5f;
    private static float forwardDisplacement = 0.0f;

    public static int queueSize = 8;
    public static int numDelegates = 4;
    public float GeneratePos = 0.0f;
    public Vector3 gPosition;

    int functionIdx = 0;

    public delegate GameObject GenerateDelegate(Vector3 pos, int width);
    GenerateDelegate[] delegates;

    public static Text scoreText;
    public static int scoreCount;

    public float translateDead = 0.30f;

    Queue<GameObject> q;

    int degree = 0;

    //Vector3 player_pos;

    int WeightedRandomNumber(float[] weightedP)
    {
        int size = weightedP.Length;

        float[] prange = new float[size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                prange[i] = prange[i] + weightedP[i];
            }
        }

        float randomN = Random.Range(0.0f, 1.0f);
        int index = size - 2;
        while (index >= 0)
        {
            if (randomN > prange[index])
            {
                return index + 1;
            }
            else
            {
                index--;
            }
        }
        return 0;

    }

    void Start()
    {
        functionIdx = (int)(Mathf.Floor(Random.Range(0.0f, numDelegates - 0.1f)));
        scoreCount = 0;
        scoreText = GameObject.FindWithTag("score").GetComponent<Text>();
        scoreText.text = "Score: " + scoreCount.ToString();

        /*
        //player_pos = transform.position;
        string path = "Assets/VoxelCharacters/Prefabs/Hero.prefab";
        Object hero = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        Vector3 old_pos = transform.position;
        GameObject actualHero = Instantiate(hero, old_pos, Quaternion.identity) as GameObject;
        actualHero.transform.Rotate(0, 90, 0);

        GameObject main_character = GameObject.FindWithTag("MainCharacter");
        actualHero.transform.parent = main_character.transform;
        */

        //main_character.GetComponent<Renderer>().enabled = false;
        //actualHero.GetComponent<Renderer>().enabled = true;

        /*two character overlap
        string path = "Assets/VoxelCharacters/Prefabs/Hero.prefab";
        Object hero = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        Vector3 old_pos = transform.position;
        GameObject actualHero = Instantiate(hero, old_pos, Quaternion.identity) as GameObject;
        actualHero.transform.Rotate(0, 90, 0);

        GameObject main_character = GameObject.FindWithTag("MainCharacter");
        actualHero.transform.parent = main_character.transform;
        */
        //main_character.GetComponent<Renderer>().enabled = false;
        //actualHero.GetComponent<Renderer>().enabled = true;

        /*
        string path = "Assets/VoxelCharacters/Prefabs/Hero.prefab";
        Object hero = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        Vector3 old_pos = transform.position;
        GameObject actualHero = Instantiate(hero, old_pos, Quaternion.identity) as GameObject;
        GameObject main_character = GameObject.FindWithTag("MainCharacter");
        actualHero.transform.parent = main_character.transform;
        actualHero.transform.Rotate(0, 90, 0);
        actualHero.GetComponent<Renderer>().enabled = false;
        main_character.GetComponent<Renderer>().enabled = false;
        */

        /*
        string path = "Assets/VoxelCharacters/Prefabs/Hero.prefab";
        Object hero = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        Vector3 old_pos = transform.position;
        GameObject actualHero = Instantiate(hero, old_pos, Quaternion.identity) as GameObject;
        actualHero.transform.Rotate(0, 90, 0);
        GameObject main_camera = GameObject.FindWithTag("MainCamera");
        main_camera.transform.parent = actualHero.transform;
        */
        //Camera main_camera = actualHero.AddComponent<Camera>();
        //main_camera.transform.position = new Vector3(0, 20, -30);
        //Destroy(this.gameObject);

        // actualHero.AddComponent("main_camera") as Camera;
        //Camera main_camera = actualHero.AddComponent<Camera>();
        //main_camera.transform.position = new Vector3(0, 20, -30);
        //Destroy(this.gameObject);
        //this.gameObject = actualHero.gameObject;

        gPosition = this.gameObject.transform.position;
        delegates = new GenerateDelegate[numDelegates];
        delegates[0] = new GenerateDelegate(GenerateLane.GenerateLanes);
        delegates[1] = new GenerateDelegate(GenerateWood.GenerateWoods);
        delegates[2] = new GenerateDelegate(GenerateRiver.GenerateRivers);
        delegates[3] = new GenerateDelegate(GenerateNeighboorhood.GenerateNeighboorhoods);

        Debug.Log("setting up functors");



        q = new Queue <GameObject>();

        //generate the initial scene
        for (int i = 0; i < queueSize; i++)
        {
           
            if (i == 0)
            {
                Debug.Log("inside for loop");
                //GeneratePos = GeneratePos + GeneratePosDelta;
                gPosition[0] = GeneratePos; gPosition[1] = 0.01f;
                GameObject first_obj = delegates[1](gPosition, 175);
                q.Enqueue(first_obj);
                continue;
            }
            else
            {
                GeneratePos = GeneratePos + GeneratePosDelta;
            }
            gPosition[0] = GeneratePos; gPosition[1] = 0.01f;
            float[] weightedP = { 0.4f, 0.2f, 0.2f, 0.2f };
            int functionIdx1 = WeightedRandomNumber(weightedP);
            
            //int functionIdx = (int)(Mathf.Floor(Random.Range(0.0f, numDelegates - 0.1f)));
            GameObject obj = delegates[functionIdx1](gPosition, 120);
            q.Enqueue(obj);
        }

        Debug.Log("Start initial scene");

        /*
        for (int i = 0; i < 4; i++)
        {
            float index = Random.Range(0.0f, 2.0f);
            if (index >= 0.0f && index <= 1.0f)
            {
                GeneratePos = GeneratePos + GeneratePosDelta;
                gPosition[0] = GeneratePos; gPosition[1] = 0.01f;
                GameObject lane = GenerateLane.GenerateLanes(gPosition, 175);
            }
            else
            {
                GeneratePos = GeneratePos + GeneratePosDelta;
                gPosition[0] = GeneratePos; gPosition[1] = 0.01f;
                GameObject lane = GenerateRiver.GenerateRivers(gPosition, 175);
            }
        }*/

        /*
        gPosition[0] += 5.0f; gPosition[1] = 0.01f;
        GameObject lane = GenerateLane.GenerateLanes(gPosition, 175);
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEnd.gameEnd == true) return;
        if (StartMenuScript.inChangeCharacter || StartMenuScript.inExit || StartMenuScript.inTutorial || StartMenuScript.inMenu)
        {
            return;
        }
        if (StartMenuScript.playFromMenu)
        {
            degree += 90;
            transform.Rotate(0, -270, 0);
            StartMenuScript.playFromMenu = false;
        }
        //reset when it's out of boundrary 
        if (Mathf.Abs(transform.position.x) >= 500.0f)
        {
            //Vector3 newposition = new Vector3(0.0f, 5.0f, 250.0f);
            //transform.position = newposition;
        }
        else
        {
            //move forward
            //Input.GetKeyDown("space")
            /*float forwardDistance = Input.GetAxis("Forward");
            if (Mathf.Abs(forwardDistance) > translateDead ||*/
            //if(Input.GetButtonDown("Y")||Input.GetButtonDown("X") || Input.GetButtonDown("B") )
            //{
            Debug.Log(transform.position.y);
            Debug.Log(transform.position.y < 0.6f);
			if (Mathf.Abs(transform.position.x) <= 500.0f && Input.GetKeyDown(KeyCode.Y))
                {
                    transform.Translate(0, 0, forwardstep);
                    if(degree == 0)
                    {
                        forwardDisplacement += forwardstep;
                    }
                }
				if (Input.GetKeyDown(KeyCode.A) && transform.position.y < 0.6f)
                {
                //Debug.Log("hi");

                    transform.Translate(0, jumpUp, jumpstep);
                    if (degree == 0)
                    {
                        forwardDisplacement += jumpstep;
                    }
                }

				if (Input.GetKeyDown(KeyCode.X))
                {
                    transform.Rotate(0,270,0);
                    degree -= 90;
                }

				if (Input.GetKeyDown(KeyCode.B))
                {
                    transform.Rotate(0, 90, 0);
                    degree += 90;
                }

                if (forwardDisplacement >= GeneratePosDelta)
                {
                    forwardDisplacement = 0.0f;

                    scoreCount += 10;
                    scoreText.text = "Score: " + scoreCount.ToString();

                    //generate new lane/wood/river/neighborhood
                    GeneratePos = GeneratePos + GeneratePosDelta;
                    gPosition[0] = GeneratePos; gPosition[1] = 0.01f;

                    if (functionIdx == 0)
                    {
                        float[] parray = { 0.30f, 0.25f, 0.3f, 0.15f };
                        functionIdx = WeightedRandomNumber(parray);
                        //Debug.Log(functionIdx);
                    }
                    else if (functionIdx == 1)
                    {
                        float[] parray2 = { 0.30f, 0.20f, 0.3f, 0.2f };
                        functionIdx = WeightedRandomNumber(parray2);
                        functionIdx = 0;
                        //Debug.Log(functionIdx);
                    }
                    else if (functionIdx == 2)
                    {
                        float[] parray3 = { 0.3f, 0.35f, 0.0f, 0.35f};
                        functionIdx = WeightedRandomNumber(parray3);
                        //Debug.Log(functionIdx);
                    }
                    else
                    {
                        float[] parray4 = { 0.3f, 0.3f, 0.35f, 0.15f };
                        functionIdx = WeightedRandomNumber(parray4);
                    }
					
					Debug.Log(functionIdx);
                    GameObject obj = delegates[functionIdx](gPosition, 120);
                    q.Enqueue(obj);
                    if (q.Count == queueSize + 1)
                    { 
                        Destroy(q.Dequeue());
                    }
            }
        }
    }
}

