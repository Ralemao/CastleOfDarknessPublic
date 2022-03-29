using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private enum Object { Normal, Ramp };
    [SerializeField] private Object obj;
    private enum Trigger { Up, Down , UpToDown};
    [SerializeField] private Trigger trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (obj == Object.Normal)
            {
                if (trigger == Trigger.Down)
                {
                    FindObjectOfType<PlatformManager>().isIn = true;
                    FindObjectOfType<PlatformManager>().IsTrigger();
                }
                else if (trigger == Trigger.Up)
                {
                    FindObjectOfType<PlatformManager>().isIn = false;
                    FindObjectOfType<PlatformManager>().IsTrigger();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (obj == Object.Ramp)
            {
                if (trigger == Trigger.Up)
                    FindObjectOfType<PlatformManager>().GetUp();
            }
            else
            {
                if (trigger == Trigger.UpToDown)
                {
                    FindObjectOfType<PlatformManager>().GetDown();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (trigger == Trigger.UpToDown)
            {
                FindObjectOfType<PlatformManager>().isIn = true;
                FindObjectOfType<PlatformManager>().IsTrigger();
            }
        }
    }
}
