using System.Collections;
using System.Collections.Generic;
using TaoTie;
using UnityEngine;

public class MaterialScript : MonoBehaviour
{
    public List<MaterialScript> BeAffectedMaterials;//����Щ����Ӱ��
    public List<MaterialScript> AffectedMaterials;//Ӱ����Щ����
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

    //�Ƴ�Ӱ���Լ��ķ���
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

    //���Ӱ���Լ��ķ���
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

    //����Լ�Ӱ��ķ���
    public void AddAffectedAffected(MaterialScript beAffected)
    {
        AffectedMaterials.Add(beAffected);
        beAffected.AddBeAffected(this);
    }

    //��������󴥷�����
    public void OnClick()
    {
        for (int i = 0; i < AffectedMaterials.Count; i++)
        {
            AffectedMaterials[i].RemoveBeAffected(this);
        }

        GameObjectPoolManager.Instance.RecycleGameObject(gameObject);
    }
}
