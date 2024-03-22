using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace TaoTie
{
	public class UIGameStartView : UIBaseView, IOnCreate, IOnEnable
	{
		public static string PrefabPath => "UI/UIGameStart/Prefabs/UIGameStartView.prefab";
		public UIEmptyView Top;
		public UIEmptyView Middle;
		public UIButton StartBtn;
		public UIEmptyView Down;
		

		#region override
		public void OnCreate()
		{
			this.Top = this.AddComponent<UIEmptyView>("Top");
			this.Middle = this.AddComponent<UIEmptyView>("Middle");
			this.StartBtn = this.AddComponent<UIButton>("Middle/StartBtn");
			this.Down = this.AddComponent<UIEmptyView>("Down");
		}
		public void OnEnable()
		{
			this.StartBtn.SetOnClick(OnClickStartBtn);
		}
		#endregion

		#region 事件绑定
		public async void OnClickStartBtn()
		{
            await SceneManager.Instance.SwitchScene<GameMainScene>();
        }
		#endregion
	}
}
