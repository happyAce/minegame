﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 

namespace MoleMole
{
    public class MainMenuContext : BaseContext
    {
        public MainMenuContext() : base(UIType.MainMenu)
        {

        }
    }

    public class MainMenuView : AnimateView
    {

        [SerializeField]
        private Button _buttonHighScore;
        [SerializeField]
        private Button _buttonOption;
        
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
            Singleton<ContextManager>.Instance.Push(new OptionMenuContext());
        }

        public void HighScoreCallBack()
        {
            Singleton<ContextManager>.Instance.Push(new HighScoreContext());
        }
        public void GameStartCallBack()
        {
            string path = "Scenes" + "/" + "LoadingScenes";
            AssetManager.LoadCallBack += substartcallback;
            AssetManager.GetInstance().startloadAsset(path);
        }
        void substartcallback()
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
