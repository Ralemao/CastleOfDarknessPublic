using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private float _fearLess;
    private bool _hasPlayer;
    private bool _isOn;

    private void Update()
    {
        SetInput();
    }

    private void SetInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && _hasPlayer)
        {
            if (_isOn)
                SetLightOn(false);
            else
                SetLightOn(true);
        }
    }

    private void SetLightOn(bool value)
    {
        _isOn = value;
        //GetComponent<BoxCollider2D>().enabled = value;
        GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = value;
        transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //_hasPlayer = true; 
            SetLightOn(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //_hasPlayer = false;
        }
    }
}
