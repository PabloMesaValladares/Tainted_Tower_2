using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticlesHit : MonoBehaviour
{
    public ParticleSystem enemyParticleAttack;

    public int damage;

    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();

    
    private void OnParticleCollision(GameObject other)
    {
        int events = enemyParticleAttack.GetCollisionEvents(other, colEvents);

        other.gameObject.GetComponent<HealthBehaviour>().Hurt(damage);
    }
}
