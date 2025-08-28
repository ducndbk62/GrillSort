using UnityEngine;

[RequireComponent(typeof(SkewerView))]
public class DraggableObject : MonoBehaviour
{
    private bool _isDragging;
    private Vector3 _offset;
    private Vector3 _originalPosition;
    private Transform _originalParent;

    private void Update()
    {
        if (!_isDragging) return;
        var cam = Camera.main; if (cam == null) return;

        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPos = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z) + _offset;
        transform.position = newPos;
    }

    private void OnMouseDown()
    {
        var cam = Camera.main; if (cam == null) return;
        _isDragging = true;
        _originalPosition = transform.position;
        _originalParent = transform.parent;
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        _offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);
        transform.SetParent(null);
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        var skewer = GetComponent<SkewerView>();
        if (skewer == null)
        {
            ResetToOriginal();
            return;
        }
        Vector3 dropWorldPos = transform.position;
        Vector2Int gridPos = GridUtils.WorldToGrid(dropWorldPos);
        Debug.LogError("Check X : " + gridPos.x);
        Debug.LogError("Check Y : " + gridPos.y);
        var gc = GridController.Instance;
        if (gc != null && gc.InBounds(gridPos.x, gridPos.y) && gc.TrySnapSkewer(gridPos.x, gridPos.y, skewer, dropWorldPos))
        {
            Debug.LogError("Ads Success");
            return;
        }
        ResetToOriginal();
    }

    private void ResetToOriginal()
    {
        transform.SetParent(_originalParent);
        transform.position = _originalPosition;
    }
}
