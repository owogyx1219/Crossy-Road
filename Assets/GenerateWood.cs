using UnityEngine;
using System.Collections;

public class GenerateWood : MonoBehaviour {

    public static int decCount = 5, grassCount = 1, treeCount = 6;
    public static float grassWidth = 10.0f, grassYPos = 0.3f;
    public static float decProb = 0.35f, treeProb = 0.5f;
    public static float decPosOffset = 0.4f, treePosOffset = 1.25f, treePosStep = 2.0f;
    public static Object[] trees, grasses, decs;
    public static bool moveLeft = true;

    public static void setUpWood()
    {
        decs = new Object[decCount];

        for (int i = 0; i < decCount; i++)
        {
            string path = "TreeAssets/dec_" + i;
            Object dec = Resources.Load(path) as GameObject;
            decs[i] = dec;
        }

        grasses = new Object[grassCount];
        for (int i = 0; i < grassCount; i++)
        {
            string path = "TreeAssets/grass_" + i;
            Object water = Resources.Load(path) as GameObject;
            grasses[i] = water;
        }

        trees = new Object[treeCount];
        for (int i = 0; i < treeCount; i++)
        {
            string path = "TreeAssets/tree_" + i;

            Object tree = Resources.Load(path) as GameObject;
            trees[i] = tree;
        }

    }


    public static void GenerateWoodBox(Vector3 pos, Transform woodParent)
    {
        Debug.Log("Entered GenerateWoodBox");
        Object grass = grasses[(int)(Mathf.Floor(Random.Range(0.0f, grassCount - 0.1f)))];
        GameObject actualGrass = Instantiate(grass, woodParent, true) as GameObject;
        actualGrass.transform.position = new Vector3(pos[0], pos[1], pos[2]);
        pos[1] = pos[1] + grassYPos;
        if (Random.value <= decProb)
        {
            Object dec = decs[(int)(Mathf.Floor(Random.Range(0.0f, decCount - 0.1f)))];
            GameObject actualDec = Instantiate(dec, woodParent, true) as GameObject;
            float xOffset = Random.Range(-decPosOffset, decPosOffset), zOffset = Random.Range(-decPosOffset, decPosOffset);
            actualDec.transform.position = new Vector3(pos[0] + xOffset, pos[1] + actualDec.transform.position[1], pos[2] + zOffset);
        }
        else
        {

            for (float x = -treePosOffset; x <= treePosOffset; x += treePosStep)
            {
                for (float z = -treePosOffset; z <= treePosOffset; z += treePosStep)
                {
                    if (Random.value <= treeProb)
                    {
                        Object tree = trees[(int)(Mathf.Floor(Random.Range(0.0f, treeCount - 0.1f)))];
                        GameObject actualTree = Instantiate(tree, woodParent, true) as GameObject;
                        actualTree.transform.position = new Vector3(pos[0] + x, pos[1] + actualTree.transform.position[1], pos[2] + z);
                    }
                }
            }
        }
        
    }

    public static GameObject GenerateWoods(Vector3 pos, int width)
    {
        setUpWood();
        GameObject woodParent = new GameObject("woodParent");

        Debug.Log("Entered GenerateWoods");

        for (float z = (width * (-1)); z < width; z += grassWidth)
        {
            GenerateWoodBox(new Vector3(pos[0], pos[1], z + pos[2]), woodParent.transform);
        }
        return woodParent;
    }
}
