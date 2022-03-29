using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] 
    private int _target;
    [SerializeField] 
    private float _speed;

    [SerializeField] 
    private List<Transform> wayPoints;

    void Update()
    {
        SetTarget();
        SetMovement();
    }

    private void SetTarget()
    {
        if (transform.position == wayPoints[_target].position)
        {
            if (_target == wayPoints.Count - 1)
                _target = 0;
            else
                _target++;
        }
    }

    private void SetMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[_target].position, _speed * Time.deltaTime);
    }
}
