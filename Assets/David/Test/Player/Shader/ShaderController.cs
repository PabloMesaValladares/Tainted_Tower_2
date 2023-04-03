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

    public Material BodyMaterial;
    public Material HairMaterial;
    public Material ClothesMaterial;
    public Material EyeMaterial;

    private void Start()
    {
        cam = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
     dist = Vector3.Distance(cam.transform.position, player.transform.position);

        if (dist < maxDistance)
        {
            BodyMaterial.SetFloat("_Amount", (maxDistance - dist) / maxDistance);
            HairMaterial.SetFloat("_Amount", (maxDistance - dist) / maxDistance);
            ClothesMaterial.SetFloat("_Amount", (maxDistance - dist) / maxDistance);
            EyeMaterial.SetFloat("_Amount", (maxDistance - dist) / maxDistance);
        }
    }

}
