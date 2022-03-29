using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private bool isOn;
    [SerializeField] private GameObject elevator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if(PlayerMovement.Instance.playerVel.y > 0 && !elevator.GetComponent<Elevator>().isOn)
            //{
            //    elevator.GetComponent<Elevator>().isOn = true;
            //}
        }
    }
}
