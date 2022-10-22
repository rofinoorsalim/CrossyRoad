using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowZCharacter : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private float cameraZ;

    public void Initialize()
    {
        cameraZ = transform.position.z; 
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, cameraZ + playerTransform.position.z);
    }
}
