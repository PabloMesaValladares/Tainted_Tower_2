using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MistEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem _particles;
    [SerializeField] private List<ParticleCollisionEvent> particles;
    public UnityEvent EnablePoison;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _particles = GetComponent<ParticleSystem>();
        particles = new List<ParticleCollisionEvent>();
    }

    /* private void OnParticleTrigger()
     {
         Debug.Log("praticula que toca algo");
     }*/

    void OnParticleCollision(GameObject lala) //esto sera para poner los controles inversos.
    {
        if (lala.TryGetComponent<PL>(out PL _pl))
        {
            lala.SetActive(false);
            Debug.Log("LA VIDA ES UNA TOMBOLA, TO TO TO TOMBOLA, DE LUZ Y DE COLOOOOOOOOOR, DE LUZ Y DE COLOOOOOOOR");
        }

        EnablePoison.Invoke();
    }

}
