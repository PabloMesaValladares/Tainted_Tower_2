using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSetter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.gameObject.TryGetComponent<RespawnPoint>(out RespawnPoint respawn))
        {
            respawn.SetRespawn(transform.position);
        }
    }
}
