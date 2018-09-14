using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MoleMole
{
    public class RestartContext : BaseContext
    {
        public RestartContext() : base(UIType.Restart)
        {

        }
    }
    public class RestartView : AnimateView
    {
        public override void Start()
        {
            base.Start();
            //GetComponent<Observer>().AddEventHandler("GameOver", OnGameOver);
        }
        void OnGameOver(object sender,System.EventArgs e)
        {
            Singleton<ContextManager>.Instance.Push(new RestartContext());
        }
        public override void OnEnter(BaseContext context)
        {
            base.OnEnter(context);
        }

        public override void OnExit(BaseContext context)
        {
            base.OnExit(context);
        }

        public override void OnPause(BaseContext context)
        {
            _animator.SetTrigger("OnExit");
        }

        public override void OnResume(BaseContext context)
        {
            _animator.SetTrigger("OnEnter");
        }

        public void OptionCallBack()
        {
          
        }
        public void RestartCallBack()
        {
            LoadingScreen.GlobLoadScenes.loadName = "StartGame";
            SceneManager.LoadScene("LoadingScenes");
        }
        public void GameExitCallBack()
        {
            Application.Quit();
        }
    }
}
