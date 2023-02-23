using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public bool firstHit;
    public float tickInterval;
    public float stormDuration;
    GameObject _go;
    Timer _timer;
    new Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        _go = GameObject.FindGameObjectWithTag("Player");
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        collider.enabled = false;

        transform.position = _go.transform.position;

        _timer.StartTimer(stormDuration);

        StartCoroutine("StartDelay");
    }

    private void OnDisable()
    {
        StopCoroutine("DamageTick");
    }

    private void OnTriggerEnter(Collider other)
    {
        //TryGetComponent<TestController>(out TestController _tc);
            DamageOverTime();
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine("DamageTick");
    }

    public void DamageOverTime()
    {
        if(firstHit)
        {
            print("big damage " + _go.name);
            firstHit = false;
        }

        StartCoroutine("DamageTick");
    }

    IEnumerator DamageTick()
    {
        print("smol damage " + _go.name);
        yield return new WaitForSeconds(tickInterval);
        DamageOverTime();
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(2.0f);

        collider.enabled = true;
    }

    void DisableObject()
    {
        this.gameObject.SetActive(false);
    }
}
