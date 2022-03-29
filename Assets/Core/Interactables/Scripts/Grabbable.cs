using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Transform _groupParent;

    public Transform GetGroupParent()
    {
        return _groupParent;
    }

    [SerializeField]
    private GameObject _interactionButton;

    void Start()
    {
        this._groupParent = transform.parent;
    }

    void Update()
    {

    }

    public void InteractionButton(bool value)
    {
        this._interactionButton.SetActive(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (collision.gameObject.GetComponent<PlayerCollider>().GetObj() != null)
                    this.InteractionButton(true);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                this.InteractionButton(false);
                break;
        }
    }
}
