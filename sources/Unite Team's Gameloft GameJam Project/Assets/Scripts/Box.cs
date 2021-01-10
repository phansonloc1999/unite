using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length)];
    }

    private void Update()
    {
    }
}