using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;
    TMP_Text gameOverText;
    [SerializeField] private GameObject grass;
    [SerializeField] private GameObject road;

    [SerializeField] private int extent;
    [SerializeField] private int frontDistance;

    [SerializeField] private int backDistance = -5;
    [SerializeField] private int MaxSameTerrainRepeat = 3;

    //private int maxZpos;

    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);


    private void Start()
    {
        gameOverPanel.SetActive(false);
        gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();

        //belakang
        for (int z = backDistance+1; z <= 0; z++)
        {
            CreateTerrain(grass, z);
        }

        //depan
        for (int z = 1; z <= frontDistance; z++)
        {
            var prefabs = GetNextRandomTerrainPrefabs(z);

            //instantiate block
            CreateTerrain(prefabs, z);
        }
        player.SetUp(backDistance, extent);
    }

    private int playerLastMaxTravel;
    private void Update()
    {
        if (player.isDie && gameOverPanel.activeInHierarchy==false)
        {
            ShowGameOverPanel();
        }

        //infinite terrain system 
        if(player.MaxTravel == playerLastMaxTravel)
        {
          return;
        }

        playerLastMaxTravel = player.MaxTravel;

        //bikin ke depan
        var randTbPrefab = GetNextRandomTerrainPrefabs(player.MaxTravel + frontDistance);
        CreateTerrain(randTbPrefab, player.MaxTravel + frontDistance);
        //hapus belakang
        var lastTB = map[player.MaxTravel + backDistance];

        map.Remove(player.MaxTravel - 1 + backDistance);
        Destroy(lastTB.gameObject);

        player.SetUp(player.MaxTravel + backDistance,extent);
    }

    public void ShowGameOverPanel()
    {
        gameOverText.text = "HIGHSCORE : " + player.MaxTravel;
        gameOverPanel.SetActive(true);
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
