using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MobStats : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, health;
    [SerializeField]
    private UnityEvent Die;

    [SerializeField]
    private BoxCollider mobCollider;
    [SerializeField]
    private StatController _playerStats;
    // Start is called before the first frame update
    void Start()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<StatController>();
        health = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die.Invoke();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == 15)
        {
            TakeDamage(_playerStats.damage);
        }
    }
}
