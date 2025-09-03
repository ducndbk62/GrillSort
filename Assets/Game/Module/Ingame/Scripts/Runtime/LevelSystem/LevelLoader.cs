using System.Collections;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [Header("Setup")]
    public TextAsset levelJson;
    public GridController gridController;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            LoadLevel(levelJson);
        }
    }

    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        Application.targetFrameRate = 60;
        yield return new WaitForSeconds(3f);
        LoadLevel(levelJson);
    }


    public void LoadLevel(TextAsset json)
    {
        LevelData level = JsonUtility.FromJson<LevelData>(json.text);
        gridController.InitGrid(level);
        //----------------------------------------------------
        //foreach (var skewer in level.skewer)
        //{
        //    viewBinder.CreateSkewer(skewer);
        //}
        //foreach (var target in level.targets)
        //{
        //    Vector2Int pos = new Vector2Int(target.x, target.y);
        //    GridManager.Instance.SetCell(pos.x, pos.y, GridObjectType.Target, target.color);
        //    viewBinder.CreateTargetView(pos, target.color);
        //}
    }

}
