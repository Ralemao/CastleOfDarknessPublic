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
    private static EnemyController instance;
    public static EnemyController Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<EnemyController>();

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
        _isGrounded = EnemyCollider.Instance.CheckIsGrounded();

        //if (!_canMove) return;

        EnemyMovement.Instance.enabled = _canMove;
        EnemyCollider.Instance.enabled = _canMove;
    }
}
