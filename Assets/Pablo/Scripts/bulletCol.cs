using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCol : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<PL>(out PL _pl))
        {
            gameObject.SetActive(false);
        }

        if(collision.gameObject.layer == 8)
        {
            gameObject.SetActive(false);
        }
    }
}