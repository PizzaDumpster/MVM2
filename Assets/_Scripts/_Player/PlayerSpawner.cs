using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : BaseMessage { }
public class PlayerCameraSpawn : BaseMessage
{
    public GameObject character;
}
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject characterToSpawn;
    [SerializeField] Transform spawnLocation;
    PlayerCameraSpawn characterCamera = new PlayerCameraSpawn();

    private GameObject spawnedCharacter;

    public void Start()
    {
        MessageBuffer<PlayerRespawn>.Subscribe(RespawnCharacterIn);
        SpawnCharacterIn();
    }

    private void RespawnCharacterIn(PlayerRespawn obj)
    {
        SpawnCharacterIn();
    }

    public void SpawnCharacterIn()
    {

        spawnedCharacter = Instantiate(characterToSpawn, spawnLocation.position, Quaternion.identity);

        PlayerCameraSpawn message = new PlayerCameraSpawn();
        message.character = spawnedCharacter;
        MessageBuffer<PlayerCameraSpawn>.Dispatch(message);
    }
}
