using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartsBehaviour : MonoBehaviour
{
    public int damage;
    public float miniTinmer;

    public void Update()
    {
        miniTinmer -= Time.deltaTime;

        if(miniTinmer <= 0)
        {
            gameObject.SetActive(false);
            miniTinmer = 1.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
            if (other.transform.parent.TryGetComponent<PlayerController>(out PlayerController player))
            {
                player.gameObject.transform.GetComponent<HealthBehaviour>().Hurt(damage);
            }

        gameObject.SetActive(false);
    }
}
