using System;
using System.Collections;
using System.Collections.Generic;
using TaoTie;
using UnityEngine;

public class DishesMangeScript : MonoBehaviour
{

    public DishesScript[] Dishess;
    int[] cookbook;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Dishess.Length; i++)
        {
            DishessAdd(UnityEngine.Random.Range(0,10));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DishessAdd(int index)
    {
        cookbook = CookBookCategory.Instance.Get(index).Batching;
        CookBook ck = new CookBook();
        ck.Id = index;
        ck.Dishess = CookBookCategory.Instance.Get(index).Batching;
        Play.Instance.DishesList.Add(ck);
        DishessRefresh();
    }

    public void DishessRemove(CookBook book)
    {
        Play.Instance.DishesList.Remove(book);
        DishessRefresh();
    }

    public void DishessRefresh()
    {
        for(int i = 0;i < Play.Instance.DishesList.Count; i++)
        {
            Dishess[i].DishesReveal(Play.Instance.DishesList[i]);
        }
    }

    public void Achieve(int index)
    {
        for (int i = 0; i < Play.Instance.DishesList.Count; i++)
        {
            for (int j = 0; j < Play.Instance.DishesList[i].Dishess.Length; j++)
            {
                if (Play.Instance.DishesList[i].Dishess[j] == index)
                {
                    if (Dishess[i].DishesAchieve(index))
                    {
                        DishessRemove(Dishess[i].cookBook);
                    }
                    return;
                }
            }

        }
    }
}
