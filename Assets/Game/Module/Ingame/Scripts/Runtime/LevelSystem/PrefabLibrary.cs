using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NamedPrefab
{
    public string name;
    public GameObject prefab;
}

public class PrefabLibrary : MonoBehaviour
{
    public GameObject gridCellPrefab;
    public GameObject skewerPrefab;
    public GameObject objPlate;
    public GameObject objPlateClear;
    public GameObject objTrayDoor;
    public GameObject objSkewerIce;
    public List<NamedPrefab> skewerPrefabs;
}
