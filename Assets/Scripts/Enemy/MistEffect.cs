using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MistEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem _particles;
    [SerializeField] private List<ParticleCollisionEvent> particles;
    public int damage;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _particles = GetComponent<ParticleSystem>();
        particles = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject lala) //esto sera para poner los controles inversos.
    {
        if (lala.TryGetComponent<PlayerController>(out PlayerController _playerController))
        {
            lala.GetComponent<HealthBehaviour>().Hurt(damage);
        }
    }
}
