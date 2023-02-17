using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public bool firstHit;
    public float tickInterval;
    GameObject _go;
    new Collider collider;

    private void OnEnable()
    {
        collider = GetComponent<Collider>();
        collider.enabled = false;

        _go = GameObject.FindGameObjectWithTag("Player");
        transform.position = _go.transform.position;

        StartCoroutine("StartDelay");
    }

    private void OnTriggerEnter(Collider other)
    {
        TryGetComponent<TestController>(out TestController _tc);
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

            return;
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
        yield return new WaitForSeconds(1.0f);

        collider.enabled = true;
    }

}
