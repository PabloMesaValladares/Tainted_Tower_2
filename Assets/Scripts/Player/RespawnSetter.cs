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
            if (respawn.GetComponent<GroundCheck>().returnCheck())
            {
                Vector3 posToResp = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                respawn.SetRespawn(posToResp);
                GameManager.instance.CheckPoint();
                GameManager.instance.RespawnPosition = positionToRespawn.position;
            }
            else
            {
                Vector3 posToResp = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);

                if (Physics.Raycast(posToResp, -transform.up, out hit, distance))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Scenario"))
                    {
                        posToResp = posToResp = new Vector3(other.transform.position.x, hit.point.y, other.transform.position.z);
                    }
                }

                respawn.SetRespawn(posToResp);
                GameManager.instance.CheckPoint();
                GameManager.instance.RespawnPosition = positionToRespawn.position;
            }
        }
    }
}
