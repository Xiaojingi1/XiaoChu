using System.Collections.Generic;

namespace TaoTie
{
    public class GameStartScene : IScene
    {
        private UIGameStartView win;
        private string[] dontDestroyWindow = { "UIGameStartView" };
        public List<string> scenesChangeIgnoreClean;
        public string[] GetDontDestroyWindow()
        {
            return dontDestroyWindow;
        }

        public List<string> GetScenesChangeIgnoreClean()
        {
            return scenesChangeIgnoreClean;
        }
        public async ETTask OnCreate()
        {
            scenesChangeIgnoreClean = new List<string>();
            scenesChangeIgnoreClean.Add(UIGameStartView.PrefabPath);
            await ETTask.CompletedTask;
        }

        public async ETTask OnEnter()
        {
           // win = await UIManager.Instance.OpenWindow<UIGameStartView>(UIGameStartView.PrefabPath);
            //win.SetProgress(0);
        }

        public async ETTask OnLeave()
        {
            await ETTask.CompletedTask;
        }

        public async ETTask OnPrepare()
        {
            await ETTask.CompletedTask;
        }

        public async ETTask OnComplete()
        {
            await ETTask.CompletedTask;
        }

        public async ETTask SetProgress(float value)
        {
            //win.SetProgress(value);
            await ETTask.CompletedTask;
        }

        public string GetScenePath()
        {
            return "Scenes/GameStartScene/GameStart.unity";
        }

        public async ETTask OnSwitchSceneEnd()
        {
            //await UIManager.Instance.OpenWindow<UIMainView>(UIMainView.PrefabPath);
            await UIManager.Instance.OpenWindow<UIGameStartView>(UIGameStartView.PrefabPath);
            win = null;
        }
    }
}