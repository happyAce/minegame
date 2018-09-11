using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *	
 *  Manage View's Create And Destory
 *
 *	by Xuanyi
 *
 */

namespace MoleMole
{
    public class UIManager: MonoBehaviour
    {
        public Dictionary<UIType, GameObject> _UIDict = new Dictionary<UIType,GameObject>();

        private Transform _canvas;
        private UIManager()
        {
            _canvas = GameObject.Find("Canvas").transform;
            foreach (Transform item in _canvas)
            {
                GameObject.Destroy(item.gameObject);
            }
        }

        public GameObject GetSingleUI(UIType uiType)
        {
            if (_UIDict.ContainsKey(uiType) == false || _UIDict[uiType] == null)
            {
                AssetManager.GetInstance().startloadAsset(uiType.Path);
                //GameObject go = GameObject.Instantiate(Resources.Load<GameObject>(uiType.Path)) as GameObject;
                AssetBundle bundle = AssetManager.GetInstance().GetAsset(uiType.Name);
                StartCoroutine(waitGameObject);
                GameObject go = GameObject.Instantiate(bundle.LoadAsset(uiType.Name)) as GameObject;
               
                go.transform.SetParent(_canvas, false);
                go.name = uiType.Name;
                _UIDict.AddOrReplace(uiType, go);
                return go;
            }
            return _UIDict[uiType];
        }
        private IEnumerator waitGameObject(Object o)
        {
            yield return GameObject.Instantiate(o) as GameObject;

        }
        public void DestroySingleUI(UIType uiType)
        {
            if (!_UIDict.ContainsKey(uiType))
            {
                return;
            }

            if (_UIDict[uiType] == null)
            {
                _UIDict.Remove(uiType);
                return;
            }

            GameObject.Destroy(_UIDict[uiType]);
            _UIDict.Remove(uiType);
        }
	}
}
