using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCol : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController _playerController))
        {
            GetComponent<SlashMovement>().ResetMovement();
            gameObject.SetActive(false);
            _playerController.gameObject.SetActive(false);
        }

        if(collision.gameObject.layer == 8)
        {
            GetComponent<SlashMovement>().ResetMovement();
            gameObject.SetActive(false);
        }
    }
}
