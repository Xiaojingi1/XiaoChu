using System;
using System.Collections;
using System.Collections.Generic;
using TaoTie;
using UnityEngine;

public class PrepareFoodAreaScript : MonoBehaviour
{
    public GameObject[] materialList;
    public DishesMangeScript dishesMange;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        IconCread();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //添加原料到备菜区
    public void AddMaterial(int index)
    {
        int w = 0;
        if (Play.Instance.PrepareFood.Count < 8)
        {
            Play.Instance.PrepareFood.Add(index);

            //判断是否满足消除条件
            for (int i = 0; i < Play.Instance.PrepareFood.Count; i++)
            {
                if (Play.Instance.PrepareFood[i] == index)
                {
                    w++;
                }
            }

            //满足就消除
            if (w > 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    Play.Instance.PrepareFood.Remove(index);
                }

                IconRefresh();
                dishesMange.Achieve(index);
            }
            else
            {
                int k = Play.Instance.PrepareFood.Count - 1;

                IconReveal(k, index);
            }


        }

    }

    private void IconReveal(int i, int index)
    {
        if (!materialList[i].activeSelf)
        {
            materialList[i].SetActive(true);
        }
        
        materialList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = ResourcesManager.Instance.Load<Sprite>("Sprites/food/png/" + IngredientCategory.Instance.Get(index).IconName + ".png");
    }

    //刷新备菜区
    private void IconRefresh()
    {
        for (int i = 0; i < materialList.Length; i++)
        {
            
            if (i < Play.Instance.PrepareFood.Count)
            {
                IconReveal(i, Play.Instance.PrepareFood[i]);
            }
            else
            {
                materialList[i].SetActive(false);
            }

        }
    }

    public void IconCread()
    {
        for (int i = 0; i < materialList.Length; i++)
        {
            materialList[i].SetActive(false);
        }



        for (int i = 0; i < materialList.Length; i++)
        {
            if (Play.Instance.PrepareFood.Count > i)
            {
                AddMaterial(Play.Instance.PrepareFood[i]);
            }
        }
    }
}
