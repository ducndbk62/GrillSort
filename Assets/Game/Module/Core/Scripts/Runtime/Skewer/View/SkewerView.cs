using UnityEngine;

public class SkewerView : MonoBehaviour
{
    public int x, y;
    public SkewerData skewerData;
    public void SetData(SkewerData data)
    {
        skewerData = data;
        GridController.Instance?.RegisterSkewerView(this);
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
