using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField]
    private float _persuitSpeed;
    [SerializeField]
    private float _attackSpeed;
    [SerializeField]
    private float _endAttackTimer;

    [Header("RayCasts")]
    [SerializeField]
    private float _xTargetRayDistance;
    [SerializeField]
    private float _xAttackRayDistance;
    [SerializeField]
    private float _groundRayDistance;
    private bool _startAttack;
    private bool _endAttack;

    [Header("Layers")]
    [SerializeField]
    private LayerMask _targetMask;
    [SerializeField]
    private LayerMask _groundLayer;

    private static EnemyCollider _instance;
    public static EnemyCollider Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<EnemyCollider>();

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        SetTarget();
        SetAttack();
    }

    private void SetTarget()
    {
        RaycastHit2D targetHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), 
                                                        Vector2.right * transform.localScale.x, _xTargetRayDistance, _targetMask);

        if (targetHit.collider != null)
        {
            EnemyMovement.Instance.IsTarget(true);

            if(!_startAttack && !_endAttack)
                EnemyMovement.Instance.SetSpeed(_persuitSpeed);
        }
        else
        {
            EnemyMovement.Instance.IsTarget(false);
            EnemyMovement.Instance.SetSpeed(0);
        }
    }

    private void SetAttack()
    {
        RaycastHit2D attackHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
                                                        Vector2.right * transform.localScale.x, _xAttackRayDistance, _targetMask);

        if (attackHit.collider != null)
        {
            if (!_endAttack)
                _startAttack = true;

            if (_startAttack)
                EnemyMovement.Instance.SetSpeed(_attackSpeed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y),
                        new Vector2(transform.position.x, transform.position.y) + 
                            Vector2.right * transform.localScale.x * _xTargetRayDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y),
                        new Vector2(transform.position.x, transform.position.y) +
                            Vector2.right * transform.localScale.x * _xAttackRayDistance);
    }

    IEnumerator EndAttack()
    {
        _startAttack = false;
        _endAttack = true;
        EnemyMovement.Instance.SetSpeed(0);

        yield return new WaitForSeconds(_endAttackTimer);

        _endAttack = false;
    }

    public bool CheckIsGrounded()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRayDistance, _groundLayer.value);
        Debug.DrawRay(transform.position, Vector2.down * _groundRayDistance, Color.green);

        if (hitGround.collider != null)
            return true;

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                StartCoroutine(EndAttack());
                break;
        }
    }
}