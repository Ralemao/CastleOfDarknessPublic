using System;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool isUp;
    public bool isOn;
    private GameObject playerParent;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;
    [SerializeField] private GameObject upPos;
    [SerializeField] private GameObject downPos;

    private void Start()
    {
        playerParent = GameObject.Find("Parallax 0");
    }

    //--------------------------------------------------------------------------------

    private void Update()
    {
        ElevatorMovement();
    }

    private void ElevatorMovement()
    { 
        if (isOn)
        {
            //PlayerMovement.Instance.isElevating = true;
            if (isUp)
                transform.position = Vector2.MoveTowards(transform.position, target1.transform.position, speed);
            else
                transform.position = Vector2.MoveTowards(transform.position, target2.transform.position, speed);
        }
        //else if(PlayerMovement.Instance != null)
            //PlayerMovement.Instance.isElevating = false;
    }

    private void SetDestroy()
    {
        Destroy(upPos.gameObject);
        Destroy(downPos.gameObject);
    }

    //--------------------------------------------------------------------------------

    public void SetUpPos()
    {
        transform.position = target1.transform.position;
        SetDestroy();
    }

    public void SetDownPos()
    {
        transform.position = target2.transform.position;
        SetDestroy();
    }

    //--------------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.parent = gameObject.transform;

        if (collision.gameObject.CompareTag("Elevator"))
            isOn = false;

        if (collision.gameObject == target1.gameObject)
            isUp = false;

        if (collision.gameObject == target2.gameObject)
            isUp = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.parent = playerParent.transform;
    }
}
