using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject carPrefabs;
    [SerializeField] TerrainBlock terrain;

    bool isRight;

    float timer = 3;

    private void Start()
    {
        isRight = Random.value > 0.5f ? true : false;
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        timer = 3;
        var spawnPos = this.transform.position + Vector3.right * (isRight ? -(terrain.Extent + 1) : terrain.Extent + 1);
        var go = Instantiate(
            original : carPrefabs, 
            position : spawnPos, 
            rotation : Quaternion.Euler(0, isRight ? 90 : -90, 0),
            this.transform);
        var car = go.GetComponent<Car>();
        car.setUp(terrain.Extent);
    }
}
