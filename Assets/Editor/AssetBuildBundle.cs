using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class AssetBuildBundle : Editor {

    static BuildTarget get_Platform()
    {
        BuildTarget _Platform = BuildTarget.StandaloneWindows64;
#if UNITY_EDITOR
        _Platform = BuildTarget.StandaloneWindows64;
#elif UNITY_IPHONE
        _Platform = BuildTarget.iOS;
#elif UNITY_ANDROID
        _Platform = BuildTarget.Android;
#elif UNITY_STANDALONE_WIN
        _Platform = BuildTarget.StandaloneWindows;
#endif
        return _Platform;
    }
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
            string targetpath = Application.dataPath + "/../WebAssets" + "/StreamingAssets/" + o.name + ".aseetbundle";
            string dpath = Application.dataPath + "/../WebAssets" + "/StreamingAssets";

            if (!Directory.Exists(dpath))
            {
                DirectoryInfo dinfo = Directory.CreateDirectory(dpath);

            }
            if (BuildPipeline.BuildAssetBundle(o, null, targetpath, BuildAssetBundleOptions.CollectDependencies, get_Platform()))
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


        string Path = Application.dataPath + "/../WebAssets" + "/StreamingAssets/ALL.assetbundle";
        string dpath = Application.dataPath + "/../WebAssets" + "/StreamingAssets";

        if (!Directory.Exists(dpath))
        {
            DirectoryInfo dinfo = Directory.CreateDirectory(dpath);

        }

        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        foreach (Object obj in SelectedAsset)
        {
            Debug.Log("Create AssetBunldes name :" + obj);
        }

        //这里注意第二个参数就行
        if (BuildPipeline.BuildAssetBundle(null, SelectedAsset, Path, BuildAssetBundleOptions.CollectDependencies, get_Platform()))
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
       // string Path = Application.dataPath + "/MyScene.unity3d";
       // string[] levels = { "Assets/Scenes/LoadingScenes.unity" };
        //打包场景


        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        foreach (Object o in SelectedAsset)
        {
            //本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
            //StreamingAssets是只读路径，不能写入
            //服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
            string targetpath = Application.dataPath + "/../WebAssets" +"/Scenes/" + o.name+".unity3d";
            string dpath = Application.dataPath + "/../WebAssets" + "/Scenes";
            
            if (!Directory.Exists(dpath))
            {
                DirectoryInfo dinfo = Directory.CreateDirectory(dpath);
                    
            }
            string[] levels = { "Assets/Scenes/"+ o.name + ".unity" };
            if (BuildPipeline.BuildPlayer(levels, targetpath, get_Platform(), BuildOptions.BuildAdditionalStreamedScenes))
            {

                Debug.Log("build场景成功:"+o.name+".unity3d");
                AssetDatabase.Refresh();
            }
        }

        //if (BuildPipeline.BuildPlayer(levels, Path, BuildTarget.StandaloneWindows, BuildOptions.BuildAdditionalStreamedScenes))
        //{

        //    Debug.Log("build场景成功");
        //    AssetDatabase.Refresh();
        //}
        
    }

}
