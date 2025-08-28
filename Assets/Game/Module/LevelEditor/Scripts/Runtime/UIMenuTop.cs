using UnityEngine;
using UnityEngine.UI;

namespace Falcon.GrillSort.LevelEditor.Runtime
{
    public class UIMenuTop : MonoBehaviour
    {
        [SerializeField] Text textCurrentLayer;
        [SerializeField] Button btnNextLayer, btnPrevLayer;
        [SerializeField] InputField inputFieldTime;

        private int currentLayer; // Layer 1 bắt đầu đến từ 0 để lấy index trong list
        private int levelTime;

        private void Awake()
        {
            EditorEvents.Instance.OnChangeLayerBoard += UpdateTextLayer;

            btnNextLayer.onClick.AddListener(ClickNextLayer);
            btnPrevLayer.onClick.AddListener(CLickPrevLayer);
            inputFieldTime.onEndEdit.AddListener(SetTime);
        }

        private void Start()
        {
            UpdateTextLayer(0);
        }

        private void OnDestroy()
        {
            EditorEvents.Instance.OnChangeLayerBoard -= UpdateTextLayer;
        }

        private void SetTime(string t)
        {
            levelTime = int.Parse(t);
        }

        private void UpdateTextLayer(int layer)
        {
            textCurrentLayer.text = (layer + 1).ToString();
        }

        private void ClickNextLayer()
        {
            currentLayer++;
            EditorEvents.Instance.OnChangeLayerBoard?.Invoke(currentLayer);
        }

        private void CLickPrevLayer()
        {
            if (currentLayer == 0) return;
            currentLayer--;
            EditorEvents.Instance.OnChangeLayerBoard?.Invoke(currentLayer);
        }
    }
}
