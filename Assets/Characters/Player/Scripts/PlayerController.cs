using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _deathTimer;
    private bool _canMove;
    private bool _isGrounded;
    public bool _isHolding;

    public bool GetGrounded()
    {
        return _isGrounded;
    }

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
    }

    void Start()
    {
        _canMove = true;
    }

    void Update()
    {
        _isGrounded = PlayerCollider.Instance.CheckIsGrounded();

        PlayerMovement.Instance.enabled = _canMove;
        PlayerCollider.Instance.enabled = _canMove;
        PlayerAnimation.Instance.SetHolding(_isHolding);
    }

    public void SetDeath()
    {
        _canMove = false;
        StartCoroutine(CanMove());
    }

    IEnumerator CanMove()
    {
        yield return new WaitForSeconds(_deathTimer);
        _canMove = true;
    }
}
