using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDistance : MonoBehaviour
{
    [SerializeField] GameObject parent;
    public float Distance;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, parent.transform.position.y + Distance, gameObject.transform.position.z);
    }

}
