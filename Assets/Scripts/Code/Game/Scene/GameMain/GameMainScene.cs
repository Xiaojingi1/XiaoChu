using System.Collections.Generic;

namespace TaoTie
{
    public class GameMainScene : IScene
    {
        private UILoadingView win;
        private string[] dontDestroyWindow = { "UILoadingView" };
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
            scenesChangeIgnoreClean.Add(UILoadingView.PrefabPath);
            await ETTask.CompletedTask;
        }

        public async ETTask OnEnter()
        {
            win = await UIManager.Instance.OpenWindow<UILoadingView>(UILoadingView.PrefabPath);
           
            win.SetProgress(0);
        }

        public async ETTask OnLeave()
        {
            await ETTask.CompletedTask;
        }

        public async ETTask OnPrepare()
        {
            //await UIManager.Instance.OpenWindow<GameView>(GameView.PrefabPath,UILayerNames.BackgroudLayer);;
            //await ETTask.CompletedTask;
        }

        public async ETTask OnComplete()
        {
            await ETTask.CompletedTask;
        }

        public async ETTask SetProgress(float value)
        {
            win.SetProgress(value);
            
            await ETTask.CompletedTask;
        }

        public string GetScenePath()
        {
            return "Scenes/GameMainScene/GameMain.unity";
        }

        public async ETTask OnSwitchSceneEnd()
        {
            //await UIManager.Instance.OpenWindow<UIMainView>(UIMainView.PrefabPath);
            await UIManager.Instance.CloseWindow<UILoadingView>();
            
            //await UIManager.Instance.OpenWindow<UIGameStartView>(UIGameStartView.PrefabPath);
            //win = null;
        }
    }
}