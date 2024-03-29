using System.Collections;
using System.Collections.Generic;
using TaoTie;
using UnityEngine;

public class MaterialScript : MonoBehaviour
{
    public List<MaterialScript> BeAffectedMaterials;//被那些方格影响
    public List<MaterialScript> AffectedMaterials;//影响那些方格
    public GameObject shadow;


    private void OnEnable()
    {
        MaterialInit();
    }

    private void MaterialInit()
    {
        BeAffectedMaterials = new List<MaterialScript>();
        AffectedMaterials = new List<MaterialScript>();

    }

    //移除影响自己的方格
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

    //添加影响自己的方格
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

    //添加自己影响的方格
    public void AddAffectedAffected(MaterialScript beAffected)
    {
        AffectedMaterials.Add(beAffected);
        beAffected.AddBeAffected(this);
    }

    //自身被点击后触发方法
    public void OnClick()
    {
        for (int i = 0; i < AffectedMaterials.Count; i++)
        {
            AffectedMaterials[i].RemoveBeAffected(this);
        }

        GameObjectPoolManager.Instance.RecycleGameObject(gameObject);
    }
}
