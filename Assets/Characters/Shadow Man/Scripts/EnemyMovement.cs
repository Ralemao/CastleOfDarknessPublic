using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float _speed;
    private bool _isRight;
    private bool _hasTarget;
    private bool _isTrigger;

    public float GetSpeed()
    {
        return _speed;
    }

    public void SetSpeed(float value)
    {
        _speed = value;
    }

    public void IsTarget(bool value)
    {
        _hasTarget = value;
    }

    public void SetTrigger(bool value)
    {
        _isTrigger = value;
    }

    private CapsuleCollider2D _collider;
    private Transform _player;

    private static EnemyMovement _instance;
    public static EnemyMovement Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<EnemyMovement>();

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _isRight = true;
        _collider = GetComponent<CapsuleCollider2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        SetFaceSide();
        SetMovement();
    }

    private void SetFaceSide()
    {
        if (_isRight && _player.position.x < transform.position.x)
            Flip();
        else if (!_isRight && _player.position.x > transform.position.x)
            Flip();
    }

    private void SetMovement()
    {
        _collider.isTrigger = _isTrigger;

        if (_hasTarget)
            transform.position = Vector3.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);

        EnemyAnimation.Instance.SpeedAnim();
    }

    private void Flip()
    {
        _isRight = !_isRight;

        if (_isRight)
            transform.localScale = new Vector2(1, 1);
        else
            transform.localScale = new Vector2(-1, 1);
    }
}
