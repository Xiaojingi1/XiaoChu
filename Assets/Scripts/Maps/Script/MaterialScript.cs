using System.Collections;
using System.Collections.Generic;
using TaoTie;
using UnityEngine;

public class MaterialScript : MonoBehaviour
{
    public List<MaterialScript> BeAffectedMaterials = new List<MaterialScript>();//被那些方格影响
    public List<MaterialScript> AffectedMaterials = new List<MaterialScript>();//影响那些方格
    public GameObject shadow;
    public GameObject prepareFoodArea;
    public int x, y;
    public int index;

    public PrepareFoodAreaScript prepareFoodAreaScript;


    /// <summary>
    ///  移除影响自己的方格
    /// </summary>
    /// <param name="beAffected"></param>

    public void RemoveBeAffected(MaterialScript beAffected)
    {
        BeAffectedMaterials.Remove(beAffected);

        //没有影响自己的方格就能被点击
        if (BeAffectedMaterials.Count == 0)
        {
            //设置Tag能被点击
            transform.tag = "Material";
            //关闭阴影效果
            shadow.SetActive(false);
        }
    }

    /// <summary>
    ///  添加影响自己的方格
    /// </summary>
    /// <param name="beAffected"></param>

    public void AddBeAffected(MaterialScript beAffected)
    {
        BeAffectedMaterials.Add(beAffected);
        if (!shadow.activeSelf)
        {
            //只要有被影响的就不能被点击
            shadow.SetActive(true);
            transform.tag = "NotMaterial";
        }

    }

    /// <summary>
    /// 添加自己影响的方格
    /// </summary>
    /// <param name="beAffected"></param>
    public void AddAffectedAffected(MaterialScript beAffected)
    {
        AffectedMaterials.Add(beAffected);
        beAffected.AddBeAffected(this);
    }

    /// <summary>
    /// 自身被点击后触发方法
    /// </summary>
    public void OnClick()
    {
        if (Play.Instance.PrepareFood.Count < 9)
        {
            for (int i = 0; i < AffectedMaterials.Count; i++)
            {
                AffectedMaterials[i].RemoveBeAffected(this);
            }

            prepareFoodAreaScript.AddMaterial(index);
            GameObjectPoolManager.Instance.RecycleGameObject(gameObject);
        }
    }
}
