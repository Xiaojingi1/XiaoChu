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
            DishessAdd(UnityEngine.Random.Range(0, 10));
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
        ck.DishesBL = new bool[Dishess.Length];
        Play.Instance.DishesList.Add(ck);
        DishessReveal(Play.Instance.DishesList.Count - 1);
    }

    public void DishessRemove(CookBook book)
    {
        Play.Instance.DishesList.Remove(book);
        DishessRefresh();
    }

    public void DishessRefresh()
    {
        for (int i = 0; i < Dishess.Length; i++)
        {
            if (i < Play.Instance.DishesList.Count && Dishess[i].current != null)
            {
                Dishess[i].Refresh(Play.Instance.DishesList[i]);
            }
            else
            {
                Dishess[i].current.SetActive(false);
            }

        }
    }

    public void DishessReveal(int index)
    {
        Dishess[index].Reveal(Play.Instance.DishesList[index]);
    }

    public void Achieve(int index)
    {
        bool bl;
        for (int i = 0; i < Play.Instance.DishesList.Count; i++)
        {
            for (int j = 0; j < Play.Instance.DishesList[i].Dishess.Length; j++)
            {
                if (Play.Instance.DishesList[i].Dishess[j] == index)
                {
                    if (Dishess[i].DishesAchieve(index, out bl))
                    {
                        DishessRemove(Dishess[i].cookBook);

                    }


                    if (!bl)
                    {
                        return;
                    }

                }
            }

        }
    }
}
