using System;
using UnityEngine;

namespace Falcon.GrillSort.LevelEditor.Runtime
{
    public class EditorEvents
    {
        #region INIT
        private static EditorEvents _instance = null;
        public static EditorEvents Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.Log("Create new EditorEvents");
                    _instance = new EditorEvents();
                }
                return _instance;
            }
        }
        #endregion

        //Layer
        public Action<int> OnChangeLayerBoard;
        //public Action OnClickNextLayer;
        //public Action OnClickPreviousLayer;

        //Type Select
        public Action OnSelectTypeCell;
        public Action OnSelectTypeItem;
    }
}