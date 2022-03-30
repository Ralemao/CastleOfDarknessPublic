using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [Header("RayCasts")]
    [SerializeField]
    private float _highRayDistance;
    [SerializeField]
    private float _highYRayPos;
    [SerializeField]
    private float _lowRayDistance;
    [SerializeField]
    private float _lowYRayPos;
    [SerializeField]
    private float _groundRayDistance;

    private bool _isPushing;
    private bool _isClimbing;
    private bool _canClimb;
    private bool _isGrabbing;

    public bool GetPush()
    {
        return _isPushing;
    }

    public bool GetClimb()
    {
        return _isClimbing;
    }

    [Header("Layers")]
    [SerializeField]
    private LayerMask _objMask;
    [SerializeField]
    private LayerMask _groundLayer;

    [Header("References")]
    [SerializeField]
    private Transform _playerGrab;
    private Transform _playerParent;

    private GameObject _obj;

    public GameObject GetObj()
    {
        return _obj;
    }

    //Singleton instantation
    private static PlayerCollider instance;
    public static PlayerCollider Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<PlayerCollider>();

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        _playerParent = transform.parent;
    }

    private void Update()
    {
        SetGravity();
        Interactions();
    }

    private void SetGravity()
    {
        if (!CheckIsGrounded())
            PlayerMovement.Instance.RB2D().gravityScale = 40;
        else
            PlayerMovement.Instance.RB2D().gravityScale = 1;
    }

    private void Interactions()
    {
        Physics2D.queriesStartInColliders = false;

        RaycastHit2D lowHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _lowYRayPos),
                                        Vector2.right * transform.localScale.x, _lowRayDistance, _objMask);

        RaycastHit2D highHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + _highYRayPos),
                                        Vector2.right * transform.localScale.x, _highRayDistance, _objMask);

        if (lowHit.collider != null)
        {
            _obj = lowHit.collider.gameObject;

            //Grab objects
            if (_obj.CompareTag("Grabbable"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _isGrabbing = !_isGrabbing;
                }

                if (_isGrabbing)
                {
                    _obj.GetComponent<Grabbable>().transform.SetParent(_playerGrab);
                    _obj.GetComponent<Grabbable>().InteractionButton(false);
                }
                else
                    _obj.GetComponent<Grabbable>().transform.SetParent(_obj.GetComponent<Grabbable>().GetGroupParent());
            }

            //Boxes interactions
            else if (_obj.CompareTag("Box"))
            {
                Box objBox = _obj.GetComponent<Box>();

                if (objBox != null)
                {
                    //Climb
                    if (CheckIsGrounded() && PlayerMovement.Instance.CurrentSpeed() == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && !_isPushing)
                            _canClimb = true;
                        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
                            _canClimb = false;
                    }

                    //highHit is the limit for climbing
                    if (!highHit && lowHit && _canClimb)
                    {
                        _isClimbing = true;
                        objBox.InteractionButton(false);
                    }

                    //Push and pull obj
                    if (_isPushing)
                    {
                        if (Input.GetKeyUp(KeyCode.E) || !objBox.IsPushing() || !CheckIsGrounded())
                        {
                            _isPushing = false;
                            objBox.SetPush(false);
                            _obj.GetComponent<FixedJoint2D>().enabled = false;
                            _obj.GetComponent<FixedJoint2D>().connectedBody = null;
                            objBox = null;
                        }
                    }
                    else
                    {
                        if (Input.GetKey(KeyCode.E) && objBox.GetGrounded() && CheckIsGrounded())
                        {
                            _isPushing = true;
                            objBox.SetPush(true);
                            objBox.InteractionButton(false);
                            _obj.GetComponent<FixedJoint2D>().enabled = true;
                            _obj.GetComponent<FixedJoint2D>().connectedBody = PlayerMovement.Instance.RB2D();
                        }
                    }
                }
            }
        }
        else
        {
            _obj = null;
            if(_canClimb) _canClimb = false;
            if(_isPushing) _isPushing = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y + _lowYRayPos),
                        new Vector2(transform.position.x, transform.position.y + _lowYRayPos) +
                            Vector2.right * transform.localScale.x * _lowRayDistance);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y + _highYRayPos),
                        new Vector2(transform.position.x, transform.position.y + _highYRayPos) +
                            Vector2.right * transform.localScale.x * _highRayDistance);
    }

    private void AddToInventory()
    {

    }

    public bool CheckIsGrounded()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRayDistance, _groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * _groundRayDistance, Color.green);

        if (hitGround.collider != null)
            return true;

        return false;
    }

    public void SetClimb(bool value)
    {
        _isClimbing = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Platform":
                transform.parent = collision.gameObject.transform;
                break;

            case "Interactable":
                AddToInventory();
                Destroy(collision.gameObject);
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Platform":
                transform.parent = _playerParent;
                break;
        }
    }
}
