using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowZCharacter : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private float cameraZ;
    private float cameraX;

    public void Initialize()
    {
        cameraZ = transform.position.z;
        cameraX = transform.position.x;
    }

    private void Update()
    {
        transform.position = new Vector3(cameraX  + transform.position.x, transform.position.y, cameraZ + playerTransform.position.z);
    }
}
