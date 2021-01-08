using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _boxPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBox();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBox()
    {
        Instantiate(_boxPrefab, transform.position, Quaternion.identity);
    }

    public void MoveHorizontally()
    {

    }
}
