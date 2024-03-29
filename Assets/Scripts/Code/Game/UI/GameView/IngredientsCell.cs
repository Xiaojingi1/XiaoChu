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
        private List<IngredientsCell> affected;//该格影响的其他格子（被影响格子无法使用
        private List<IngredientsCell> beAffected;//影响该格的格子（长不为0时无法使用


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
