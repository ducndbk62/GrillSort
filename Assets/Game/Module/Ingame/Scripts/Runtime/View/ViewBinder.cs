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

    public SkewerView CreateSkewerData(int x, int y, SkewerData skewerData, Transform transformParent, Vector3 localPos)
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

    public GameObject CreateUISkewer(int idSkewer , Vector3 vecPos, Vector3 vecScale, Transform transParent)
    {
        var prefab = prefabLibrary.skewerPrefabs[idSkewer].prefab;
        if (prefab == null)
        {
            Debug.LogError($"Missing prefab");
            return null;
        }
        var go = Instantiate(prefab, Vector3.zero, Quaternion.identity, transParent);
        go.transform.localPosition = vecPos;
        go.transform.localScale = vecScale;
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

    public GameObject CreateTrayDoor(Transform transform)
    {
        var prefab = prefabLibrary.objTrayDoor;
        if (prefab == null)
        {
            Debug.LogError($"Missing prefab for plate");
            return null;
        }
        var go = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        go.transform.localPosition = Vector3.zero;
        return go;
    }

    public GameObject CreateSkewerIce(float posY, Transform transform)
    {
        var prefab = prefabLibrary.objSkewerIce;
        if (prefab == null)
        {
            Debug.LogError($"Missing prefab for objSkewerIce");
            return null;
        }
        var go = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        go.transform.localPosition = new Vector3(0, posY, 0);
        return go;
    }
}

