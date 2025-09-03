using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridCellView : MonoBehaviour
{
    public int x, y;
    public GridCellState gridCellState;
    private List<GameObject> listObjPlate = new();
    public SkewerView[] skewersViewPlate = new SkewerView[GridConstants.MaxSkewerSlots];

    public void SetData(int x, int y, GridCellData data)
    {
        this.x = x;
        this.y = y;
        gridCellState.gridCellData = data;
        gridCellState.SetTypeFirstGame();
        SetFirstSkewerToTray();
    }

    private void SetFirstSkewerToTray()
    {
        var data = gridCellState.gridCellData;
        if (data.listLayerSkewer.Count == 0) return;
        foreach (var skewerData in data.listLayerSkewer[0].listSkewerData)
        {
            int index = skewerData.indexSlot;
            if (index >= 0 && index < GridConstants.MaxSkewerSlots)
            {
                Vector3 localPos = GridUtils.SLOT_POSITIONS[index];
                gridCellState.skewersView[index] =
                    GridController.Instance.viewBinder.CreateSkewer(x, y, skewerData, transform, localPos);
            }
        }
        data.listLayerSkewer.RemoveAt(0);
        if (data.listLayerSkewer.Count > 0)
        {
            float basePosY = -1.75f;
            float offsetY = data.listLayerSkewer.Count > 5 ? 0.05f : 0.1f;
            for (int i = 0; i < data.listLayerSkewer.Count; i++)
            {
                GameObject plate = GridController.Instance.viewBinder.CreatePlate(basePosY, transform);
                plate.GetComponentInChildren<SpriteRenderer>().sortingOrder = i;
                listObjPlate.Add(plate);
                basePosY += offsetY;
            }
        }
        SetCacheLayerNext();
    }

    private float timeAction = 0.2f;

    public void NextLayer()
    {
        if (!AllSkewerSlotsEmpty(gridCellState.skewersView) || !HasNextLayer()) return;
        for (int i = 0; i < GridConstants.MaxSkewerSlots; i++)
        {
            gridCellState.skewersView[i] = skewersViewPlate[i];
            skewersViewPlate[i] = null;
            if (gridCellState.skewersView[i] != null)
            {
                var skewer = gridCellState.skewersView[i];
                skewer.transform.SetParent(transform);
                StartCoroutine(ActionMextLayer(skewer, skewer.transform, GridUtils.SLOT_POSITIONS[skewer.skewerData.indexSlot]));
            }
        }
        var lastPlate = listObjPlate[^1];
        float timePlate = timeAction + 0.05f;
        DOVirtual.DelayedCall(timePlate, () =>
        {
            lastPlate.SetActive(false);
        });
        listObjPlate.RemoveAt(listObjPlate.Count - 1);
        SetCacheLayerNext();
    }

    private void SetCacheLayerNext()
    {
        var data = gridCellState.gridCellData;
        if (data.listLayerSkewer.Count == 0) return;
        GameObject lastPlate = listObjPlate[^1];
        foreach (var skewerData in data.listLayerSkewer[0].listSkewerData)
        {
            int index = skewerData.indexSlot;
            if (index >= 0 && index < GridConstants.MaxSkewerSlots)
            {
                Vector3 localPos = GridUtils.SLOT_PLATES[index];
                SkewerView view = GridController.Instance.viewBinder.CreateSkewer(x, y, skewerData, lastPlate.transform, localPos);
                Transform t = view.transform;
                t.localScale = Vector3.zero;
                t.eulerAngles = new Vector3(0, 0, -35);
                view.boxCollider.enabled = false;
                skewersViewPlate[index] = view;
            }
        }
        data.listLayerSkewer.RemoveAt(0);
        StartCoroutine(ScaleSkemerPlate());
    }

    private IEnumerator ScaleSkemerPlate()
    {
        float time = timeAction + 0.15f;
        yield return new WaitForSeconds(time);
        for (int i = 0; i < skewersViewPlate.Length; i++)
        {
            if (skewersViewPlate[i] != null)
            {
                var t = skewersViewPlate[i].transform;
                t.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.15f);
            }
        }
    }

    private IEnumerator ActionMextLayer(SkewerView view, Transform trans, Vector3 target)
    {
        trans.DOScale(Vector3.one, timeAction);
        trans.DOLocalMove(target, timeAction).SetEase(Ease.InBack);
        trans.DORotate(Vector3.zero, timeAction);
        yield return new WaitForSeconds(timeAction);
        view.boxCollider.enabled = true;
    }

    private bool AllSkewerSlotsEmpty(SkewerView[] views)
    {
        foreach (var view in views)
        {
            if (view != null)
            {
                Debug.LogWarning($"Not Complete Layer: {x}/{y}");
                return false;
            }
        }
        return true;
    }

    private bool HasNextLayer()
    {
        foreach (var view in skewersViewPlate)
        {
            if (view != null)
                return true;
        }
        return false;
    }

    public Vector3 GetSlotWorldPos(int index)
    {
        return transform.TransformPoint(GridUtils.SLOT_POSITIONS[index]);
    }

    public void AttachSkewerToSlot(int slot, SkewerView skewer)
    {
        if (skewer == null || slot < 0 || slot >= gridCellState.skewersView.Length) return;
        skewer.transform.SetParent(transform);
        //skewer.transform.localPosition = GridUtils.SLOT_POSITIONS[slot];
        skewer.transform.DOLocalMove(GridUtils.SLOT_POSITIONS[slot] , 0.15f).SetEase(Ease.OutBack);
        skewer.transform.localRotation = Quaternion.identity;
    }

    public void DetachSlot(int slot)
    {
        if (slot < 0 || slot >= gridCellState.skewersView.Length) return;
        var view = gridCellState.skewersView[slot];
        if (view == null) return;
        view.transform.SetParent(null);
        gridCellState.skewersView[slot] = null;
        Debug.Log($"Detached Skewer at slot {slot}");
        SetViewUI();
    }

    public void SetViewUI()
    {
        Debug.Log("SetViewUI called");
    }

  
}
