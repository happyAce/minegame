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
    
    int progress = 0;
    void Start()
    {
        StartCoroutine(loadScene());
    }
    //注意这里返回值一定是 IEnumerator

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(3f);
        async = Application.LoadLevelAsync(GlobLoadScenes.loadName);
        yield return async;
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
