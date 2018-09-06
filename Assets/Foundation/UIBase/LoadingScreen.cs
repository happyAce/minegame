using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

    public class GlobLoadScenes
    {
        //在这里记录当前切换场景的名称
        public static string loadName;

    }
   
    private float fps = 10.0f;
    private float time;
    //异步对象
    AsyncOperation async;
    string secenes_path;
    int progress = 0;
    private void Awake()
    {
        secenes_path = Application.dataPath + "/"+ GlobLoadScenes.loadName+".unity3d";
    }
    void Start()
    {
        StartCoroutine(loadScene());
    }
    //注意这里返回值一定是 IEnumerator

    IEnumerator loadScene()
    {
        WWW www = WWW.LoadFromCacheOrDownload(secenes_path, 1);
       // WWW www = new WWW(secenes_path);
        yield return www;

        AssetBundle bundle = www.assetBundle;
        Instantiate(bundle.mainAsset);
        bundle.Unload(false);

        //yield return new WaitForSeconds(3f);
        // Application.LoadLevelAsync(GlobLoadScenes.loadName);
        // yield return async;
    }

    void OnGUI()
    {
        DrawTitle();
    }
    void Update()
    {
    }
    void DrawTitle()
    {
      
    }
}
