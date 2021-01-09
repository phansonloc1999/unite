using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _boxPrefab;

    [SerializeField] private float _eachHorizontalSwingDuration;

    [SerializeField] private GameObject _newBox = null;

    [SerializeField] private float _dropAndSpawnBoxInterval;

    private Sequence _newBoxMoveSequence;

    private Vector3 _cameraLeftEdgeWorldPos;

    private Vector3 _cameraRightEdgeWorldPos;

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
        _cameraLeftEdgeWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        _cameraRightEdgeWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        // Start x position is at camera's right edge world pos x
        transform.position = new Vector3(_cameraRightEdgeWorldPos.x, transform.position.y, 0);
    }

    private void DropNewBox()
    {
        _newBoxMoveSequence.Kill();
        // Reapply gravity scale to new box
        _newBox.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public void SpawnNewBox()
    {
        _newBox = Instantiate(_boxPrefab, transform.position, Quaternion.identity);
    }

    public void BackForthHorizontalSwing()
    {
        _newBoxMoveSequence = DOTween.Sequence()
        .Append(_newBox.transform.DOMoveX(_cameraLeftEdgeWorldPos.x, _eachHorizontalSwingDuration).SetEase(Ease.Linear))
        .Append(_newBox.transform.DOMoveX(_cameraRightEdgeWorldPos.x, _eachHorizontalSwingDuration).SetEase(Ease.Linear))
        .SetLoops(-1);
    }
}
