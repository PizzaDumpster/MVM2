using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffectMultiplier;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        //lastCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraPosition.x;
        float deltaY = cameraTransform.position.y - lastCameraPosition.y;

        transform.position += new Vector3(deltaX * parallaxEffectMultiplier, 0, 0);

        lastCameraPosition = cameraTransform.position;
    }
}
