using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;
    public bool rotate;
    public float camDistance;
    public float camZoom;
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y += camDistance;
        transform.position = newPosition;

        cam.orthographicSize = camZoom;

        if (rotate)
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
