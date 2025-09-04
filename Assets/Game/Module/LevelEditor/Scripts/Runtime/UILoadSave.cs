using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Falcon.GrillSort.LevelEditor.Runtime
{
    public class UILoadSave : MonoBehaviour
    {
        [Header("Save Load")]
        [SerializeField] string folderPath = Application.dataPath + "/Game/Module/Ingame/Resources/LevelData";
        [SerializeField] Text textFileName;
        [SerializeField] LevelData levelData;

        public void SaveLevel(bool saveAs)
        {
            SetLevelDataFromScene();
            //string json = JsonUtility.ToJson(LevelLoader.Instance.levelData, true);
            //if (saveAs)
            //{
            //    string path = EditorUtility.SaveFilePanel("Lưu file level", folderPath, fileName.text, "txt");
            //    if (!string.IsNullOrEmpty(path))
            //    {
            //        fileName.text = Path.GetFileName(path);
            //        folderPath = Path.GetDirectoryName(path);
            //        File.WriteAllText(path, json);
            //        Debug.Log("Đã lưu: " + path);
            //    }
            //}
            //else
            //{
            //    string path = Path.Combine(folderPath, fileName.text);
            //    File.WriteAllText(path, json);
            //    Debug.Log("Đã lưu: " + path);
            //}
        }

        public void LoadLevel()
        {
            //string path = EditorUtility.OpenFilePanel("Chọn file level", folderPath, "txt");
            //if (!string.IsNullOrEmpty(path))
            //{
            //    fileName.text = Path.GetFileName(path);
            //    folderPath = Path.GetDirectoryName(path);
            //    string json = File.ReadAllText(path);
            //    LevelLoader.Instance.LoadLevel(json);
            //    timeInputField.text = LevelLoader.Instance.levelData.time.ToString();
            //    difficultyInputField.text = LevelLoader.Instance.levelData.difficulty.ToString();
            //    widthInputField.text = LevelLoader.Instance.levelData.width.ToString();
            //    heightInputField.text = LevelLoader.Instance.levelData.height.ToString();
            //    int idCoumpound = 0;
            //    foreach (var drag in FindObjectsByType<BlockDragAndDrop>(FindObjectsSortMode.None))
            //    {
            //        drag.enabled = false;
            //        if (drag.IsCompoundBlock)
            //        {
            //            foreach (var view in drag.listViewSub)
            //                view.idCompound = idCoumpound;
            //            idCoumpound++;
            //        }
            //    }
            //    playingTest = false;
            //}
        }

        [SerializeField] Image imgBtnPlay;
        bool playingTest = false;
        LevelData cacheLevelData;
        public void Play()
        {
            //if (!playingTest)
            //{
            //    SetLevelDataFromScene();
            //    cacheLevelData = LevelLoader.Instance.levelData;
            //    string json = JsonUtility.ToJson(LevelLoader.Instance.levelData, true);
            //    LevelLoader.Instance.LoadLevel(json);
            //    playingTest = true;
            //    imgBtnPlay.color = Color.red;
            //    imgBtnPlay.GetComponentInChildren<Text>().text = "Stop";
            //}
            //else // Stop play test
            //{
            //    playingTest = false;
            //    LevelLoader.Instance.levelData = cacheLevelData;
            //    string json = JsonUtility.ToJson(LevelLoader.Instance.levelData, true);
            //    LevelLoader.Instance.LoadLevel(json);
            //    int idCoumpound = 0;
            //    foreach (var drag in FindObjectsByType<BlockDragAndDrop>(FindObjectsSortMode.None))
            //    {
            //        drag.enabled = false;
            //        if (drag.IsCompoundBlock)
            //        {
            //            foreach (var view in drag.listViewSub)
            //                view.idCompound = idCoumpound;
            //            idCoumpound++;
            //        }
            //    }
            //    imgBtnPlay.color = Color.white;
            //    imgBtnPlay.GetComponentInChildren<Text>().text = "Play";
            //}
        }

        private void SetLevelDataFromScene()
        {
            //LevelLoader.Instance.levelData.blocks = new();
        }
    }
}
