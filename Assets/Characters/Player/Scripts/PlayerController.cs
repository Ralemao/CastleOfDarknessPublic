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
    private static PlayerController _instance;
    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<PlayerController>();

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        _canMove = true;
    }

    void Update()
    {
        _isGrounded = PlayerCollider.Instance.CheckIsGrounded();

        PlayerMovement.Instance.enabled = _canMove;
        PlayerCollider.Instance.enabled = _canMove;
    }
}
