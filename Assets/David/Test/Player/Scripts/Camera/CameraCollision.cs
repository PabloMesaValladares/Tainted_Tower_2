using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;
    public float smooth = 10.0f;
    Vector3 direction;
    public float distance;

    public LayerMask layers;

    // Start is called before the first frame update
    void Awake()
    {
        direction = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(direction * maxDistance);
        RaycastHit hit;

        Debug.DrawLine(transform.parent.position, desiredCameraPos, Color.white);
        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit, layers))
        {
            distance = Mathf.Clamp((hit.distance * 0.09f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, direction * distance, Time.deltaTime * smooth);
    }
}
