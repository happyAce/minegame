﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace MoleMole
{
    public class LoadingScreen : MonoBehaviour
    {

        public class GlobLoadScenes
        {
            //在这里记录当前切换场景的名称
            public static string loadName;
            public static string beforeName;

        }
        string secenes_path;
        private void Awake()
        {
            //secenes_path = "file://" + Application.dataPath + "/../WebAssets/" + "Scenes" + "/"+ GlobLoadScenes.loadName+".unity3d";
            secenes_path = "Scenes" + "/" + GlobLoadScenes.loadName;
        }
        void Start()
        {
            AssetManager.LoadCallBack = loadcb;
            AssetManager.GetInstance().startloadAsset(secenes_path);
        }
        void loadcb()
        {
            StartCoroutine( loading() );
        }
        IEnumerator loading()
        {
            AsyncOperation asy = SceneManager.LoadSceneAsync(GlobLoadScenes.loadName);
            yield return asy;
        }
        //注意这里返回值一定是 IEnumerator

        //IEnumerator loadScene()
        // {
        //WWW www = WWW.LoadFromCacheOrDownload(secenes_path, 1);
        //WWW www = new WWW(secenes_path);
        //yield return www;

        //if (www.error == null)
        //{
        //    AssetBundle ab = www.assetBundle; //将场景通过AssetBundle方式加载到内存中
        //    //异步对象
        //    AsyncOperation asy = SceneManager.LoadSceneAsync(GlobLoadScenes.loadName); //sceneName不能加后缀,只是场景名称
        //    //AsyncOperation asy = Application.LoadLevelAdditiveAsync(GlobLoadScenes.loadName);
        //    yield return asy;
        //    ab.Unload(false);
        //}
        //else
        //{
        //    Debug.LogError(www.error);
        //}
        // }
         
    }
}
