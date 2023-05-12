using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPosition : MonoBehaviour
{
    [SerializeField]
    GameObject pos;
    [SerializeField]
    bool move;
    ParticleSystem effect;
    // Start is called before the first frame update
    void Start()
    {
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pos != null)
        {
            transform.position = pos.transform.position;
        }
    }

    public void Move(GameObject p)
    {
        pos = p;
    }
}
