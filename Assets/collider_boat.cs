using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_boat : MonoBehaviour {

    private static GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("avatar");
    }

    // Update is called once per frame
    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.CompareTag("avatar"))
        {
            player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z); 
        }
    }
}
