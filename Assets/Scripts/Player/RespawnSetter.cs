using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSetter : MonoBehaviour
{
    public Transform positionToRespawn;
    RaycastHit hit;
    public int healthAddPerc;
    float distance = 1000;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.gameObject.TryGetComponent<RespawnPoint>(out RespawnPoint respawn))
        {
            if (respawn.GetComponent<GroundCheck>().returnCheck())
            {
                GameManager.instance.CheckPoint();
                GameManager.instance.SetScripts();
                if (SceneManager.GetActiveScene().name == "OpenWorld")
                {
                    Vector3 posToResp = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                    respawn.SetRespawn(posToResp);
                    GameManager.instance.Spawns = transform.position;
                }
            }
            else
            {
                GameManager.instance.CheckPoint();
                GameManager.instance.SetScripts();

                if (SceneManager.GetActiveScene().name == "OpenWorld")
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
                    GameManager.instance.Spawns = transform.position;
                }
            }
        }
        other.gameObject.transform.parent.GetComponent<HealthBehaviour>().AddHealthPercent(healthAddPerc); //para curar un porcentaje
    }
}
