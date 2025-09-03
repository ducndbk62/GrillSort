using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Falcon.GrillSort.LevelEditor.Runtime
{
    public class UILoadSave : MonoBehaviour
    {
        //[Header("Save Load")]
        //[SerializeField] string folderPath = Application.dataPath + "/Game/Module/Ingame/Resources/LevelData";
        //[SerializeField] Text fileName;
        //[SerializeField] LevelData
        ////[SerializeField] InputField timeInputField, difficultyInputField, widthInputField, heightInputField;
        ////[SerializeField] Image imgBtnAddRemoveCell;

        //public void SaveLevel(bool saveAs)
        //{
        //    SetLevelDataFromScene();
        //    string json = JsonUtility.ToJson(LevelLoader.Instance.levelData, true);
        //    if (saveAs)
        //    {
        //        string path = EditorUtility.SaveFilePanel("Lưu file level", folderPath, fileName.text, "txt");
        //        if (!string.IsNullOrEmpty(path))
        //        {
        //            fileName.text = Path.GetFileName(path);
        //            folderPath = Path.GetDirectoryName(path);
        //            File.WriteAllText(path, json);
        //            Debug.Log("Đã lưu: " + path);
        //        }
        //    }
        //    else
        //    {
        //        string path = Path.Combine(folderPath, fileName.text);
        //        File.WriteAllText(path, json);
        //        Debug.Log("Đã lưu: " + path);
        //    }
        //}

        //public void LoadLevel()
        //{
        //    string path = EditorUtility.OpenFilePanel("Chọn file level", folderPath, "txt");
        //    if (!string.IsNullOrEmpty(path))
        //    {
        //        fileName.text = Path.GetFileName(path);
        //        folderPath = Path.GetDirectoryName(path);
        //        string json = File.ReadAllText(path);
        //        LevelLoader.Instance.LoadLevel(json);
        //        timeInputField.text = LevelLoader.Instance.levelData.time.ToString();
        //        difficultyInputField.text = LevelLoader.Instance.levelData.difficulty.ToString();
        //        widthInputField.text = LevelLoader.Instance.levelData.width.ToString();
        //        heightInputField.text = LevelLoader.Instance.levelData.height.ToString();
        //        int idCoumpound = 0;
        //        foreach (var drag in FindObjectsByType<BlockDragAndDrop>(FindObjectsSortMode.None))
        //        {
        //            drag.enabled = false;
        //            if (drag.IsCompoundBlock)
        //            {
        //                foreach (var view in drag.listViewSub)
        //                    view.idCompound = idCoumpound;
        //                idCoumpound++;
        //            }
        //        }
        //        playingTest = false;
        //    }
        //}

        //[SerializeField] Image imgBtnPlay;
        //bool playingTest = false;
        //LevelData cacheLevelData;
        //public void Play()
        //{
        //    if (!playingTest)
        //    {
        //        SetLevelDataFromScene();
        //        cacheLevelData = LevelLoader.Instance.levelData;
        //        string json = JsonUtility.ToJson(LevelLoader.Instance.levelData, true);
        //        LevelLoader.Instance.LoadLevel(json);
        //        playingTest = true;
        //        imgBtnPlay.color = Color.red;
        //        imgBtnPlay.GetComponentInChildren<Text>().text = "Stop";
        //    }
        //    else // Stop play test
        //    {
        //        playingTest = false;
        //        LevelLoader.Instance.levelData = cacheLevelData;
        //        string json = JsonUtility.ToJson(LevelLoader.Instance.levelData, true);
        //        LevelLoader.Instance.LoadLevel(json);
        //        int idCoumpound = 0;
        //        foreach (var drag in FindObjectsByType<BlockDragAndDrop>(FindObjectsSortMode.None))
        //        {
        //            drag.enabled = false;
        //            if (drag.IsCompoundBlock)
        //            {
        //                foreach (var view in drag.listViewSub)
        //                    view.idCompound = idCoumpound;
        //                idCoumpound++;
        //            }
        //        }
        //        imgBtnPlay.color = Color.white;
        //        imgBtnPlay.GetComponentInChildren<Text>().text = "Play";
        //    }
        //}

        //private void SetLevelDataFromScene()
        //{
        //    LevelLoader.Instance.levelData.blocks = new();
        //    Dictionary<int, int> dicIdCompound = new(); // key là id trong BlockView, value là index của nó trong LevelLoader.Instance.levelData.blocks
        //    foreach (var obj in FindObjectsByType<BlockDragAndDrop>(FindObjectsSortMode.None))
        //    {
        //        if (obj.blockView.idCompound >= 0) // nếu là 1 khối trong 1 compound
        //        {
        //            if (dicIdCompound.ContainsKey(obj.blockView.idCompound)) // nếu có key tức là đã tạo block trước rồi, giờ chỉ thêm vào shape thôi
        //            {
        //                var compound = LevelLoader.Instance.levelData.blocks[dicIdCompound[obj.blockView.idCompound]];
        //                compound.shapes.Add(new SubBlockShapeData()
        //                {
        //                    shapeId = obj.shapeId,
        //                    indexShape = compound.shapes.Count,
        //                    rotation = obj.rotation,
        //                    offset = obj.centerGridPos - new Vector2Int(compound.x, compound.y),
        //                    layer = obj.layers[0],
        //                });
        //            }
        //            else //tạo block mới tại vị trí block này và gán block này vào shape đầu tiên luôn
        //            {
        //                dicIdCompound[obj.blockView.idCompound] = LevelLoader.Instance.levelData.blocks.Count;
        //                LevelLoader.Instance.levelData.blocks.Add(new()
        //                {
        //                    x = obj.centerGridPos.x,
        //                    y = obj.centerGridPos.y,
        //                    shapes = new()
        //                    {
        //                        new SubBlockShapeData() // indexShape và offset bằng mặc định = 0
        //                        {
        //                            shapeId = obj.shapeId,
        //                            rotation = obj.rotation,
        //                            layer = obj.layers[0],
        //                        }
        //                    }
        //                });
        //            }
        //        }
        //        else
        //            LevelLoader.Instance.levelData.blocks.Add(new BlockData
        //            {
        //                x = obj.centerGridPos.x,
        //                y = obj.centerGridPos.y,
        //                shapeId = obj.shapeId,
        //                rotation = obj.rotation,
        //                movementAxis = obj.moveAxis,
        //                keyAndLock = obj.blockKeyAndLock,
        //                layers = obj.layers,
        //                shapes = obj.listDataSubShapes,
        //            });
        //    }
        //    LevelLoader.Instance.levelData.targets = new();
        //    foreach (var obj in FindObjectsByType<TargetView>(FindObjectsSortMode.None))
        //        if (obj.x >= 0) // trong elevator hay pipe cũng có TargetView nhưng x,y=-1
        //            LevelLoader.Instance.levelData.targets.Add(new TargetData()
        //            {
        //                x = obj.x,
        //                y = obj.y,
        //                targetCustom = obj.targetBase,
        //            });
        //    LevelLoader.Instance.levelData.elevators = new();
        //    foreach (var obj in FindObjectsByType<ElevatorView>(FindObjectsSortMode.None))
        //        LevelLoader.Instance.levelData.elevators.Add(obj.elevatorData);
        //    LevelLoader.Instance.levelData.pipes = new();
        //    foreach (var obj in FindObjectsByType<PipeView>(FindObjectsSortMode.None))
        //        LevelLoader.Instance.levelData.pipes.Add(obj.pipeData);
        //}
    }
}
