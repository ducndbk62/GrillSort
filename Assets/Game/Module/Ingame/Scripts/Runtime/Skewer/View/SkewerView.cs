using UnityEngine;

public class SkewerView : MonoBehaviour
{
    public int x, y;
    public SkewerData skewerData;
    public BoxCollider2D boxCollider;
    public DraggableObject draggableObject;
    public SpriteRenderer spriteRendererIcon;

    public void SetData(int x, int y, SkewerData data)
    {
        skewerData = data;
        this.x = x;
        this.y = y;
        GridController.Instance?.RegisterSkewerView(this);
        SetView();
    }

    public void SetView()
    {
        var obj = GridController.Instance.viewBinder.CreateUISkewer(skewerData.idSkewer, transform);
        spriteRendererIcon = obj.GetComponentInChildren<SpriteRenderer>();
    }

    public void StartMove()
    {
        spriteRendererIcon.sortingOrder = 100;
        transform.localScale = Vector3.one * 1.2f;
    }

    public void EndMove()
    {
        spriteRendererIcon.sortingOrder = 10;
        transform.localScale = Vector3.one * 1f;
    }

    public void RemoveFromCurrentCell()
    {
        if (x < 0 || y < 0 || skewerData.indexSlot < 0) return;
        GridController.Instance?.ClearSkewerAt(x, y, skewerData.indexSlot);
        transform.SetParent(null);
        x = -1;
        y = -1;
        skewerData.indexSlot = -1;
    }

    private void OnDisable() 
    { 
        GridController.Instance?.UnregisterSkewerView(this);
    }
}
