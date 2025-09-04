using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Falcon.GrillSort.LevelEditor.Runtime
{
    public class LevelEditorManager : Singleton<LevelEditorManager>
    {
        public TextAsset level;
        public LevelData levelData;
        [Tooltip("Trong edit level, mỗi idSkewer sẽ ứng với 1 sprite tương ứng ở list này")]
        public List<Sprite> listSpriteSkewer;

        [Button]
        private void LoadLevel()
        {
            LevelLoader.Instance.LoadLevel(level);
        }
    }
}
