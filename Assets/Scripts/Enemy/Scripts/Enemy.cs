using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] float lookAtPlayerSpeed;


    [SerializeField] Transform referencePoint;
    [SerializeField] float deSpawnDistance;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;

    public UnityEvent DieEvent, HitEvent;

    GameObject player;
    public Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = GetComponent<StatController>().health;
        health = maxHealth;

        //life = GetComponent<LifeTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(player.transform.position, referencePoint.position) > deSpawnDistance)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        
        

    }

    
    void LookAtPlayer()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 lookPoint = Vector3.Lerp(playerPos, transform.position, lookAtPlayerSpeed);
        transform.LookAt(lookPoint);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        HitEvent.Invoke();
        Debug.Log(damageAmount);
        //animator.SetTrigger("damage");
        //CameraShake.Instance.ShakeCamera(2f, 0.2f);
        //life.Life = health;
        if (health <= 0)
        {
            DieEvent.Invoke();
            //Die();
        }
    }
    public void StartDealDamage()
    {
        //GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        //GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    public void SetSpawnPoint(Transform point)
    {
        referencePoint = point;
    }
}