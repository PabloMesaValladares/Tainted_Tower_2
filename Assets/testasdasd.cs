using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testasdasd : MonoBehaviour
{
    Vector3 spherePos;

    [SerializeField] private float knockBackForce, knockBackRadius, upKnockBackForce;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<TestController>(out TestController player))
        {
            print("enters");
            spherePos = player.transform.position;
            player.GetComponent<Rigidbody>().AddForce(-player.transform.forward * knockBackForce);
        }
    }
}
