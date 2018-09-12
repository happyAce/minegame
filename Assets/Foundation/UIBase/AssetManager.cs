using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace MoleMole
{
    public class AssetManager : MonoBehaviour
    {
        // Hashtable AssetsNotCached = new Hashtable();
        private static AssetManager am;
        public static Dictionary<string, Object> AseetFamily = new Dictionary<string, Object>();
        public delegate void LoadDelegate();
        public static LoadDelegate LoadCallBack;
        public static bool Loading = false;
        void Awake()
        {
            am = this;
        }
        public static AssetManager GetInstance()
        {
            return am;
        }

        public AssetBundle GetAsset(string name)
        {
            return !AseetFamily.ContainsKey(name) ? null : AseetFamily[name] as AssetBundle;
        }

        public void startloadAsset(string name)
        {
            if (!AseetFamily.ContainsKey(name) || AseetFamily[name] == null)
            {
                int a = 0;
                StartCoroutine(load_sub(name));
                
            }
            else
            {
                if (LoadCallBack != null)
                {
                    LoadCallBack();
                    LoadCallBack = null;
                }
            }
        }
        IEnumerator load_sub(string name)
        {
            string[] sArray = name.Split('/');
            string assetname = sArray[1];
            string suffix = "";
            string filename = "";
            switch (sArray[0])
            {
                case "View":
                    {
                        suffix = ".aseetbundle";
                        filename = "StreamingAssets";
                    }
                    break;
                case "Scenes":
                    {
                        suffix = ".unity3d";
                        filename = "Scenes";
                    }
                    break;

            }
            string path = "file://" + Application.dataPath + "/../WebAssets/" + filename + "/" + assetname + suffix;
            WWW www = new WWW(path);
            yield return www;
            if (www.error == null)
            {
                AssetBundle ab = www.assetBundle;
                if (!AseetFamily.ContainsKey(name))
                {
                    AseetFamily.Add(name, ab);
                }
                if (LoadCallBack != null)
                {
                    LoadCallBack();
                    LoadCallBack = null;
                }
                //ab.Unload(false);
            }
            else
            {
                Debug.LogError(www.error);
            }
        }

    }
}
