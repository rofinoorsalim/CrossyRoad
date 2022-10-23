using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject road;

    [SerializeField] private int extent;
    [SerializeField] private int frontDistance = 10;

    [SerializeField] private int minZPos = -5;
    [SerializeField] private int MaxSameTerrainRepeat = 3;

    //private int maxZpos;

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);


    private void Start()
    {
        //belakang
        for (int z = minZPos+1; z <= 0; z++)
        {
            CreateTerrain(grass, z);
        }

        //depan
        for (int z = 1; z < frontDistance; z++)
        {
            var prefabs = GetNextRandomTerrainPrefabs(z);

            //instantiate block
            CreateTerrain(prefabs, z);
        }
        player.SetUp(minZPos, extent);
    }



    private void CreateTerrain(GameObject prefabs, int zPos)
    {
        var go = Instantiate(prefabs, new Vector3(0, 0, zPos), Quaternion.identity);
        var tb = go.GetComponent<TerrainBlock>();
        tb.build(extent);
        map.Add(zPos, tb);
    }

    private GameObject GetNextRandomTerrainPrefabs(int nextPos)
    {
        bool isUniform = true;
        var tbRef = map[nextPos - 1];
        for (int i = 2; i <= MaxSameTerrainRepeat; i++)
        {
            if (map[nextPos - i].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }

        if (isUniform)
        {
           if(tbRef is Grass)
            {
                return road; 
            }
            else
            {
                return grass;
            }
        }

        //penentuan terrain dengan probabilitas 50%
        return Random.value > 0.5f ? road : grass;
    }
}
