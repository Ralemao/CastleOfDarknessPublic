using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _anim;

    public Animator Anim()
    {
        return _anim;
    }

    private static EnemyAnimation _instance;
    public static EnemyAnimation Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<EnemyAnimation>();

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _anim = transform.GetChild(0).GetComponent<Animator>();
    }

    public void SpeedAnim()
    {
        _anim.SetFloat("speed", EnemyMovement.Instance.GetSpeed());
    }
}
