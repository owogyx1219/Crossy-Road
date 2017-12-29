using UnityEngine;
using System.Collections;

public class car_move_left : MonoBehaviour {

    float forwardstep = -0.5f;
    void Update()
    {
        if (transform.position.z >= 500.0f)
        {
            Debug.Log("inside reset function");
            Vector3 curr_position = transform.position;
            curr_position.z = curr_position.z - 500.0f;
            transform.position = curr_position;
        }

        if (transform.position.z >= 0.0f)
        {
            transform.Translate(forwardstep, 0, 0);
        }
    }
}
