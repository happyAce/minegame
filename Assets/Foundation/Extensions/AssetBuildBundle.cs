using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetBuildBundle : Editor {

    [MenuItem("Assets/Tools/Create AssetBundles Main")]
    static void CreatAssetBundlesMain()
    {
        //获取当前在project视窗中选择的资源文件
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach(Object o in SelectedAsset)
        {
            //本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
            //StreamingAssets是只读路径，不能写入
            //服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
            string targetpath = Application.dataPath + "/StreamingAssets/" + o.name + ".aseetbundle";
            if (BuildPipeline.BuildAssetBundle(o, null, targetpath, BuildAssetBundleOptions.CollectDependencies,BuildTarget.StandaloneWindows))
            {
                Debug.Log(o.name + "资源打包成功");
            }
            else
            {
                Debug.Log(o.name + "资源打包失败");
            }
        }
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Tools/Create AssetBunldes ALL")]
    static void CreateAssetBunldesALL()
    {

        Caching.ClearCache();


        string Path = Application.dataPath + "/StreamingAssets/ALL.assetbundle";


        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object obj in SelectedAsset)
        {
            Debug.Log("Create AssetBunldes name :" + obj);
        }

        //这里注意第二个参数就行
        if (BuildPipeline.BuildAssetBundle(null, SelectedAsset, Path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows))
        {
            AssetDatabase.Refresh();
        }
        else
        {

        }
    }

    [MenuItem("Assets/Tools/Create Scene")]
    static void CreateSceneALL()
    {
        //清空一下缓存
        Caching.ClearCache();
        string Path = Application.dataPath + "/MyScene.unity3d";
        string[] levels = { "Assets/Scenes/LoadingScenes.unity" };
        //打包场景
       
        if(BuildPipeline.BuildPlayer(levels, Path, BuildTarget.StandaloneWindows, BuildOptions.BuildAdditionalStreamedScenes))
        {

            Debug.Log("build场景成功");
            AssetDatabase.Refresh();
        }
        
    }

}
