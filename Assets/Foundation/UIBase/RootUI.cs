using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoleMole
{
    public class RootUI : MonoBehaviour {

        public GameObject root;
        static public GameObject static_root;
        void Awake()
        {
            static_root = root;
        }
        public static T getUI<T>() where T : BaseView
        {
            if (static_root == null)
                return null;
            T ui = static_root.GetComponent<T>();
            if (ui == null)
            {
                ui = static_root.AddComponent<T>();

            }
            return ui;
        }
        public static BaseView getUI(string name)
        {
            if (static_root == null)
            {
                return null;
            }
            BaseView ui = static_root.GetComponent(name) as BaseView;
            return ui;
        }
        public static void AddAllUIs()
        {
            getUI<HighScoreView>();
            getUI<MainMenuView>();
            getUI<OptionMenuView>();
            getUI<NextMenuView>();
            getUI<RestartView>();

        }
        void Start()
        { 
            AddAllUIs();
        }
    }
}
