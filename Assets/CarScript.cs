using UnityEngine;
using System.Collections;

public class CarScript : MonoBehaviour {

    public bool moveLeft;
    public float forwardSpeed;

    void Start()
    {
        Debug.Log("Attached Car Script");
        if (moveLeft == false)
            forwardSpeed = forwardSpeed * (-1);
    }

	// Update is called once per frame
	void Update () {

        if (75f <= transform.position.z && transform.position.z <= 425f)
        {
            transform.Translate(0, 0, forwardSpeed, Space.World);

        } else
        {
            if (moveLeft)
            {
                Vector3 p = transform.position;
                transform.position = new Vector3(p[0], p[1], 75f);
            } else
            {
                Vector3 p = transform.position;
                transform.position = new Vector3(p[0], p[1], 425f);
            }
        }
    }
}
