using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    public Animator Anim()
    {
        return _anim;
    }

    private static PlayerAnimation _instance;
    public static PlayerAnimation Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<PlayerAnimation>();

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _anim = transform.GetChild(0).GetComponent<Animator>();
    }

    public void SetHolding(bool value)
    {
        _anim.SetBool("isHolding", value);
    }

    public void SpeedAnim()
    {
        _anim.SetFloat("speed", PlayerMovement.Instance.CurrentSpeed());
    }

    public void ClimbAnim()
    {
        _anim.SetTrigger("climb");
    }
}
