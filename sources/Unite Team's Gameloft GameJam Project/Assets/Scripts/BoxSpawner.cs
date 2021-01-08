using DG.Tweening;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _boxPrefab;

    [SerializeField] private float _eachHorizontalSwingDuration;

    private Vector3 _cameraLeftEdgeWorldPos;
    private Vector3 _cameraRightEdgeWorldPos;

    // Start is called before the first frame update
    void Start()
    {
        Init();

        var newBox = SpawnBox();
        BackForthHorizontalSwing(newBox);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        _cameraLeftEdgeWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        _cameraRightEdgeWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        // Start x position is at camera's right edge world pos x
        transform.position = new Vector3(_cameraRightEdgeWorldPos.x, transform.position.y, 0);
    }

    public GameObject SpawnBox()
    {
        return Instantiate(_boxPrefab, transform.position, Quaternion.identity);
    }

    public void BackForthHorizontalSwing(GameObject newBox)
    {
        var swingSeq = DOTween.Sequence()
        .Append(newBox.transform.DOMoveX(_cameraLeftEdgeWorldPos.x, _eachHorizontalSwingDuration).SetEase(Ease.Linear))
        .Append(newBox.transform.DOMoveX(_cameraRightEdgeWorldPos.x, _eachHorizontalSwingDuration).SetEase(Ease.Linear))
        .SetLoops(-1);
    }
}
