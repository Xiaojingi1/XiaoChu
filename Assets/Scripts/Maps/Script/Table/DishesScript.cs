using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaoTie;
using UnityEngine;

public class DishesScript : MonoBehaviour
{
    public GameObject[] Material;
    public GameObject current;
    public CookBook cookBook;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    public void Reveal(CookBook cookBook)
    {
        this.cookBook = cookBook;

        GetCurrent();

        for (int i = 0; i < current.transform.childCount; i++)
        {
            current.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = ResourcesManager.Instance.Load<Sprite>("Sprites/food/png/" + IngredientCategory.Instance.Get(cookBook.Dishess[i]).IconName + ".png");

            cookBook.DishesBL[i] = true;
        }
    }

    public void Refresh(CookBook cookBook)
    {
        this.cookBook = cookBook;

        GetCurrent();

        for (int i = 0; i < current.transform.childCount; i++)
        {
            current.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = ResourcesManager.Instance.Load<Sprite>("Sprites/food/png/" + IngredientCategory.Instance.Get(cookBook.Dishess[i]).IconName + ".png");

            if (cookBook.DishesBL[i])
            {
                current.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                current.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }
        }
    }


    public bool DishesAchieve(int index, out bool bl)
    {
        bl = true;
        for (int i = 0; i < cookBook.Dishess.Length; i++)
        {
            if (cookBook.Dishess[i] == index && cookBook.DishesBL[i])
            {
                current.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                cookBook.DishesBL[i] = false;
                bl = false;
                if (!cookBook.DishesBL.Contains(true))
                {
                    return true;
                }

                return false;
            }

        }

        return false;
    }

    void GetCurrent()
    {
        if (current != null)
        {
            current.SetActive(false);
        }

        current = Material[cookBook.Dishess.Length - 1];
        current.SetActive(true);
    }
}
