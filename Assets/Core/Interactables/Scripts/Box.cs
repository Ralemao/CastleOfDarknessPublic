using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private bool _isBeenPush;
    private bool _isGrounded;
    private bool _isTrigged;

    public bool GetGrounded()
    {
        return _isGrounded;
    }

    [SerializeField]
    private Transform _interactionButton;

    public bool IsPushing()
    {
        return _isBeenPush;
    }

    private Rigidbody2D _rb2D;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PushMovement();
    }

    private void PushMovement()
    {
        if (!_isBeenPush)
        {
            if (_isGrounded)
                _rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            else
                GetComponent<FixedJoint2D>().enabled = false;
        }
        else
        {
            _rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void SetPush(bool value)
    {
        _isBeenPush = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                _isGrounded = true;
                break;

            case "Box":
                _isGrounded = true;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Box":
                _isGrounded = false;
                SetPush(false);
                break;

            case "Player":
                SetPush(false);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                _rb2D.isKinematic = true;
                _isTrigged = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                _rb2D.isKinematic = false;
                _isTrigged = false;
                break;
        }
    }
}
