using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoisonMist : MonoBehaviour
{
    //[SerializeField] private GameObject _player;

    public UnityEvent EnableMist; 
    public UnityEvent DisableMist;
    private void Awake()
    {
        DisableMist.Invoke();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<PL>(out PL _pl))
        {
            EnableMist.Invoke();
            Debug.Log("Ha Entrado");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        DisableMist.Invoke();
        Debug.Log("Ha Salido");
    }
}
