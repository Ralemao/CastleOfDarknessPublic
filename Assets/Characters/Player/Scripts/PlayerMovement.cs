using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField]
    private float _normalSpeed;
    [SerializeField]
    private float _slowSpeed;
    [SerializeField] 
    private float _runSpeed;
    [SerializeField] 
    private float _pushSpeed;
    private float _currentSpeed;
    [SerializeField]
    private float _xClimb;
    [SerializeField]
    private float _yClimb;
    private bool _isSlow;
    private bool _isRuning;
    private bool _isRight;

    public float CurrentSpeed()
    {
        return _currentSpeed;
    }

    public bool IsFaceRight()
    {
        return _isRight;
    }

    private Rigidbody2D _rb2D;
    private Vector2 _moveVelocity;

    public Rigidbody2D GetRB2D()
    {
        return _rb2D;
    }

    //Singleton instantation
    private static PlayerMovement _instance;
    public static PlayerMovement Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<PlayerMovement>();

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _isRight = true;
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        InputSpeed();
        SetSpeed();
        SetMovementInputs();
        Climb();
    }

    private void InputSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _isRuning = true;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            _isRuning = false;

        if (Input.GetKeyDown(KeyCode.LeftControl))
            _isSlow = true;

        if (Input.GetKeyUp(KeyCode.LeftControl))
            _isSlow = false;
    }

    private void SetSpeed()
    {
        if(PlayerCollider.Instance.GetPush())
            _currentSpeed = _pushSpeed;
        else if(_isRuning)
            _currentSpeed = _runSpeed;
        else if (_isSlow)
            _currentSpeed = _slowSpeed;
        else
            _currentSpeed = _normalSpeed;
    }

    private void SetMovementInputs()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 horizontalInput = new Vector2(horizontal, _rb2D.velocity.y);
        _moveVelocity = horizontalInput.normalized * _currentSpeed;

        if (!PlayerCollider.Instance.GetPush())
        {
            if (!_isRight && horizontal > 0)
                Flip();
            else if (_isRight && horizontal < 0)
                Flip();
        }

        if (horizontal != 0)
            PlayerAnimation.Instance.SpeedAnim();
        else
        {
            _currentSpeed = 0;
            PlayerAnimation.Instance.SpeedAnim();
        }
    }

    private void Climb()
    {
        if (PlayerCollider.Instance.GetClimb())
        {
            PlayerCollider.Instance.SetClimb(false);
            PlayerAnimation.Instance.ClimbAnim();

            if (PlayerAnimation.Instance.Anim().GetCurrentAnimatorStateInfo(0).IsName("Climb"))
                transform.position = new Vector2(transform.position.x + (_xClimb * transform.localScale.x), transform.position.y + _yClimb);
        }
    }

    private void FixedUpdate()
    {
        if (!PlayerCollider.Instance.GetClimb() && PlayerController.Instance.GetGrounded())
            _rb2D.velocity = _moveVelocity * Time.fixedDeltaTime;
    }

    private void Flip()
    {
        _isRight = !_isRight;

        if(_isRight) 
            transform.localScale = new Vector2(1, 1);
        else
            transform.localScale = new Vector2(-1, 1);
    }

    public void SetRuning(bool value)
    {
        _isRuning = value;
    }
}
