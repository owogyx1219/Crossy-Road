using UnityEngine;
using System.Collections;

public class GenerateRiver : MonoBehaviour {

    public static int boatCount = 1, waterCount = 1, bridgeCount = 2;
    public static float boatXOffset = -0.75f;
    public static float waterWidth = 10.0f;
    public static float boatProb = 0.25f, bridgeProb = 0.1f;
    public static Object[] boats, waters, bridges;
    public static bool moveLeft = true;


    public static void setUpWater()
    {
        boats = new Object[boatCount];
        for (int i = 0; i < boatCount; i++)
        {
            string path = "RiverAssets/boat_" + i;

            Object boat = Resources.Load(path) as GameObject;
            boats[i] = boat;
        }

        waters = new Object[waterCount];
        for (int i = 0; i < waterCount; i++)
        {

            string path = "RiverAssets/water_" + i;

            Object water = Resources.Load(path) as GameObject;

            waters[i] = water;
        }

        bridges = new Object[bridgeCount];
        for (int i = 0; i < bridgeCount; i++)
        {
            string path = "RiverAssets/bridge_" + i;

            Object bridge = Resources.Load(path) as GameObject;

            bridges[i] = bridge;
        }
    }


    public static void GenerateRiverBox(Vector3 pos, Transform riverParent, float speed, bool moveLeft)
    {
        Debug.Log("Entered GenerateWater");
        Object water = waters[(int)(Mathf.Floor(Random.Range(0.0f, waterCount - 0.1f)))];
        GameObject actualWater = Instantiate(water, riverParent, true) as GameObject;
        actualWater.transform.position = new Vector3(pos[0], pos[1], pos[2]);

        if (Random.value <= boatProb)
        {
            Object boat = boats[(int)(Mathf.Floor(Random.Range(0.0f, boatCount - 0.1f)))];
            GameObject actualBoat = Instantiate(boat, riverParent, true) as GameObject;
            actualBoat.transform.position = new Vector3(pos[0] + boatXOffset, pos[1] + actualBoat.transform.position[1], pos[2]);
            CarScript script = actualBoat.AddComponent<CarScript>();
            script.moveLeft = moveLeft;
            script.forwardSpeed = speed;
        }

        if (Random.value <= bridgeProb)
        {
            Object bridge = bridges[(int)(Mathf.Floor(Random.Range(0.0f, bridgeCount - 0.1f)))];
            GameObject actualBridge = Instantiate(bridge, riverParent, true) as GameObject;
            actualBridge.transform.position = new Vector3(pos[0], pos[1] + +actualBridge.transform.position[1], pos[2]);
        }
    }

    public static GameObject GenerateRivers(Vector3 pos, int width)
    {
        setUpWater();
        GameObject riverParent = new GameObject("riverParent");

        Debug.Log("Entered GenerateWaters");
        if (Random.value <= 0.5f)
        {
            moveLeft = false;
        }
        else
        {
            moveLeft = true;
        }

        float speed = Random.Range(0.025f, 0.05f);
        for (float z = (width * (-1)); z < width; z += waterWidth)
        {
            GenerateRiverBox(new Vector3(pos[0], pos[1], z + pos[2]), riverParent.transform, speed, moveLeft);
        }
        return riverParent;
    }
}
