using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *	
 *  Manage Context For UI Stack
 *
 *	by Xuanyi
 *
 */

namespace MoleMole
{
    public class ContextManager
    {
        private Stack<BaseContext> _contextStack = new Stack<BaseContext>();

        private ContextManager()
        {
           
        }
        
        public BaseContext nextContext;
        public void Push(BaseContext _nextContext)
        {
            nextContext = _nextContext;
            if (_contextStack.Count != 0)
            {
                BaseContext curContext = _contextStack.Peek();
                AssetManager.LoadCallBack += pushcurcallback;
                AssetManager.GetInstance().startloadAsset(curContext.ViewType.Path);
            }
            AssetManager.LoadCallBack += pushnextcallback;
            AssetManager.GetInstance().startloadAsset(nextContext.ViewType.Path);
        }
        void pushcurcallback()
        {
            if (_contextStack.Count != 0)
            {
                BaseContext curContext = _contextStack.Peek();
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnPause(curContext);
            }
          
        }
        void pushnextcallback()
        {
            _contextStack.Push(nextContext);
            BaseView nextView = Singleton<UIManager>.Instance.GetSingleUI(nextContext.ViewType).GetComponent<BaseView>();
            nextView.OnEnter(_contextStack.Peek());
        }
        public void Pop()
        {
            if (_contextStack.Count != 0)
            {
                BaseContext curContext = _contextStack.Peek();
                _contextStack.Pop();
               
               BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
               curView.OnExit(curContext);
            }

            if (_contextStack.Count != 0)
            {
                BaseContext lastContext = _contextStack.Peek();
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(lastContext.ViewType).GetComponent<BaseView>();
                curView.OnResume(lastContext);
                
            }
        }
        void popcallback()
        {
            if (_contextStack.Count != 0)
            {
                BaseContext curContext = _contextStack.Peek();
                _contextStack.Pop();

                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnExit(curContext);
            }

            if (_contextStack.Count != 0)
            {
                BaseContext lastContext = _contextStack.Peek();
     
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(lastContext.ViewType).GetComponent<BaseView>();
                curView.OnResume(lastContext);
            }
        }
        public BaseContext PeekOrNull()
        {
            if (_contextStack.Count != 0)
            {
                return _contextStack.Peek();
            }
            return null;
        }
        public void Clear()
        {
            if(_contextStack.Count != 0 )
                _contextStack.Clear();
        }
    }
}
