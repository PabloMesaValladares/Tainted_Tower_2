using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandStorm : MonoBehaviour
{
    public Transform player;
    public GameObject stormPrefab;

    public float timer;
    [SerializeField] private float time;
    [SerializeField] private bool instantiate = false;

    private void Start()
    {
        time = timer;
    }

    void Update()
    {
        UpdateTimer();

        if(instantiate)
        {
            Instantiate(stormPrefab, player.position, Quaternion.Euler(-90, 0, 0));
            instantiate = false;
        }
    }

    private void UpdateTimer()
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            instantiate = true;
            time = timer;
        }
    }
}
