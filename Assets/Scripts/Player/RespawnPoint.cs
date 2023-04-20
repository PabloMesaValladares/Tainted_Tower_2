using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{

    public Vector3 RespawnPosition;


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
        gameObject.transform.position = RespawnPosition;
    }
}