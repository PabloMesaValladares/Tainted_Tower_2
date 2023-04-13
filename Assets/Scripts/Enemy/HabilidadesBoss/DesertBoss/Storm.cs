using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public bool firstHit;
    public float tickInterval;
    public float stormDuration;
    GameObject _go;
    HealthBehaviour _playerHB;
    Timer _timer;
    new Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        _go = GameObject.FindGameObjectWithTag("Player");
        _playerHB = _go.GetComponent<HealthBehaviour>();
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        collider.enabled = false;
        firstHit = true;

        transform.position = _go.transform.position;

        _timer.StartTimer(stormDuration);

        StartCoroutine(nameof(StartDelay));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(DamageTick));
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageOverTime();
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(nameof(DamageTick));
    }

    public void DamageOverTime()
    {
        if(firstHit)
        {
            _playerHB.Hurt(250);
            firstHit = false;
        }

        StartCoroutine(nameof(DamageTick));
    }

    IEnumerator DamageTick()
    {
        _playerHB.Hurt(10);
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
        gameObject.SetActive(false);
    }
}
