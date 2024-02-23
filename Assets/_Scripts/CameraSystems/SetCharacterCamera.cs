using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class SetCharacterCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cmVirtualCamera;
    // Start is called before the first frame update
    void Awake()
    {
        cmVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        MessageBuffer<PlayerCameraSpawn>.Subscribe(SetPlayerToCamera);
    }

    private void OnDisable()
    {
        MessageBuffer<PlayerCameraSpawn>.Unsubscribe(SetPlayerToCamera);
    }

    private void SetPlayerToCamera(PlayerCameraSpawn obj)
    {
        print(obj.character.transform);
        cmVirtualCamera.m_Follow = obj.character.transform;
    }
}
