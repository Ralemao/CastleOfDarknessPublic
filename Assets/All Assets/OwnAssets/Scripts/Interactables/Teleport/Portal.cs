using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject targetPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //PlayerMovement.Instance.StopVel();
            //PlayerMovement.Instance.transform.position = targetPortal.transform.position;
            targetPortal.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Enemy") && FindObjectOfType<PortalActive>().alowEnemy)
        {
            collision.gameObject.transform.position = targetPortal.transform.position;
            targetPortal.gameObject.SetActive(false);
        }
    }
}
