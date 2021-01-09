using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _boxPrefab;

    [SerializeField] private float _eachHorizontalSwingDuration;

    [SerializeField] private GameObject _newBox = null;

    [SerializeField] private float _dropAndSpawnBoxInterval;

    [SerializeField] private Camera _mainCamera;

    private Sequence _newBoxMoveSequence;

    private Vector3 _camLeftWorldPos;

    private Vector3 _camTopRightPos;

    private static float _camHeight;

    // Start is called before the first frame update
    void Start()
    {
        Init();

        SpawnNewBox();
        BackForthHorizontalSwing();
    }

    // Update is called once per frame
    void Update()
    {
        if (_newBox != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IEnumerator MyCouroutine()
                {
                    DropNewBox();
                    yield return new WaitForSeconds(_dropAndSpawnBoxInterval);
                    UpdateCameraSpawnerPos();
                    GetCamLeftRightPos();
                    SpawnNewBox();
                    BackForthHorizontalSwing();
                    yield break;
                }
                StartCoroutine(MyCouroutine());
            }
        }
    }

    private void Init()
    {
        GetCamLeftRightPos();

        // Start position is at camera's top right edge world pos
        transform.position = new Vector3(_camTopRightPos.x, _camTopRightPos.y, 0);

        _camHeight = _mainCamera.orthographicSize * 2.0f;
    }

    private void DropNewBox()
    {
        _newBoxMoveSequence.Kill();
        // Reapply gravity scale to new box
        _newBox.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void SpawnNewBox()
    {
        _newBox = Instantiate(_boxPrefab, transform.position, Quaternion.identity);
    }

    private void BackForthHorizontalSwing()
    {
        _newBoxMoveSequence = DOTween.Sequence()
        .Append(_newBox.transform.DOMoveX(_camLeftWorldPos.x, _eachHorizontalSwingDuration).SetEase(Ease.Linear))
        .Append(_newBox.transform.DOMoveX(_camTopRightPos.x, _eachHorizontalSwingDuration).SetEase(Ease.Linear))
        .SetLoops(-1);
    }

    private void GetCamLeftRightPos()
    {
        _camLeftWorldPos = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        _camTopRightPos = _mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
    }

    private void UpdateCameraSpawnerPos()
    {
        _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x, _newBox.transform.position.y + _camHeight / 8.0f, _mainCamera.transform.position.z);
        var camTopCenterPos = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
        transform.position = new Vector3(camTopCenterPos.x, camTopCenterPos.y, transform.position.z);
    }
}
