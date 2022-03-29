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

    private static EnemyAnimation instance;
    public static EnemyAnimation Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<EnemyAnimation>();

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        _anim = transform.GetChild(0).GetComponent<Animator>();
    }

    public void SpeedAnim()
    {
        _anim.SetFloat("speed", EnemyMovement.Instance.GetSpeed());
    }
}
