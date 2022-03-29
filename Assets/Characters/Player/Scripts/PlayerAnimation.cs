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

    //Singleton instantation
    private static PlayerAnimation instance;
    public static PlayerAnimation Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<PlayerAnimation>();

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        _anim = transform.GetChild(0).GetComponent<Animator>();
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
