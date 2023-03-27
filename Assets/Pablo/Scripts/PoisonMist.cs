using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoisonMist : MonoBehaviour
{
    //[SerializeField] private GameObject _player;

    public UnityEvent ActivateMob; 
    public UnityEvent DesactivateMob;
    private void Awake()
    {
        DesactivateMob.Invoke();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<PL>(out PL _pl))
        {
            ActivateMob.Invoke();
            Debug.Log("Ha Entrado");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        DesactivateMob.Invoke();
        Debug.Log("Ha Salido");
    }
}
