using UnityEngine;
using System.Collections;

public class GenerateNeighboorhood : MonoBehaviour {
    
    public static int houseCount = 8, pavementCount = 1, decCount = 5;
    public static float decXOffset = 3.5f, pavementWidth = 10.0f, pavementYOffset = 0.4f;
    public static float houseProb = 0.25f, decProb = 0.25f;
    public static Object[] houses, carls, pavements, decs;

    public static void setUpNeighboorhood()
    {
        houses = new Object[houseCount];
        for (int i = 0; i < houseCount; i++)
        {
            string path = "NeighboorhoodAssets/house_" + i;

            Object house = Resources.Load(path) as GameObject;

            houses[i] = house;
        }

        pavements = new Object[pavementCount];
        for (int i = 0; i < pavementCount; i++)
        {
            string path = "NeighboorhoodAssets/pavement_" + i;
            Object pavement = Resources.Load(path) as GameObject;
            pavements[i] = pavement;
        }

        decs = new Object[decCount];
        for (int i = 0; i < decCount; i++)
        {
            string path = "NeighboorhoodAssets/dec_" + i;
            Object dec = Resources.Load(path) as GameObject;
            decs[i] = dec;
        }
    }


    public static void GenerateNeighboorhoodBox(Vector3 pos, Transform neighboorhoodParent)
    {
        Debug.Log("Entered GenerateNeighboorhoodBox");
        Object pavement = pavements[(int)(Mathf.Floor(Random.Range(0.0f, pavementCount - 0.1f)))];
        GameObject actualPavement = Instantiate(pavement, neighboorhoodParent, true) as GameObject;
        //actualPavement.AddComponent<Rigidbody>();
        //actualPavement.GetComponent<Rigidbody>().useGravity = false;
        actualPavement.transform.position = new Vector3(pos[0], pos[1], pos[2]);
        pos[1] = pos[1] + pavementYOffset;
        if (Random.value <= houseProb)
        {
            Object house = houses[(int)(Mathf.Floor(Random.Range(0.0f, houseCount - 0.1f)))];
            GameObject actualHouse = Instantiate(house, neighboorhoodParent, true) as GameObject;

            actualHouse.transform.position = new Vector3(pos[0], actualHouse.transform.position[1] + pos[1], pos[2]);
        }

        if (Random.value <= decProb)
        {
            Object dec = decs[(int)(Mathf.Floor(Random.Range(0.0f, decCount - 0.1f)))];
            GameObject actualDec = Instantiate(dec, neighboorhoodParent, true) as GameObject;
            //actualDec.AddComponent<Rigidbody>();
            //actualDec.GetComponent<Rigidbody>().useGravity = false;
            actualDec.transform.position = new Vector3(pos[0] - decXOffset, actualDec.transform.position[1] + pos[1], pos[2]);
        }
    }

    public static GameObject GenerateNeighboorhoods(Vector3 pos, int width)
    {
        setUpNeighboorhood();
        GameObject neighboorhoodParent = new GameObject("neighboorhoodParent");
        //neighboorhoodParent.AddComponent<Rigidbody>();
        //neighboorhoodParent.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        //neighboorhoodParent.GetComponent<Rigidbody>().useGravity = false;

        Debug.Log("Entered GenerateNeighboorhoods");

        for (float z = (width * (-1)); z < width; z += pavementWidth)
        {
            GenerateNeighboorhoodBox(new Vector3(pos[0], pos[1], z + pos[2]), neighboorhoodParent.transform);
        }
        return neighboorhoodParent;
    }
}
