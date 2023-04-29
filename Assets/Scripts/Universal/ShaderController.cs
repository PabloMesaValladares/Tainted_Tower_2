using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShaderController : MonoBehaviour
{
    GameObject cam;
    GameObject player;

    public float maxDistance;
    [SerializeField] float dist;
    [SerializeField] float amount;

    public SkinnedMeshRenderer Body;
    MarkEnemy mark;

    private void Start()
    {
        cam = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        amount = 0;
        mark = player.GetComponent<MarkEnemy>();

    }
    private void Update()
    {
        dist = Vector3.Distance(cam.transform.position, player.transform.position);
        amount = (maxDistance - dist) / maxDistance;
        if (dist < maxDistance)
        {
        }
    }

}
