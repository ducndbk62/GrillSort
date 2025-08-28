using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class DucTest : MonoBehaviour
{
    [Button]
    private void ChangeText()
    {
        var listTMP = FindObjectsByType<Text>(FindObjectsSortMode.None);
        foreach (var t in listTMP)
        {
            t.alignment = TextAnchor.MiddleCenter;
        }
    }
}
