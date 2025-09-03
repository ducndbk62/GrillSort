using DG.Tweening;
using UnityEditor.Graphs;
using UnityEngine;

[RequireComponent(typeof(SkewerView))]
public class DraggableObject : MonoBehaviour
{
    // ------ Static --------
    public static DraggableObject ActiveDragged { get; private set; }

    // ------ State ------
    private bool isDragging = false;
    private bool hasMovedSinceMouseDown = false;
    private bool wasAlreadySelected = false;

    // ------ References ------
    private SkewerView skewer;

    // ------ Positioning ------
    private Vector3 dragOffset;
    private Vector3 originalPosition;
    private Transform originalParent;
    private Vector3 mouseDownScreenPosition;

    private void Awake()
    {
        skewer = GetComponent<SkewerView>();
    }

    private void Update()
    {
        var cam = Camera.main;
        if (cam == null) return;

        HandleClickSnap(cam);
        HandleDragging(cam);
    }

    private void HandleClickSnap(Camera cam)
    {
        if (Input.GetMouseButtonDown(0) && ActiveDragged == this && !hasMovedSinceMouseDown)
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            Vector2Int gridPos = GridUtils.WorldToGrid(mouseWorldPos);

            var gridController = GridController.Instance;
            if (gridController != null &&
                gridController.InBounds(gridPos.x, gridPos.y) &&
                gridController.TrySnapSkewer(gridPos.x, gridPos.y, skewer, mouseWorldPos))
            {
                Debug.Log("[DraggableObject] Snap on Click");
                ActiveDragged = null;
                skewer.EndMove();
            }
        }
    }

    private void HandleDragging(Camera cam)
    {
        if (!isDragging) return;
        if (!hasMovedSinceMouseDown && Input.mousePosition != mouseDownScreenPosition)
        {
            hasMovedSinceMouseDown = true;
        }
        if (hasMovedSinceMouseDown)
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z) + dragOffset;
            transform.position = newPos;
        }
    }

    private void OnMouseDown()
    {
        var cam = Camera.main;
        if (cam == null) return;
        originalPosition = transform.position;
        originalParent = transform.parent;
        mouseDownScreenPosition = Input.mousePosition;
        hasMovedSinceMouseDown = false;
        wasAlreadySelected = (ActiveDragged == this);
        if (ActiveDragged != null && ActiveDragged != this)
        {
            ActiveDragged.ResetToOriginal();
            ActiveDragged.skewer.EndMove();
        }
        ActiveDragged = this;
        isDragging = true;
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        dragOffset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
        skewer.StartMove();
        transform.SetParent(null);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (!hasMovedSinceMouseDown)
        {
            if (wasAlreadySelected)
            {
                Debug.Log("[DraggableObject] Deselect on click");
                ActiveDragged = null;
                ResetToOriginal();
                skewer.EndMove();
            }
            return;
        }
        skewer.EndMove();
        ActiveDragged = null;
        Vector3 dropWorldPos = transform.position;
        Vector2Int gridPos = GridUtils.WorldToGrid(dropWorldPos);
        if (gridPos.x < 0)
        {
            gridPos = GridUtils.WorldToGridNearest(dropWorldPos);
        }
        var gridController = GridController.Instance;
        if (gridController != null &&
            gridController.InBounds(gridPos.x, gridPos.y) &&
            gridController.TrySnapSkewer(gridPos.x, gridPos.y, skewer, dropWorldPos))
        {
            return;
        }
        ResetToOriginal();
    }

    private void ResetToOriginal()
    {
        transform.SetParent(originalParent);
        //transform.position = originalPosition;
        transform.DOMove(originalPosition, 0.15f).SetEase(Ease.OutBack);
    }
}
