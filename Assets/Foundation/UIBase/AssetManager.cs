using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace MoleMole
{
    public class AssetManager : MonoBehaviour
    {

        public static Dictionary<string, Object> AseetFamily = new Dictionary<string, Object>();

        public AssetBundle GetAsset(string name)
        {
            return !AseetFamily.ContainsKey(name) ? null : AseetFamily[name] as AssetBundle;
        }
        public void startloadAsset(string name)
        {
            StartCoroutine(load_sub(name));
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
                if (!AseetFamily.ContainsKey(assetname))
                {
                    AseetFamily.Add(assetname, ab);
                }
                ab.Unload(false);
            }
            else
            {
                Debug.LogError(www.error);
            }
        }

    }
}
