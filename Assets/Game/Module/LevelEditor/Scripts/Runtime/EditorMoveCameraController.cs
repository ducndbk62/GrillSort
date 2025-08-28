using UnityEngine;

public class EditorMoveCameraController : MonoBehaviour
{
    private float _moveSpeed = 10;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private Vector3 lastClickPos;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastClickPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            _camera.transform.position += lastClickPos - _camera.ScreenToWorldPoint(Input.mousePosition);
            lastClickPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }


        if (Input.GetKey(KeyCode.UpArrow))
            _camera.transform.position += Vector3.up * _moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.DownArrow))
            _camera.transform.position += Vector3.down * _moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.LeftArrow))
            _camera.transform.position += Vector3.left * _moveSpeed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.RightArrow))
            _camera.transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
    }
}
