using UnityEngine;

public class ElevatorPos : MonoBehaviour
{
    [SerializeField] private bool isUp;

    private void SetPos()
    {
        if (isUp)
            FindObjectOfType<Elevator>().SetUpPos();
        else
            FindObjectOfType<Elevator>().SetDownPos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SetPos();
        }
    }
}
