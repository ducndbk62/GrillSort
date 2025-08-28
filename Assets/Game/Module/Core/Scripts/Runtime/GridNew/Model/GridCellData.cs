
[System.Serializable]
public sealed class GridCellState
{
    public GridObjectType objectType = GridObjectType.Empty;
    public GridCellData gridCellData;

    public SkewerView[] skewersView = new SkewerView[GridConstants.MaxSkewerSlots];
    public bool IsEmpty => objectType == GridObjectType.Empty;

    public void Clear()
    {
        for (int i = 0; i < skewersView.Length; i++)
            skewersView[i] = null;
    }
}