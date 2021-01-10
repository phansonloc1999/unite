using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;

    [SerializeField] private float _eachHorizontalSwingDuration;

    [SerializeField] private GameObject _newBox = null;

    [SerializeField] private float _dropAndSpawnBoxInterval;

    [SerializeField] private float _updateCameraPosDuration;

    [SerializeField] private Camera _mainCamera;

    private Sequence _newBoxMoveSequence;

    private Vector3 _camLeftWorldPos;

    private Vector3 _camTopRightPos;

    private static float _camHeight;

    private static bool _canDropNewBox = true;

    private static float _offsetYfromTop = 1f;
    private static GameObject _dadsHand = null;

    // Start is called before the first frame update
    void Start()
    {
        Init();

        SpawnNewBox();
        RandomizeBoxMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canDropNewBox && !Pause._gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
            {
                IEnumerator MyCouroutine()
                {
                    _canDropNewBox = false;
                    DropNewBox();
                    yield return new WaitForSeconds(_dropAndSpawnBoxInterval);
                    UpdateCameraSpawnerPos();

                    yield return new WaitForSeconds(_updateCameraPosDuration);
                    GetCamLeftRightPos();
                    SpawnNewBox();
                    RandomizeBoxMovement();

                    _canDropNewBox = true;
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
        transform.position = new Vector3(_camTopRightPos.x, _camTopRightPos.y - _offsetYfromTop, 0);

        _camHeight = _mainCamera.orthographicSize * 2.0f;

        if (_dadsHand == null) _dadsHand = GameObject.Find("Dad'sHand");

        _canDropNewBox = true;
        Pause._gamePaused = false;
    }

    private void DropNewBox()
    {
        _newBox.GetComponent<CircularMovement>()?.Remove();
        _newBoxMoveSequence.Kill();
        _dadsHand.GetComponent<SpriteRenderer>().enabled = false;

        // Reapply gravity scale to new box
        _newBox.GetComponent<Rigidbody2D>().gravityScale = 1;
        Score.Increase();
        _dadsHand.transform.parent = transform.root;
    }

    private void SpawnNewBox()
    {
        _newBox = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], transform.position, Quaternion.identity);

        // Attach Dad's hand above
        _dadsHand.transform.parent = _newBox.transform;
        var dadSpriteRend = _dadsHand.GetComponent<SpriteRenderer>();
        _dadsHand.transform.localPosition = new Vector3(0, _newBox.GetComponent<SpriteRenderer>().bounds.size.y / 2 + dadSpriteRend.bounds.size.y / 2 - 0.5f, 0);
        _dadsHand.transform.rotation = Quaternion.identity;
        dadSpriteRend.enabled = true;
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
        // Move camera
        _mainCamera.transform.DOMove(new Vector3(_mainCamera.transform.position.x, _newBox.transform.position.y + _camHeight / 5.0f, _mainCamera.transform.position.z), _updateCameraPosDuration)
        .OnComplete(() =>
        {
            GetCamLeftRightPos();
            // Move spawner game object
            transform.position = new Vector3(_camTopRightPos.x, _camTopRightPos.y - _offsetYfromTop, transform.position.z);
        });
    }

    private void RandomizeBoxMovement()
    {
        var randResult = Random.Range(0, 2);
        if (randResult == 0)
        {
            BackForthHorizontalSwing();
        }
        else
        {
            BoxCircularMovement();
        }
    }

    private void BoxCircularMovement()
    {
        var camTopCenterPos = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
        _newBox.transform.position = new Vector3(camTopCenterPos.x, camTopCenterPos.y - _offsetYfromTop, transform.position.z);
        _newBox.AddComponent<CircularMovement>();
    }

    private void OnDestroy()
    {
        _newBoxMoveSequence.Kill();
    }
}
