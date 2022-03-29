using UnityEngine;

public class PortalActive : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    public bool alowEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            portal.gameObject.SetActive(true);

        if(alowEnemy && collision.gameObject.CompareTag("Enemy"))
            portal.gameObject.SetActive(true);
    }
}
