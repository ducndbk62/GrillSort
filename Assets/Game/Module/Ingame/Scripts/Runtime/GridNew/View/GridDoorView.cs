using DG.Tweening;
using UnityEngine;
using System.Collections;

namespace Falcon.GrillSort.Ingame.Runtime
{
    public class GridDoorView : MonoBehaviour
    {
        public Transform transPos;
        private float timeAction = 0.35f;

        public void SetView(int id)
        {
            var obj = GridController.Instance.viewBinder.CreateUISkewer(id, new Vector3(0, -0.1f, 0f), new Vector3(0.4f, 0.4f, 1f), transPos);
            SpriteRenderer sprItem = obj.GetComponentInChildren<SpriteRenderer>();
            sprItem.sortingLayerName = "Door";
            sprItem.sortingOrder = 15;
            obj.transform.localRotation = Quaternion.identity;
        }

        public void HideDoor()
        {
            SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
            transform.DOLocalMoveY(1, timeAction).SetEase(Ease.OutBack);
            for (int i = 0; i < renderers.Length; i++)
            {
                StartCoroutine(FadeOut(renderers[i], timeAction));
            }
            DOVirtual.DelayedCall(timeAction, () =>
            {
                gameObject.SetActive(false);
            });
        }

        public IEnumerator FadeOut(SpriteRenderer renderer, float duration)
        {
            Color startColor = renderer.color;
            float startAlpha = startColor.a;
            float time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, 0f, time / duration);
                renderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }
            renderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        }

    }
}
