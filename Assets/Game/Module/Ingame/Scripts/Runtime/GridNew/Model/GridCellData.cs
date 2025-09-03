
[System.Serializable]
public sealed class GridCellState
{
    public GridTypeUnlockTray typeUnlockTray = GridTypeUnlockTray.Empty;
    public GridTypeTray typeTray = GridTypeTray.Empty;
    public SkewerView[] skewersView = new SkewerView[GridConstants.MaxSkewerSlots];
    public GridCellData gridCellData;

    public void SetTypeFirstGame()
    {
        typeUnlockTray = (GridTypeUnlockTray)gridCellData.typeUnlockTray;
        typeTray = (GridTypeTray)gridCellData.typeTray;
    }

    public void SetTypeUnlockTray(GridTypeUnlockTray unlockType)
    {
        typeUnlockTray = unlockType;
    }

    public void SetType(GridTypeTray type)
    {
        typeTray = type;
    }


    public void Clear()
    {
        for (int i = 0; i < skewersView.Length; i++)
            skewersView[i] = null;
    }
}