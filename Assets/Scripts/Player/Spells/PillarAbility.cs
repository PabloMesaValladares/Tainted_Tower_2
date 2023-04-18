using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarAbility : MonoBehaviour
{
    [SerializeField] private bool hasOffset, playing;
    [SerializeField] private float timer, duration;

    Timer[] _timer;
    new Collider collider;
    Animator _anim;

    Vector3 spherePos;

    private void Awake()
    {
        _timer = GetComponents<Timer>();
        collider = GetComponent<Collider>();
        _anim = GetComponentInChildren<Animator>();
        playing = false;
    }

    void OnEnable()
    {
        collider.isTrigger = true;
        _timer[0].enabled = true;
        _timer[1].enabled = false;

        if(hasOffset)
            StartTimer(timer, 0);
        
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

        StartTimer(duration, 1);
    }

    public void ChangeColliderType()
    {
        collider.isTrigger = true;
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
