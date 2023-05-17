using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTrigger : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
            if (other.transform.parent.TryGetComponent<PlayerController>(out PlayerController player))
                player.GetComponent<HealthBehaviour>().Hurt(damage);
    }
}
