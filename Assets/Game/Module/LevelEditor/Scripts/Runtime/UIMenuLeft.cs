using UnityEngine;
using UnityEngine.UI;

namespace Falcon.GrillSort.LevelEditor.Runtime
{
    public class UIMenuLeft : MonoBehaviour
    {
        [SerializeField] Button btnCell, btnItem;
        [SerializeField] GameObject viewCell, viewItem;

        private void Awake()
        {
            btnCell.onClick.AddListener(OnClickBtnCell);
            btnItem.onClick.AddListener(OnClickBtnItem);

            EditorEvents.Instance.OnSelectTypeCell += ShowListCell;
            EditorEvents.Instance.OnSelectTypeItem += ShowListItem;
        }

        private void OnClickBtnCell()
        {
            EditorEvents.Instance.OnSelectTypeCell?.Invoke();
        }

        private void OnClickBtnItem()
        {
            EditorEvents.Instance.OnSelectTypeItem?.Invoke();
        }

        private void ShowListCell()
        {
            viewCell.SetActive(true);
            viewItem.SetActive(false);
        }

        private void ShowListItem()
        {
            viewCell.SetActive(false);
            viewItem.SetActive(true);
        }
    }
}
