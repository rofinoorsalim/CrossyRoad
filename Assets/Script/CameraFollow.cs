using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject Player;

    [SerializeField] Vector3 offset;

    public void Start()
    {
        offset = this.transform.position - Player.transform.position;
    }

    Vector3 lastAnimalPos;

    private void Update()
    {
        if (lastAnimalPos == Player.transform.position)
        {
            return;
        }
        var targetPlayerPos = new Vector3(Player.transform.position.x, 0, Player.transform.position.z);

        transform.position = targetPlayerPos + offset;

        lastAnimalPos = Player.transform.position;
    }
}
