using UnityEngine;

public class Freeze : MonoBehaviour
{
    private static GameObject _mainCam = null;

    private static float? _mainCamHalfHeight = null;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        if (_mainCam == null) _mainCam = GameObject.Find("Main Camera");
        if (_mainCamHalfHeight == null) _mainCamHalfHeight = _mainCam.GetComponent<Camera>().orthographicSize;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y < _mainCam.transform.position.y - _mainCamHalfHeight - 1.0f)
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(this);
        }
    }
}