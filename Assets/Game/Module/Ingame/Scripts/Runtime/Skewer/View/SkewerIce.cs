using UnityEngine;

namespace Falcon.GrillSort.Ingame.Runtime
{
    public class SkewerIce : MonoBehaviour
    {
        public int countDestroy = 3;
        public SpriteRenderer SpriteRenderer;
        //---------------------------------------------------------
        private SkewerView skewer;
        private int keyRef;

        public void SetView(SkewerView skewerView, int key) 
        {
            countDestroy = 3;
            keyRef = key;
            skewer = skewerView;
            SpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

        public void AttackIce()
        {
            countDestroy--;
            switch (countDestroy)
            {
                case 0:
                    SpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
                    break;
                case 1:
                    SpriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
                    break;
                case 2:
                    SpriteRenderer.color = new Color(1f, 1f, 1f, 0.6f);
                    break;
            }
            if (countDestroy <= 0)
            {
                Debug.LogError("Remove Test");
                GridController.Instance._dicLockSkewerIce.Remove(keyRef);
                skewer.skewerType = SkewerType.Empty;
                gameObject.SetActive(false);
            }
        }
    }
}
