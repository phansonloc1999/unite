﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 2f;
    [SerializeField] private float _radius = 1f;

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

    public void Remove()
    {
        Destroy(this);
    }
}