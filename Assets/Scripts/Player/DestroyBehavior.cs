using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBehavior : MonoBehaviour
{
    public GameObject FX;
    public GameObject Animation;

    private void Start()
    {

    }

    public void DestroyObject()
    {
        Destroy(gameObject);

        if (FX != null)
        {
            if (FX.TryGetComponent(out ParticleSystem ps))
            {
                Instantiate(FX, transform.position, transform.rotation);
                ps.Play();

            }
        }

        /*if (Animation!= null)
        {
            
            if (Animation.TryGetComponent(out Animator anim))
            {
                Instantiate(Animation, transform.position, transform.rotation);
                anim.Play(1);
            }

        }*/
    }
}
