using System.Collections.Generic;
using UnityEngine;

namespace Falcon.GrillSort.LevelEditor.Runtime
{
    public class UIGridCell : MonoBehaviour
    {
        [SerializeField] GameObject objPlate, objSkewer;
        [SerializeField] Transform posPlate;
        //[SerializeField] List<Transform> ;

        public GridCellData cellData;

        public void SetUICell(GridCellData cellData)
        {
            // Set type cell
            //.........
            //
            if (cellData.listLayerSkewer.Count == 0)
                return;
            foreach (var skew in cellData.listLayerSkewer[0].listSkewerData)
            {

            }
        }
    }
}
