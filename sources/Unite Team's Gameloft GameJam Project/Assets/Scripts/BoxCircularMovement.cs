using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCircularMovement : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 5f;
    [SerializeField] private float _radius = 0.1f;

    private Vector2 _centre;
    private float _angle;

    private void Start()
    {
        _centre = transform.position;
    }

    private void Update()
    {
        _angle += _rotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * _radius;
        transform.position = _centre + offset;
    }
}
