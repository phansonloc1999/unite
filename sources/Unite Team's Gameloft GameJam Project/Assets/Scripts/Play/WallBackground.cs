using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBackground : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_mainCamera.transform.position.x, _mainCamera.transform.position.y, 0);
    }
}
