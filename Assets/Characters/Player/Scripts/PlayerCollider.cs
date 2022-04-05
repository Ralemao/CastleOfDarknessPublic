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
    private Transform _interactionButton;
    [SerializeField]
    private Transform _playerGrab;
    private Transform _playerParent;

    private GameObject _obj;

    public GameObject GetObj()
    {
        return _obj;
    }

    private static PlayerCollider _instance;
    public static PlayerCollider Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<PlayerCollider>();

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _playerParent = transform.parent;
    }

    private void Update()
    {
        Interactions();
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
            switch (_obj.tag)
            {
                case "Grabbable":

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _isGrabbing = !_isGrabbing;
                    }

                    if (_isGrabbing)
                    {
                        InteractionButton(false);
                        _obj.GetComponent<Grabbable>().transform.SetParent(_playerGrab);
                    }
                    else
                    {
                        InteractionButton(true);
                        _obj.GetComponent<Grabbable>().transform.SetParent(_obj.GetComponent<Grabbable>().GetGroupParent());
                    }
                    break;

                //Boxes interactions
                case "Box":

                    Box objBox = _obj.GetComponent<Box>();

                    if (objBox != null)
                    {
                        _isPushing = objBox.IsPushing();

                        //Climb
                        //highHit is the limit for climbing
                        if (CheckIsGrounded() && PlayerMovement.Instance.CurrentSpeed() == 0 && !_canClimb && !highHit)
                        {
                            InteractionButton(true);

                            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && !_isPushing)
                                _canClimb = true;

                            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
                                _canClimb = false;
                        }

                        if (_canClimb)
                        {
                            _isClimbing = true;
                            InteractionButton(false);
                        }

                        //Push and pull obj
                        if (_isPushing)
                        {
                            if (Input.GetKeyUp(KeyCode.E))
                                InteractionButton(true);

                            if (Input.GetKeyUp(KeyCode.E) || !CheckIsGrounded())
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
                                InteractionButton(false);
                                _obj.GetComponent<FixedJoint2D>().enabled = true;
                                _obj.GetComponent<FixedJoint2D>().connectedBody = PlayerMovement.Instance.GetRB2D();
                            }
                        }
                    }
                    break;
            }
        }
        else
        {
            InteractionButton(false);
            _obj = null;
            _canClimb = false;
            _isPushing = false;
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

    public void InteractionButton(bool value)
    {
        _interactionButton.gameObject.SetActive(value);

        if (value)
            _interactionButton.transform.position = _obj.transform.GetChild(1).transform.position;
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
