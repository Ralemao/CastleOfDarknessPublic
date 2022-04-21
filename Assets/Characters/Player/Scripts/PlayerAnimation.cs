using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private float speedAnim;
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
        if (PlayerMovement.Instance.CurrentSpeed() == 150) //Run
            speedAnim = 1;
        else if (PlayerMovement.Instance.CurrentSpeed() == 100) //Walk
            speedAnim = 0.75f;
        else if (PlayerMovement.Instance.CurrentSpeed() == 80) //Push
            speedAnim = 0.5f;
        else if (PlayerMovement.Instance.CurrentSpeed() == 60) //Slow
            speedAnim = 0.25f;
        else if (PlayerMovement.Instance.CurrentSpeed() == 0) //Idle
            speedAnim = 0;

        _anim.SetFloat("speed", speedAnim);
    }

    public void ClimbAnim()
    {
        _anim.SetTrigger("climb");
    }
}
