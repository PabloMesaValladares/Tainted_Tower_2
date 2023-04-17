using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarAbility : MonoBehaviour
{
    [SerializeField] private bool hasOffset;
    [SerializeField] private float timer, knockBackForce, knowckBackRadius,upKnockBackForce;

    Timer[] _timer;
    new Collider collider;
    Animator _anim;

    Vector3 spherePos;

    private void Awake()
    {
        _timer = GetComponents<Timer>();
        collider = GetComponent<Collider>();
        _anim = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        collider.isTrigger = true;

        if (hasOffset)
            StartTimer(timer, 0);
        else
            PlayAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            spherePos = player.transform.position;
        }
    }

    public void PlayAnimation()
    {
        _anim.SetTrigger("Pilar");
    }

    public void ChangeColliderType()
    {
        collider.isTrigger = false;
    }

    public void DisableParent()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void StartTimer(float timer, int i)
    {
        _timer[i].StartTimer(timer);
    }
}
