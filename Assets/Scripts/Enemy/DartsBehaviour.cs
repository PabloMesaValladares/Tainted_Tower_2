using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartsBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController _playerController))
        {
            gameObject.SetActive(false);
            _playerController.gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == 8)
        {
            gameObject.SetActive(false);
        }
    }
}
