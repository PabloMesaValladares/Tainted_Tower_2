using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SandStormBehaviour : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] int destPoint = 1;
    [SerializeField] float speed, remainingDistance;

    private void Start()
    {
        transform.position = points[0].position;
        GotoNextPoint();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[destPoint].position, speed * Time.deltaTime);

        if (GetRemainingDistance() < 2f)
        {
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        //destPoint = (Random.Range(0, points.Length)) % points.Length; RANDOM POINTS
        if(destPoint + 1 < points.Length)
        {
            destPoint++;    
        }
        else
        {
            destPoint = 0;
        }
    }

    float GetRemainingDistance()
    {
        remainingDistance = Vector3.Distance(transform.position, points[destPoint].position);

        return remainingDistance;
    }
}
