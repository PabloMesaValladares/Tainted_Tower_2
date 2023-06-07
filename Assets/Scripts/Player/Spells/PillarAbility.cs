using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarAbility : MonoBehaviour
{
    [SerializeField] private bool hasOffset;
    [SerializeField] private float timer, duration;
    [SerializeField] private int damage;

    Timer[] _timer;
    new Collider collider;
    Animator _anim;

    public LayerMask markable;
    float markedLayer;


    private void Awake()
    {
        _timer = GetComponents<Timer>();
        collider = GetComponent<Collider>();
        _anim = GetComponentInChildren<Animator>();

        var rawValue = markable.value;
        var layerValue = Mathf.Log(rawValue, 2);
        markedLayer = layerValue;
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
        if(other.gameObject.layer == markedLayer)
        {
            other.GetComponent<HealthBehaviour>().Hurt(damage);
        }
    }

    public void PlayAnimation()
    {
        _anim.SetTrigger("Pilar");

        StartTimer(duration, 1);
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