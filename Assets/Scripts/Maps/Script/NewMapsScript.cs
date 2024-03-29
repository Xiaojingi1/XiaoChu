using System.Collections;
using System.Collections.Generic;
using TaoTie;
using UnityEngine;
using YooAsset;

public class NewMapsScript : MonoBehaviour
{
    public Transform[] maps;
    GameObject material, clickedObject;
    Vector3 materialV3 = Vector3.zero;
    int currentCevelLayer;
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    async void Start()
    {
        await GameObjectPoolManager.Instance.PreLoadGameObjectAsync("Game/Maps/Prefabs/Material.prefab", 512);
    }

    private void OnEnable()
    {
        int[,] ints =
        {
            { 1,1,0,1,1,0,1,1},
            { 1,1,1,0,0,1,1,1},
            { 1,1,1,1,1,1,1,1},
            { 1,0,0,1,0,1,0,1},
            { 1,0,1,0,0,0,1,1},
            { 1,1,1,1,1,1,1,1},
            { 1,1,1,0,0,1,1,1},
            { 1,0,0,1,1,0,0,1}
        };
        LevelCreate(ints, 8);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.transform.CompareTag("Material"))
            {
                GameObjectPoolManager.Instance.RecycleGameObject(hit.transform.gameObject);
            }
        }
    }

    public async void MapCreate(int[,] mapList, Transform parent, int currentLayer)
    {
        int x = 0;
        int y = 0;
        int spriteLayer = currentLayer * 16 + 1;

        for (int i = 0; i < mapList.GetLength(0); i++)
        {
            for (int j = 0; j < mapList.GetLength(1); j++)
            {
                if (mapList[i, j] != 0)
                {
                    if (currentLayer > 3)
                    {
                        x = currentLayer - 3;
                    }
                    else
                    {
                        x = 0;
                    }

                    if (currentLayer > 3)
                    {
                        y = currentLayer - 3;
                    }
                    else
                    {
                        y = 0;
                    }

                    material = await GameObjectPoolManager.Instance.GetGameObjectAsync("Game/Maps/Prefabs/Material.prefab");
                    material.transform.parent = parent;
                    material.transform.localScale = 0.1f * Vector3.one;
                    materialV3.x = -0.42f + 0.12f * i + x * 0.06f;
                    materialV3.y = 0.36f - 0.12f * j + currentLayer * 0.04f - y * 0.1f;
                    material.transform.localPosition = materialV3;
                    material.GetComponent<SpriteRenderer>().sortingOrder = spriteLayer + i * 2;
                    material.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = spriteLayer + i * 2 + 1;
                }

            }
        }
    }

    public void LevelCreate(int[,] mapList, int Layer)
    {
        int[,] currendMapList = mapList;
        for (int l = 0; l < Layer; l++)
        {

            if (l > 3)
            {

                currendMapList = MapListCreate(currendMapList);

                MapCreate(currendMapList, maps[l], l);
            }
            else
            {
                MapCreate(currendMapList, maps[l], l);

            }
        }

    }

    public int[,] MapListCreate(int[,] mapList)
    {
        int[,] map = new int[mapList.GetLength(0) - 1, mapList.GetLength(1) - 1];
        float index = Mathf.Ceil(mapList.GetLength(0) / 2f) - 1;
        int m = 0;
        int m1 = 0;

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (i >= index)
                {
                    m = i + 1;
                }
                else
                {
                    m = i;
                }

                if (j >= index)
                {
                    m1 = j + 1;
                }
                else
                {
                    m1 = j;
                }

                map[i, j] = mapList[m, m1];
            }
        }

        return map;
    }

}
