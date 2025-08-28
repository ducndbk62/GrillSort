using UnityEngine;

public class EditorZoomCameraController : MonoBehaviour
{
    private float _maxFOV = 90;
    private float _minFOV = 5;
    private float _sensitivity = 10f;

    private void Update()
    {
        var fov = Camera.main.orthographicSize;
        fov -= Input.GetAxis("Mouse ScrollWheel") * _sensitivity;
        fov = Mathf.Clamp(fov, _minFOV, _maxFOV);
        Camera.main.orthographicSize = fov;
    }
}
