using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TaoTie;
using UnityEngine;

public class DishesScript : MonoBehaviour
{
    public GameObject[] Material;
    GameObject current;
    bool[] materialbBl;
    public CookBook cookBook;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    public void DishesReveal(CookBook cookBook)
    {
        if (current != null)
        {
            current.SetActive(false);
        }

        this.cookBook = cookBook;
        
        current = Material[cookBook.Dishess.Length - 1];
        current.SetActive(true);

        materialbBl = new bool[cookBook.Dishess.Length];


        for (int i = 0; i < current.transform.childCount; i++)
        {
            try
            {
                current.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite = ResourcesManager.Instance.Load<Sprite>("Sprites/food/png/" + IngredientCategory.Instance.Get(cookBook.Dishess[i]).IconName + ".png");
            }
            catch (Exception)
            {

                Log.Debug(cookBook.Dishess.Length + "______");
            }
           
            materialbBl[i] = true;
        }
    }

    public bool DishesAchieve(int index)
    {
        for (int i = 0; i < cookBook.Dishess.Length; i++)
        {
            if (cookBook.Dishess[i] == index)
            {
                current.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                materialbBl[i] = false;
                if (!materialbBl.Contains(true))
                {
                    return true;
                }

                return false;
            }

        }

        return false;
    }

}
