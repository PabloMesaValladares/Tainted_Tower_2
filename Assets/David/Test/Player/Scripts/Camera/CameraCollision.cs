using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraCollision : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    public GameObject camera;
    public GameObject player;
    public float DistanceToGet;
    public float maxDistance;

    [SerializeField]
    float distance;


    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, camera.transform.position);

        float fov = Mathf.Lerp(20, DistanceToGet, maxDistance - distance);
        cam.m_Lens.FieldOfView = fov;
    }
}
