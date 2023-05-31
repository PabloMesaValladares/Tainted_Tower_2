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
    public ParticleSystem particle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.gameObject.TryGetComponent<RespawnPoint>(out RespawnPoint respawn))
        {
            if (other.transform.parent.gameObject.GetComponent<GroundCheck>().returnCheck())
            {
                if (SceneManager.GetActiveScene().name == "OpenWorld" || SceneManager.GetActiveScene().name == "TestWorld")
                {
                    Vector3 posToResp = new Vector3(other.transform.parent.position.x, other.transform.parent.position.y, other.transform.parent.position.z);
                    respawn.SetRespawn(posToResp);
                    GameManager.instance.Spawns = posToResp;
                    GameManager.instance.CheckPointSpawns = posToResp;
                }
                GameManager.instance.CheckPoint();
                GameManager.instance.SetScripts();
            }
            else
            {

                if (SceneManager.GetActiveScene().name == "OpenWorld" || SceneManager.GetActiveScene().name == "TestWorld")
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

                GameManager.instance.CheckPoint();
                GameManager.instance.SetScripts();
            }
        }
        particle.gameObject.SetActive(true);
        particle.Play();
        other.gameObject.transform.parent.GetComponent<HealthBehaviour>().AddHealthPercent(healthAddPerc); //para curar un porcentaje
    }
}
