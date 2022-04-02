using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField]
    private Transform _interactionButton;
    private Transform _groupParent;

    public Transform GetGroupParent()
    {
        return _groupParent;
    }

    void Start()
    {
        _groupParent = transform.parent;
    }

    void Update()
    {

    }
}
