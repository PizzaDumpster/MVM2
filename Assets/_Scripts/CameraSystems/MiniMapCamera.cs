using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    Camera cam;
    Vector3 offset;
    GameObject player;


    void Start()
    {
        cam = GetComponent<Camera>();
        offset.z = -25; 
    }

    void Update()
    {
        if (player == null)
            player = GameObject.Find("Player(Clone)");

        if (player != null && player.GetComponent<PlayerHealth>() != null)
        {
            if (player.GetComponent<PlayerHealth>().HealthAmount <= 0)
                player = null;
        }

        if (player != null)
            cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, offset.z);
    }

}
