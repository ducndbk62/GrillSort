using System.Collections.Generic;
using UnityEngine;

public class ViewBinder : MonoBehaviour
{
    [SerializeField] private PrefabLibrary prefabLibrary;

    public void CreateSkewer(SkewerData skewerData)
    {
        //var prefab = prefabLibrary.GetBlock(skewerData.idSkewer);
        //if (prefab == null)
        //{
        //    Debug.LogError($"Missing prefab for {skewerData.idSkewer}");
        //    return;
        //}
        //var go = Instantiate(prefab, GridUtils.GridToWorld(new Vector2Int(skewerData.x, skewerData.y)), Quaternion.identity);
        //SkewerView skewerView = go.GetComponent<SkewerView>();
       // skewerView.SetData(skewerData);
        //var go = Instantiate(prefab, GridUtils.GridToWorld(new Vec));
    }
}
