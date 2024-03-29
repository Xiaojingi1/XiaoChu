using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;

namespace TaoTie
{
    public class IngredientsCell : UIBaseContainer, IOnCreate
    {
        private UIImage image;
        string path = "Sprites/food/png/";
        private List<IngredientsCell> affected;//�ø�Ӱ����������ӣ���Ӱ������޷�ʹ��
        private List<IngredientsCell> beAffected;//Ӱ��ø�ĸ��ӣ�����Ϊ0ʱ�޷�ʹ��


        public void OnCreate()
        {
            affected = new List<IngredientsCell>();
            beAffected = new List<IngredientsCell>();
            image = AddComponent<UIImage>("Icon");
        }


        public async void SetData(int id)
        {
            path += IngredientCategory.Instance.Get(id).IconName + ".png";
            await image.SetSpritePath(path);

        }
    }
}
