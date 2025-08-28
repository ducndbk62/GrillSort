using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public sealed class GridModel
{
    public int Width { get; }
    public int Height { get; }
    public GridCellState[,] Cells { get; }
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

    public GridModel(int w, int h, List<GridCellData> gridCellData)
    {
        Width = w; Height = h;
        Cells = new GridCellState[w, h];
        CellViews = new GridCellView[w, h];
        var dicMap = gridCellData.ToDictionary(c => new Vector2Int(c.x, c.y));
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
            {
                var state = new GridCellState();
                if (dicMap.TryGetValue(new Vector2Int(x, y), out var data))
                {
                    state.gridCellData = data;
                }
                Cells[x, y] = state;
            }
    }

    public bool InBounds(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
    public GridCellState Get(int x, int y) => InBounds(x, y) ? Cells[x, y] : null;

    public bool TrySnap(SkewerView skewerView, int toX, int toY, int toSlot)
    {
        int fx = skewerView.x, fy = skewerView.y, fs = skewerView.skewerData.indexSlot;
        if (!InBounds(toX, toY)) return false;
        var cell = Cells[toX, toY];
        if (cell.objectType == GridObjectType.Empty) return false;
        if (toSlot < 0 || toSlot >= GridConstants.MaxSkewerSlots) return false;
        if (cell.skewersView[toSlot] != null) return false;
        if (skewerView.x == toX && skewerView.y == toY && skewerView.skewerData.indexSlot == toSlot) return true;
        if (skewerView.x >= 0 && InBounds(skewerView.x, skewerView.y))
        {
            var oldCell = Cells[skewerView.x, skewerView.y];
            if (skewerView.skewerData.indexSlot >= 0 && skewerView.skewerData.indexSlot < oldCell.skewersView.Length)
            {
                oldCell.skewersView[skewerView.skewerData.indexSlot] = null;
            }
        }
        skewerView.x = toX; skewerView.y = toY; skewerView.skewerData.indexSlot = toSlot;
        cell.skewersView[toSlot] = skewerView;
        OnSkewerMoved?.Invoke(new SkewerMoved(skewerView, fx, fy, fs, toX, toY, toSlot));
        if (IsCellComplete(toX, toY))
        {
            OnCellCompleted?.Invoke(new CellCompleted(toX, toY));
            ClearCellData(toX, toY);
        }
        return true;
    }


    public bool IsCellComplete(int x, int y)
    {
        if (!InBounds(x, y)) return false;
        var s = Cells[x, y].skewersView;
        if (s == null || s.Length == 0) return false;
        for (int i = 0; i < s.Length; i++) if (s[i] == null) return false;
        string id = s[0].skewerData.idSkewer;
        for (int i = 1; i < s.Length; i++) if (s[i].skewerData.idSkewer != id) return false;
        return true;
    }

    private void ClearCellData(int x, int y)
    {
        var s = Cells[x, y].skewersView;
        for (int i = 0; i < s.Length; i++) s[i] = null;
    }
}
