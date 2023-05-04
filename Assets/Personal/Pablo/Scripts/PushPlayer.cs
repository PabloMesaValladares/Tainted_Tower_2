using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    [SerializeField]
    private ChargeAttackMode charge;
    public int damage;
    public float force;
    public Vector3 dirToGo;

    void OnCollisionEnter(Collision lala)
    {
        if (lala.gameObject.transform.TryGetComponent<PlayerController>(out PlayerController _playerController))
        {
            _playerController.enabled = false;
            Vector3 dir = lala.contacts[0].point - transform.position;
            dir = -dir.normalized;
            lala.gameObject.transform.GetComponent<Rigidbody>().AddForce(dirToGo * force, ForceMode.Force);
            lala.gameObject.transform.GetComponent<HealthBehaviour>().Hurt(damage);
            _playerController.enabled = true;
            charge.ChangeTime();
            gameObject.SetActive(false);
        }
    }
}
