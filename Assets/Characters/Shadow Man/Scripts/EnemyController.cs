using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool _canMove;
    private bool _isGrounded;

    public bool GetGrounded()
    {
        return _isGrounded;
    }

    //Singleton instantation
    private static EnemyController _instance;
    public static EnemyController Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<EnemyController>();

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
        _isGrounded = EnemyCollider.Instance.CheckIsGrounded();

        EnemyMovement.Instance.enabled = _canMove;
        EnemyCollider.Instance.enabled = _canMove;
    }
}
