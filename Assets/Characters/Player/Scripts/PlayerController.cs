using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _canMove;
    private bool _isGrounded;

    public bool GetGrounded()
    {
        return _isGrounded;
    }

    //Singleton instantation
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<PlayerController>();

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        _canMove = true;
    }

    void Update()
    {
        _isGrounded = PlayerCollider.Instance.CheckIsGrounded();

        //if (!_canMove) return;

        PlayerMovement.Instance.enabled = _canMove;
        PlayerCollider.Instance.enabled = _canMove;
    }
}
