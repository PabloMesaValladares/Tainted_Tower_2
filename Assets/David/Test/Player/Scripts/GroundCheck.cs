using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float normalColliderHeight;
    public LayerMask layersToReact;
    [SerializeField]
    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = CheckCollisionOverlap(gameObject.transform.position + Vector3.down * normalColliderHeight);
    }

    public bool returnCheck()
    {
        return grounded;
    }
    public bool CheckCollisionOverlap(Vector3 targetPositon)
    {
        RaycastHit hit;

        Vector3 direction = targetPositon - gameObject.transform.position;
        if (Physics.Raycast(gameObject.transform.position, direction, out hit, normalColliderHeight, layersToReact))
        {
            Debug.DrawRay(gameObject.transform.position, direction * normalColliderHeight, Color.yellow);
            return true;
        }
        else
        {
            Debug.DrawRay(gameObject.transform.position, direction * normalColliderHeight, Color.white);
            return false;
        }
    }
}
