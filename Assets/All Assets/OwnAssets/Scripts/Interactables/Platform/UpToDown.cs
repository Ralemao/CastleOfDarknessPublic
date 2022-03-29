using UnityEngine;

public class UpToDown : MonoBehaviour
{
    [SerializeField] private GameObject platform;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player") && PlayerMovement.Instance.playerVel.y < 0 && platform != null)
        //    platform.GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && platform != null)
            platform.GetComponent<Collider2D>().isTrigger = false;
    }
}
