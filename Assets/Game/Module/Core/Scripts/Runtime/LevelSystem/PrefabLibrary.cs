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
    public List<NamedPrefab> skewerPrefabs;

    public GameObject GetBlock(string idSkewer)
    {
        string key = $"{idSkewer}";
        return skewerPrefabs.Find(p => p.name == key)?.prefab;
    }

}
