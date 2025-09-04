using UnityEngine;
using DG.Tweening;
using Falcon.GrillSort.Ingame.Runtime;

public class SkewerView : MonoBehaviour
{
    public SkewerType skewerType;
    public int x, y;
    public SkewerData skewerData;
    public BoxCollider2D boxCollider;
    public DraggableObject draggableObject;
    [HideInInspector] public SpriteRenderer spriteRendererIcon;
    // ------ References ------
    [HideInInspector] public SkewerIce scrSkewerIce;

    public void SetData(int x, int y, SkewerData data)
    {
        this.x = x; this.y = y;
        skewerData = data;
        skewerType = (SkewerType)skewerData.typeSkewer;
        SetView();
    }

    public void SetView()
    {
        var obj = GridController.Instance.viewBinder.CreateUISkewer(skewerData.idSkewer, new Vector3(0, 0.2f, 0), new Vector3(0.9f, 0.9f, 1f),  transform);
        spriteRendererIcon = obj.GetComponentInChildren<SpriteRenderer>();
        if(skewerType == SkewerType.Ice)
        {
            Debug.LogError("Ice");
            var go = GridController.Instance.viewBinder.CreateSkewerIce(0.2f, transform);
            scrSkewerIce = go.GetComponent<SkewerIce>();
            int key = go.GetInstanceID();
            GridController.Instance._dicLockSkewerIce[key] = scrSkewerIce;
            scrSkewerIce.SetView(this, key);
        }
    }

    public void StartMove()
    {
        spriteRendererIcon.sortingLayerName = "Top";
        transform.localScale = Vector3.one * 1.2f;
    }

    public void EndMove()
    {
        spriteRendererIcon.sortingLayerName = "Skewer";
        transform.localScale = Vector3.one * 1f;
    }

    public void ClearSkewer(int index, Transform trans)
    {
        spriteRendererIcon.sortingLayerName = "Top";
        spriteRendererIcon.sortingOrder = 5;
        boxCollider.enabled = false;
        gameObject.transform.SetParent(trans);
        gameObject.transform.DOLocalMove(GridUtils.SLOT_PLATES_CLEAR[index], 0.15f);
        gameObject.transform.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.15f);
    }
    public bool GetStatusLock()
    {
        if(skewerType == SkewerType.Ice)
            return true;
        return false;
    }
}
