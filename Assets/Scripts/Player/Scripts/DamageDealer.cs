using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;

    public StatController stats;
    public ParticleSystem hitEffect;

    public LayerMask layersToReact;
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
        //stats = GetComponent<StatController>();
    }

    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layersToReact))
            {
                if (hit.transform.TryGetComponent(out Enemy enemy) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    hitEffect.gameObject.transform.position = hit.collider.transform.position;
                    hitEffect.Play();
                    Debug.Log(enemy.name);
                    enemy.TakeDamage(stats.CalculateDmg(stats.mainHand.damage, enemy.gameObject.GetComponent<StatController>().defense));
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
                else if (hit.transform.TryGetComponent(out HealthBehaviour health) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    hitEffect.gameObject.transform.position = hit.collider.transform.position;
                    hitEffect.Play();
                    Debug.Log(health.name);
                    health.Hurt(stats.CalculateDmg(stats.mainHand.damage, health.gameObject.GetComponent<StatController>().defense));
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}
