using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    Camera cam;
    Vector3 offset;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        offset.z = -25; 
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
            player = GameObject.Find("Player(Clone)");
        if(player.GetComponent<PlayerHealth>().currentHealth.healthData.currentHealth <= 0)
            player = null; 
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, offset.z);
    }
}
