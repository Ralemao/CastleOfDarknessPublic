using System;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public bool isIn;
    [SerializeField] private GameObject[] platforms;

    private void Start()
    {
        IsTrigger();
    }

    public void GetUp()
    {
        //if (PlayerMovement.Instance.playerVel.y > 0)
        //{
        //    isIn = false;
        //    IsTrigger();
        //}
    }

    public void GetDown()
    {
        //if (PlayerMovement.Instance.playerVel.y < 0)
        //{
        //    isIn = true;
        //    IsTrigger();
        //}
    }

    public void IsTrigger()
    {
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<Collider2D>().isTrigger = isIn;
        }
    }
}
