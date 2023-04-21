using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnPoint : MonoBehaviour
{

    public Vector3 RespawnPosition;

    public UnityEvent RespawnEvent;

    // Start is called before the first frame update
    void Start()
    {
        RespawnPosition = transform.position;
    }

    public void SetRespawn(Vector3 p)
    {
        RespawnPosition = p;
    }

    public void Respawn()
    {
        GameManager.instance.SetScripts();
        RespawnEvent.Invoke();
    }
}
