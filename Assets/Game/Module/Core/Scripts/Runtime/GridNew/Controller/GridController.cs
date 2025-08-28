using System;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController Instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject gridCellPrefab;

    [Header("Grid Settings")]
    private float offsetY = 0.5f; // Camera

    public GridModel Model { get; private set; }
    public Vector3 Origin { get; private set; }

    private readonly Dictionary<Vector2Int, SkewerView> _dicView = new();
    public void RegisterSkewerView(SkewerView v)
    {
        if (v != null && v.skewerData != null) _dicView[new Vector2Int(v.x, v.y)] = v;
    }
    public void UnregisterSkewerView(SkewerView v)
    {
        if (v != null && v.skewerData != null) 
        {
            var key = new Vector2Int(v.x, v.y);
            _dicView.Remove(key);
        }
    }


    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void InitGrid(LevelData data)
    {
        Model = new GridModel(data.width, data.height, data.gridCellData);
        float totalWidth = data.width * (GridUtils.CELL_WIDTH + GridUtils.SPACING_X) - GridUtils.SPACING_X;
        float totalHeight = data.height * (GridUtils.CELL_HEIGHT + GridUtils.SPACING_Y) - GridUtils.SPACING_Y;
        Origin = new Vector3(-totalWidth / 2f, -totalHeight / 2f, 0f);
        for (int x = 0; x < data.width; x++)
            for (int y = 0; y < data.height; y++)
            {
                Vector3 worldPos = GridUtils.GridToWorld(x, y);
                var go = Instantiate(gridCellPrefab, worldPos, Quaternion.identity, transform);
                var view = go.GetComponent<GridCellView>();
                Model.CellViews[x, y] = view;
                view.SetData(Model.Cells[x, y]);
            }
        Model.OnSkewerMoved += OnSkewerMoved;
        Model.OnCellCompleted += OnCellCompleted;
        CenterCamera();
    }

    private void OnSkewerMoved(GridModel.SkewerMoved e)
    {
        var key = new Vector2Int(e.FromX, e.FromY);
        if (!_dicView.TryGetValue(key, out var view)) return;
        if (Model.InBounds(e.FromX, e.FromY) && e.FromSlot >= 0)
            Model.CellViews[e.FromX, e.FromY].DetachSlot(e.FromSlot);
        var toCell = Model.CellViews[e.ToX, e.ToY];
        toCell.AttachSkewerToSlot(e.ToSlot, view);
        GetCellView(e.FromX, e.FromY)?.SetViewUI();
    }

    private void OnCellCompleted(GridModel.CellCompleted e)
    {
        var view = Model.CellViews[e.X, e.Y];
        SkewerView[] skewers = GetCellView(e.X, e.Y).GetComponentsInChildren<SkewerView>();
        foreach (SkewerView scr in skewers)
        {
            scr.gameObject.SetActive(false);
        }
        Debug.LogError("Cell Completed at: " + skewers.Length);
    }

    public bool InBounds(int x, int y) => Model != null && Model.InBounds(x, y);
    public GridCellState GetCellData(int x, int y) => Model?.Get(x, y);
    public GridCellView GetCellView(int x, int y) => InBounds(x, y) ? Model.CellViews[x, y] : null;

    public void ClearCell(int x, int y)
    {
        if (!InBounds(x, y)) return;
        var data = Model.Cells[x, y];
        data.Clear();
        //_views[x, y].Bind(data);
    }

    public void SetObject(int x, int y, GridObjectType type, string color = null)
    {
        if (!InBounds(x, y)) return;
        var data = Model.Cells[x, y];
        data.objectType = type;
        //_views[x, y].Bind(data);
    }

    private void CenterCamera()
    {
        var cam = Camera.main; if (cam == null) return;
        float cellW = GridUtils.CELL_WIDTH + GridUtils.SPACING_X;
        float cellH = GridUtils.CELL_HEIGHT + GridUtils.SPACING_Y;
        float centerX = Origin.x + ((Model.Width - 1) * cellW + GridUtils.CELL_WIDTH) / 2f;
        float centerY = Origin.y + ((Model.Height - 1) * cellH + GridUtils.CELL_HEIGHT) / 2f;
        cam.transform.position = new Vector3(centerX, centerY + offsetY, cam.transform.position.z);
    }

    private void OnDrawGizmos()
    {
        if (Model == null) return;
        Gizmos.color = Color.red;
        for (int x = 0; x < Model.Width; x++)
            for (int y = 0; y < Model.Height; y++)
            {
                Vector3 pos = GridUtils.GridToWorld(x, y);
                Gizmos.DrawWireCube(pos, new Vector3(GridUtils.CELL_WIDTH, GridUtils.CELL_HEIGHT, 1f));
            }
    }

    private float INTRA_SNAP_RADIUS => 0.35f * GridUtils.CELL_WIDTH;
    private float INTRA_SNAP_RADIUS_SQR => 0.35f * GridUtils.CELL_WIDTH * 0.35f * GridUtils.CELL_WIDTH;

    public bool TrySnapSkewer(int x, int y, SkewerView skewer, Vector3 worldPos)
    {
        if (!InBounds(x, y) || skewer == null || skewer.skewerData == null) return false;
        var cellView = Model.CellViews[x, y];
        bool SameCell(int i) => skewer.x == x && skewer.y == y && skewer.skewerData.indexSlot == i;
        int target = FindNearestSlot(cellView, worldPos, i =>
        {
            var cell = Model.Cells[x, y];
            var free = cell.skewersView[i] == null;
            return free || SameCell(i);
        });
        if (target == -1) return false;
        bool isSameCell = (skewer.x == x && skewer.y == y);
        //if (!isSameCell)
        //{
        //    Debug.LogError("Check Same Cell");
        //    Vector3 slotPos = cellView.GetSlotWorldPos(target);
        //    float d2 = (worldPos - slotPos).sqrMagnitude;
        //    if (d2 > INTRA_SNAP_RADIUS_SQR) return false;
        //}
        if (!Model.TrySnap(skewer, x, y, target)) return false;
        return true;
    }


    private int FindNearestSlot(GridCellView view, Vector3 worldPos, Predicate<int> canUseSlot)
    {
        if (view == null) return -1;
        int best = -1;
        float bestDistSqr = float.MaxValue;
        for (int i = 0; i < GridConstants.MaxSkewerSlots; i++)
        {
            if (!canUseSlot(i)) continue;
            Vector3 p = view.GetSlotWorldPos(i);
            float d2 = (worldPos - p).sqrMagnitude;
            if (d2 < bestDistSqr) { bestDistSqr = d2; best = i; }
        }
        return best;
    }

    public void ClearSkewerAt(int x, int y, int slot)
    {
        if (!InBounds(x, y)) return;
        if (slot < 0 || slot >= GridConstants.MaxSkewerSlots) return;
        var cell = Model.Cells[x, y];
        if (cell.skewersView != null) cell.skewersView[slot] = null;
    }
    private void OnDestroy()
    {
        if (Model != null)
        {
            Model.OnCellCompleted -= OnCellCompleted;
            Model.OnSkewerMoved -= OnSkewerMoved;
        }
    }
}
