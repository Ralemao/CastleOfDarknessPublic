using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] 
    private Transform _grabPos;
    [SerializeField]
    private Transform _candlePos;

    public void SetPos()
    {
        _candlePos.position = _grabPos.position;
    }
}
