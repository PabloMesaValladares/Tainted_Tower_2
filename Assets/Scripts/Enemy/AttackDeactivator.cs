using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDeactivator : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SlashMovement>(out SlashMovement slash))
        {
            slash.ResetMovement();
        }
        other.gameObject.SetActive(false);
    }
}
