using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSpawn : BaseMessage
{
    public GameObject character;
}
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject characterToSpawn;
    [SerializeField] Transform spawnLocation;
    PlayerCameraSpawn characterCamera = new PlayerCameraSpawn();

    public void Start()
    {
        characterCamera.character = characterToSpawn;
        SpawnCharacterIn();
    }

    public void SpawnCharacterIn()
    {
        Instantiate(characterToSpawn, spawnLocation);
        MessageBuffer<PlayerCameraSpawn>.Dispatch(characterCamera);
    }
}
