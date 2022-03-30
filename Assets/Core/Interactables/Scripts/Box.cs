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
    private GameObject _interactionButton;

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
        this.PushMovement();
    }

    private void PushMovement()
    {
        if (!this._isBeenPush)
        {
            if (this._isGrounded)
                this._rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            else
                this.GetComponent<FixedJoint2D>().enabled = false;
        }
        else
        {
            this.InteractionButton(false);
            this._rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void InteractionButton(bool value)
    {
        this._interactionButton.SetActive(value);
    }

    public void SetPush(bool value)
    {
        this._isBeenPush = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                this._isGrounded = true;
                break;

            case "Box":
                this._isGrounded = true;
                break;

            case "Player":
                if(collision.gameObject.GetComponent<PlayerCollider>().GetObj() != null && !this._isTrigged)
                    this.InteractionButton(true);
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Box":
                this._isGrounded = false;
                this.SetPush(false);
                break;

            case "Player":
                this.SetPush(false);
                this.InteractionButton(false);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                this._rb2D.isKinematic = true;
                this._isTrigged = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                this._rb2D.isKinematic = false;
                this._isTrigged = false;
                break;
        }
    }
}
