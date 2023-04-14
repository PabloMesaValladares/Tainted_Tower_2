using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarAbility : MonoBehaviour
{

    [SerializeField] private bool hasOffset;
    [SerializeField] private float offset, knockBackForce, knowckBackRadius,upKnockBackForce;

    Timer _timer;
    new Collider collider;

    Vector3 spherePos;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        _timer = GetComponent<Timer>();
    }

    void OnEnable()
    {
        if(hasOffset)
        {
            collider.enabled = false;
            _timer.StartTimer(offset);
        }
        else
            PlayAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            spherePos = player.transform.position;
            player.GetComponent<Rigidbody>().AddExplosionForce(knockBackForce, spherePos, knowckBackRadius, upKnockBackForce);
        }
    }

    public void PlayAnimation()
    {

    }

    public void ChangeColliderType()
    {
        collider.isTrigger = false;
    }
}
