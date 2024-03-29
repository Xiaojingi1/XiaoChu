using System.Collections;
using System.Collections.Generic;
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
}
