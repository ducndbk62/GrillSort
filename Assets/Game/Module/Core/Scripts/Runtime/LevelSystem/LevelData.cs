using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public int time;
    public int difficulty; // 0: normal, 1: hard, 2: very hard
    public int width;
    public int height;
    public List<GridCellData> gridCellData;
}

[System.Serializable]
public class GridCellData
{
    public int x, y;
    public int typeUnlockTray;
    public int typeTray;
    public List<LayerSkewerData> listLayerSkewer;
}

[System.Serializable]
public class LayerSkewerData
{
    public List<SkewerData> listSkewerData;
}

[System.Serializable]
public class SkewerData
{
    public int indexSlot;
    public string idSkewer;
}

