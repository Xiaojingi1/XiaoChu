using System.Collections;
using System.Collections.Generic;
using TaoTie;
using UnityEngine;

public class Play
{
    //µ¥Àý
    private static Play instance = null;
    private static object obj = new object();
    private Play() { }
    public static Play Instance
    {
        get
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new Play();
                    }
                }
            }
            return instance;
        }
    }

    public int Days;
    public int Money;

    public List<int[,]> wholeMapList;
    public List<MaterialScript[,]> mapMaterialList;
    public List<int> PrepareFood;
    public List<CookBook> DishesList;

    public void PlayInit()
    {
        Days = 0;
        Money = 0;
        wholeMapList = new List<int[,]>();
        mapMaterialList = new List<MaterialScript[,]>();
        PrepareFood = new List<int>();
        DishesList = new List<CookBook>();
    }
}
