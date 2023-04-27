using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSetter : MonoBehaviour
{
    public Transform positionToRespawn;
    RaycastHit hit;
    float distance = 1000;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.gameObject.TryGetComponent<RespawnPoint>(out RespawnPoint respawn))
        {
            if(respawn.GetComponent<GroundCheck>().returnCheck())
            {
                Vector3 posToResp = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
                respawn.SetRespawn(posToResp);
                GameManager.instance.CheckPoint();
            }
            else
            {
                Vector3 posToResp = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);

                if (Physics.Raycast(other.transform.parent.transform.position, -other.transform.parent.transform.up, out hit, distance))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Scenario"))
                    {
                        posToResp = posToResp = new Vector3(other.transform.position.x, transform.position.y - hit.distance, other.transform.position.z);
                    }
                }
              
                respawn.SetRespawn(posToResp);
                GameManager.instance.CheckPoint();
            }
           
        }
    }
}
