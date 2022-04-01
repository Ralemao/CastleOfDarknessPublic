using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField]
    private GameObject _respawnPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.Lives() > 1)
            {
                GameManager.Instance.RemoveLives();
                collision.GetComponent<PlayerController>().SetDeath();
                collision.transform.position = _respawnPlayer.transform.position;
            }
            else
                LevelManager.Instance.ReloadLevel();
        }
    }
}
