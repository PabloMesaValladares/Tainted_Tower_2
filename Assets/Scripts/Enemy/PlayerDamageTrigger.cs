using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTrigger : MonoBehaviour
{
    public int damage;



    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<HealthBehaviour>().Hurt(damage);
    }
}
