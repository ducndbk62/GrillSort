using System;

public sealed class GridModel
{
    public GridCellView[,] CellViews { get; set; }

    public struct SkewerMoved
    {
        public SkewerView Skewer;
        public int FromX, FromY, FromSlot;
        public int ToX, ToY, ToSlot;
        public SkewerMoved(SkewerView s, int fx, int fy, int fs, int tx, int ty, int ts)
        {
            Skewer = s; FromX = fx; FromY = fy; FromSlot = fs; ToX = tx; ToY = ty; ToSlot = ts;
        }
    }
    public struct CellCompleted { public int X, Y; public CellCompleted(int x, int y) { X = x; Y = y; } }

    public event Action<SkewerMoved> OnSkewerMoved;
    public event Action<CellCompleted> OnCellCompleted;

    public bool InBounds(int x, int y) => x >= 0 && x < GridUtils.WIDTH && y >= 0 && y < GridUtils.HEIGHT;

    public bool TrySnap(SkewerView skewerView, int toX, int toY, int toSlot)
    {
        int fx = skewerView.x, fy = skewerView.y, fs = skewerView.skewerData.indexSlot;
        if (!InBounds(toX, toY)) return false;
        var cell = CellViews[toX, toY];
        if (cell.gridCellState.typeTray == GridTypeTray.Empty) return false;
        if (toSlot < 0 || toSlot >= GridConstants.MaxSkewerSlots) return false;
        if (cell.gridCellState.skewersView[toSlot] != null) return false;
        if (skewerView.x == toX && skewerView.y == toY && skewerView.skewerData.indexSlot == toSlot) return true;
        if (skewerView.x >= 0 && InBounds(skewerView.x, skewerView.y))
        {
            var oldCell = CellViews[skewerView.x, skewerView.y];
            if (skewerView.skewerData.indexSlot >= 0 && skewerView.skewerData.indexSlot < oldCell.gridCellState.skewersView.Length)
            {
                oldCell.gridCellState.skewersView[skewerView.skewerData.indexSlot] = null;
            }
        }
        skewerView.x = toX; skewerView.y = toY; skewerView.skewerData.indexSlot = toSlot;
        cell.gridCellState.skewersView[toSlot] = skewerView;
        OnSkewerMoved?.Invoke(new SkewerMoved(skewerView, fx, fy, fs, toX, toY, toSlot));
        if (IsCellComplete(toX, toY))
        {
            OnCellCompleted?.Invoke(new CellCompleted(toX, toY));
        }
        return true;
    }

    public bool IsCellComplete(int x, int y)
    {
        if (!InBounds(x, y)) return false;
        var s = CellViews[x, y].gridCellState.skewersView;
        if (s == null || s.Length == 0) return false;
        for (int i = 0; i < s.Length; i++) if (s[i] == null) return false;
        int id = s[0].skewerData.idSkewer;
        for (int i = 1; i < s.Length; i++) if (s[i].skewerData.idSkewer != id) return false;
        return true;
    }

}
