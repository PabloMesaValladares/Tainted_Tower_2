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

    public Material BodyMaterial;
    public Material HairMaterial;
    public Material ClothesMaterial;
    public Material EyeMaterial;

    private void Start()
    {
        cam = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        amount = 0;
        BodyMaterial.SetFloat("_Amount", amount);
        HairMaterial.SetFloat("_Amount", amount);
        ClothesMaterial.SetFloat("_Amount", amount);
        EyeMaterial.SetFloat("_Amount", amount);
    }
    private void Update()
    {
        dist = Vector3.Distance(cam.transform.position, player.transform.position);
        amount = (maxDistance - dist) / maxDistance;
        if (dist < maxDistance)
        {
            BodyMaterial.SetFloat("_Amount", amount);
            HairMaterial.SetFloat("_Amount", amount);
            ClothesMaterial.SetFloat("_Amount", amount);
            EyeMaterial.SetFloat("_Amount", amount);
        }
    }

}
