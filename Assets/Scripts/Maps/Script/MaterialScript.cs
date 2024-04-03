using System.Collections;
using System.Collections.Generic;
using TaoTie;
using UnityEngine;

public class MaterialScript : MonoBehaviour
{
    public List<MaterialScript> BeAffectedMaterials = new List<MaterialScript>();//����Щ����Ӱ��
    public List<MaterialScript> AffectedMaterials = new List<MaterialScript>();//Ӱ����Щ����
    public GameObject shadow;
    public GameObject prepareFoodArea;
    public int x, y;
    public int index;

    public PrepareFoodAreaScript prepareFoodAreaScript;


    /// <summary>
    ///  �Ƴ�Ӱ���Լ��ķ���
    /// </summary>
    /// <param name="beAffected"></param>

    public void RemoveBeAffected(MaterialScript beAffected)
    {
        BeAffectedMaterials.Remove(beAffected);

        //û��Ӱ���Լ��ķ�����ܱ����
        if (BeAffectedMaterials.Count == 0)
        {
            //����Tag�ܱ����
            transform.tag = "Material";
            //�ر���ӰЧ��
            shadow.SetActive(false);
        }
    }

    /// <summary>
    ///  ���Ӱ���Լ��ķ���
    /// </summary>
    /// <param name="beAffected"></param>

    public void AddBeAffected(MaterialScript beAffected)
    {
        BeAffectedMaterials.Add(beAffected);
        if (!shadow.activeSelf)
        {
            //ֻҪ�б�Ӱ��ľͲ��ܱ����
            shadow.SetActive(true);
            transform.tag = "NotMaterial";
        }

    }

    /// <summary>
    /// ����Լ�Ӱ��ķ���
    /// </summary>
    /// <param name="beAffected"></param>
    public void AddAffectedAffected(MaterialScript beAffected)
    {
        AffectedMaterials.Add(beAffected);
        beAffected.AddBeAffected(this);
    }

    /// <summary>
    /// ��������󴥷�����
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
