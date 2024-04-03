using System;
using System.Collections;
using System.Collections.Generic;
using TaoTie;
using Unity.VisualScripting;
using UnityEngine;
using YooAsset;

public class NewMapsScript : MonoBehaviour
{
    public Transform[] maps;
    public GameObject prepareFoodArea;
    GameObject material, clickedObject;
    Vector3 materialV3 = Vector3.zero;
    int currentCevelLayer;
    Ray ray;
    RaycastHit hit;
    PrepareFoodAreaScript prepareFoodAreaScript;
    SpriteRenderer icon;

    MaterialScript currentMaterial;

    // Start is called before the first frame update
    async void Awake()
    {
        Play.Instance.PlayInit();
        await GameObjectPoolManager.Instance.PreLoadGameObjectAsync("Game/Maps/Prefabs/Material.prefab", 512);
        prepareFoodAreaScript = prepareFoodArea.GetComponent<PrepareFoodAreaScript>();
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

        Play.Instance.wholeMapList.Add(ints);

        LevelCreate(ints, 8);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("Material"))
                {
                    hit.transform.GetComponent<MaterialScript>().OnClick();
                }
            }

        }
    }

    public async void MapCreate(int[,] mapList, Transform parent, int currentLayer)
    {
        int x = 0;
        int y = 0;
        int spriteLayer = currentLayer * 25 + 10;

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
                    materialV3.y = 0.36f - 0.12f * j + currentLayer * -0.04f - y * 0.01f;
                    material.transform.localPosition = materialV3;
                    material.GetComponent<SpriteRenderer>().sortingOrder = spriteLayer + j * 2;
                    icon = material.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    icon.sortingOrder = spriteLayer + j * 2 + 1;

                    currentMaterial = material.GetComponent<MaterialScript>();
                    currentMaterial.prepareFoodAreaScript = this.prepareFoodAreaScript;
                    currentMaterial.shadow.GetComponent<SpriteRenderer>().sortingOrder = spriteLayer + j * 2 + 2;

                    currentMaterial.index = UnityEngine.Random.Range(0, 11);
                    icon.sprite = ResourcesManager.Instance.Load<Sprite>("Sprites/food/png/" + IngredientCategory.Instance.Get(currentMaterial.index).IconName + ".png");
                    icon.transform.localScale = 0.2f * Vector3.one;


                    currentMaterial.x = x;
                    currentMaterial.y = y;

                    Play.Instance.mapMaterialList[currentLayer][i, j] = currentMaterial;
                    if (currentLayer != 0)
                    {

                        if (currentLayer < 4)
                        {
                            material.GetComponent<MaterialScript>().AddAffectedAffected(Play.Instance.mapMaterialList[currentLayer - 1][i, j]);
                        }
                        else
                        {
                            for (int a = 0; a < 2; a++)
                            {
                                for (int b = 0; b < 2; b++)
                                {
                                    if (Play.Instance.mapMaterialList[currentLayer - 1][i + a, j + b] != null)
                                    {
                                        material.GetComponent<MaterialScript>().AddAffectedAffected(Play.Instance.mapMaterialList[currentLayer - 1][i + a, j + b]);
                                    }

                                }
                            }
                        }
                    }

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
            MaterialScript[,] materials = new MaterialScript[currendMapList.GetLength(0), currendMapList.GetLength(1)];
            Play.Instance.mapMaterialList.Add(materials);
            Play.Instance.wholeMapList.Add(currendMapList);
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
