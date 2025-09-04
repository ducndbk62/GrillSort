using UnityEngine;

namespace Falcon.GrillSort.LevelEditor.Runtime
{
    public class UILevelIngame : MonoBehaviour
    {
        [SerializeField] GameObject objCell;
        [SerializeField] Transform posCell;
        
        public void SetUILevel(LevelData levelData)
        {
            foreach (var cell in levelData.gridCellData)
            {

            }
        }
    }
}
