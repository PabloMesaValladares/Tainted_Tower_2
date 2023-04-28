using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;
    [SerializeField] GameObject hitVFX;
    [SerializeField] float lookAtPlayerSpeed;
    public GameObject head;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;

    GameObject player;
    NavMeshAgent agent;
    public Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = GetComponent<StatController>().health;
        health = maxHealth;
        //life = GetComponent<LifeTest>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        //if (player == null)
        //{
        //    return;
        //}

        //if (timePassed >= attackCD)
        //{
        //    if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        //    {
        //        animator.SetTrigger("attack");
        //        timePassed = 0;
        //    }
        //}
        //timePassed += Time.deltaTime;

        //if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        //{
        //    LookAtPlayer();
        //    newDestinationCD = 0.5f;
        //    agent.SetDestination(player.transform.position);
        //}
        //newDestinationCD -= Time.deltaTime;

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        print(true);
    //        player = collision.gameObject;
    //    }
    //}

    void LookAtPlayer()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 lookPoint = Vector3.Lerp(playerPos, transform.position, lookAtPlayerSpeed);
        transform.LookAt(lookPoint);
    }

    void Die()
    {
        //Instantiate(ragdoll, transform.position, transform.rotation);
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        //animator.SetTrigger("damage");
        //CameraShake.Instance.ShakeCamera(2f, 0.2f);
        //life.Life = health;
        if (health <= 0)
        {
            Die();
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

    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}