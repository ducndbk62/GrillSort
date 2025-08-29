using System.Diagnostics;
using UnityEngine;

public class GridCellView : MonoBehaviour
{
    public int x, y;

    public SpriteRenderer spriteRenderer;

    public GridCellState gridCellState;

    private SkewerView[] arrrSkewerView = new SkewerView[GridConstants.MaxSkewerSlots];

    public void SetData(int x, int y, GridCellData data)
    {
        this.x = x;
        this.y = y;
        gridCellState.gridCellData = data;
        spriteRenderer.enabled = gridCellState.objectType == GridObjectType.BakingTray;
        SetViewUI();
    }

    public void SetViewUI()
    {
        UnityEngine.Debug.Log("UpdateSnapshot");

    }

    public void ClearUI()
    {
        for (int i = 0; i < arrrSkewerView.Length; i++)
            DetachSlot(i);
    }

    public Vector3 GetSlotWorldPos(int i) => transform.TransformPoint(GridUtils.SLOT_POSITIONS[i]);

    public void AttachSkewerToSlot(int slot, SkewerView skewer)
    {
        if (skewer == null) return;
        if (slot < 0 || slot >= arrrSkewerView.Length) return;
        if (arrrSkewerView[slot] != null && arrrSkewerView[slot] != skewer)
            DetachSlot(slot);
        arrrSkewerView[slot] = skewer;
        skewer.transform.SetParent(transform);
        skewer.transform.localPosition = GridUtils.SLOT_POSITIONS[slot];
        skewer.transform.localRotation = Quaternion.identity;
    }

    public void DetachSlot(int slot)
    {
        if (slot < 0 || slot >= arrrSkewerView.Length) return;
        var v = arrrSkewerView[slot];
        if (v == null) return;
        v.transform.SetParent(null);
        arrrSkewerView[slot] = null;
    }

    public void PlayCompleteFX()
    {
        // TODO: particle/tween/flash sprite...
    }
}
