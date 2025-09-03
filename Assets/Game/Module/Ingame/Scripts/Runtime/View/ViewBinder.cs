using System.Collections.Generic;
using UnityEngine;

public class ViewBinder : MonoBehaviour
{
    [SerializeField] private PrefabLibrary prefabLibrary;

    public GameObject CreateGridCell(Vector3 pos , Transform trans)
    {
        var prefab = prefabLibrary.gridCellPrefab;
        if (prefab == null)
        {
            Debug.LogError($"Missing prefab for gridCell");
            return null;
        }
        var go = Instantiate(prefab, pos, Quaternion.identity, transform);
        return go;
    }

    public SkewerView CreateSkewer(int x, int y, SkewerData skewerData, Transform transformParent, Vector3 localPos)
    {
        var prefab = prefabLibrary.skewerPrefab;
        if (prefab == null)
        {
            Debug.LogError($"Missing prefab for {skewerData.idSkewer}");
            return null;
        }
        var go = Instantiate(prefab, GridUtils.GridToWorld(new Vector2Int(x, y)), Quaternion.identity, transformParent);
        go.transform.localPosition = localPos;
        SkewerView skewerView = go.GetComponent<SkewerView>();
        skewerView.SetData(x , y, skewerData);
        return skewerView;
    }

    public GameObject CreateUISkewer(int idTest , Transform transform)
    {
        var prefab = prefabLibrary.skewerPrefabs[idTest].prefab;
        if (prefab == null)
        {
            Debug.LogError($"Missing prefab");
            return null;
        }
        var go = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        go.transform.localPosition = new Vector3(0,0.2f,0);
        go.transform.localScale = new Vector3(0.9f,0.9f,1f);
        return go;
    }

    public GameObject CreatePlate(float posY, Transform transform)
    {
        var prefab = prefabLibrary.objPlate;
        if (prefab == null)
        {
            Debug.LogError($"Missing prefab for plate");
            return null;
        }
        var go = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        go.transform.localPosition = new Vector3(0, posY, 0);
        return go;
    }
}

