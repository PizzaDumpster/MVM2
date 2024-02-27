using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckMark : BaseMessage { public Transform checkPoint; }
public class PlayerRespawn : BaseMessage { }
public class PlayerCameraSpawn : BaseMessage
{
    public GameObject character;
}
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject characterPrefab; // Prefab to instantiate
    [SerializeField] Transform spawnLocation; // Location to spawn the character
    PlayerCameraSpawn characterCamera = new PlayerCameraSpawn();

    private GameObject spawnedCharacter; // Reference to the instantiated character

    public void Start()
    {
        MessageBuffer<SetCheckMark>.Subscribe(SetSpawenPoint);
        MessageBuffer<PlayerRespawn>.Subscribe(RespawnCharacterIn);
        SpawnCharacterIn();
    }

    private void SetSpawenPoint(SetCheckMark obj)
    {
        spawnLocation = obj.checkPoint;
    }

    private void RespawnCharacterIn(PlayerRespawn obj)
    {
        SpawnCharacterIn();
    }

    public void SpawnCharacterIn()
    {
        if (spawnedCharacter != null)
        {
            spawnedCharacter.transform.position = spawnLocation.position;
            spawnedCharacter.SetActive(true);
        }
        else
        {
            spawnedCharacter = Instantiate(characterPrefab, spawnLocation.position, Quaternion.identity);
        }

        PlayerCameraSpawn message = new PlayerCameraSpawn();
        message.character = spawnedCharacter;
        MessageBuffer<PlayerCameraSpawn>.Dispatch(message);
    }
}
