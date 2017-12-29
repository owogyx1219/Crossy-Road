using UnityEngine;
using System.Collections;

public class GenerateLane : MonoBehaviour {

    public static int carCount = 11, laneCount = 1, decCount = 2;
    public static float laneYPos = 1.0f, decXOffset = 5.0f, laneWidth = 10.0f;
    public static float carProb = 0.25f, decProb = 0.01f;
    public static float carXOffset = 2.35f;
    public static Object[] carrs, carls, lanes, decs;
    public static bool moveLeft = true;

    public static void setUpLane()
    {
        carrs = new Object[carCount];
        for (int i = 0; i < carCount; i++)
        {
            string path = "LaneAssets/carr_" + i;

            Object car = Resources.Load(path) as GameObject;

            carrs[i] = car;
        }

        carls = new Object[carCount];
        for (int i = 0; i < carCount; i++)
        {
            string path = "LaneAssets/carl_" + i;

            Object car = Resources.Load(path) as GameObject;

            carls[i] = car;
        }

        lanes = new Object[laneCount];
        for (int i = 0; i < laneCount; i++)
        {
            string path = "LaneAssets/lane_" + i;

            Object lanei = Resources.Load(path) as GameObject;

            lanes[i] = lanei;
        }

        decs = new Object[decCount];
        for (int i = 0; i < decCount; i++)
        {
            string path = "LaneAssets/dec_" + i;

            Object dec = Resources.Load(path) as GameObject;

            decs[i] = dec;
        }
    }


    public static void GenerateLaneBox(Vector3 pos, Transform laneParent, float speed)
    {
        
        Debug.Log("Entered GenerateLane");
        Object lane = lanes[(int)(Mathf.Floor(Random.Range(0.0f, laneCount - 0.1f)))];
        GameObject actualLane = Instantiate(lane, laneParent, true) as GameObject;
        pos[1] += actualLane.transform.position[1];
        actualLane.transform.position = new Vector3(pos[0], pos[1], pos[2]);

        if (Random.value <= carProb)
        {
            if (moveLeft)
            {
                Object car = carls[(int)(Mathf.Floor(Random.Range(0.0f, carCount - 0.1f)))];
                GameObject actualCar = Instantiate(car, laneParent, true) as GameObject;
                actualCar.transform.position = new Vector3(pos[0], pos[1] + actualCar.transform.position[1], pos[2]);
                CarScript script = actualCar.AddComponent<CarScript>();
                script.moveLeft = true;
                script.forwardSpeed = speed;
                GameEnd endScript = actualCar.AddComponent<GameEnd>();
                endScript.dead_reason = "You have been hit by a car";
            }
            else
            {
                Object car = carrs[(int)(Mathf.Floor(Random.Range(0.0f, carCount - 0.1f)))];
                GameObject actualCar = Instantiate(car, laneParent, true) as GameObject;
                actualCar.transform.position = new Vector3(pos[0], pos[1] + actualCar.transform.position[1], pos[2]);
                CarScript script = actualCar.AddComponent<CarScript>();
                script.moveLeft = false;
                script.forwardSpeed = speed;
                GameEnd endScript = actualCar.AddComponent<GameEnd>();
                endScript.dead_reason = "You have been hit by a car";
            }
        }

        if (Random.value <= decProb)
        {
            Object dec = decs[(int)(Mathf.Floor(Random.Range(0.0f, decCount - 0.1f)))];
            float xOffset = decXOffset;
            if (Random.value <= 0.5f)
            {
                xOffset = xOffset * (-1);
            }
            GameObject actualDec = Instantiate(dec, laneParent, true) as GameObject;
            actualDec.transform.position = new Vector3(pos[0] + xOffset, pos[1], pos[2]);
        }
    }

    public static GameObject GenerateLanes(Vector3 pos, int width)
    {
        setUpLane();
        GameObject laneParent = new GameObject("laneParent");

        Debug.Log("Entered GenerateLanes");
        if (Random.value <= 0.5f)
        {
            moveLeft = false;
            //laneParent.transform.Rotate(0, 180, 0);
        } else
        {
            moveLeft = true;
        }

        float speed = Random.Range(0.1f, 0.25f);
        for (float z=(width * (-1)); z<width; z+=laneWidth)
        {
            GenerateLaneBox(new Vector3(pos[0], pos[1], z + pos[2]), laneParent.transform, speed);
        }
        return laneParent;
    }
}
