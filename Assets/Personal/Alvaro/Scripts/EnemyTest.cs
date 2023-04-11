using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] Transform referencePoint;
    [SerializeField] float deSpawnDistance;

    private void Update()
    {
        if(Vector3.Distance(transform.position, referencePoint.position) > deSpawnDistance)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetSpawnPoint(Transform point)
    {
        referencePoint = point;
    }
}
