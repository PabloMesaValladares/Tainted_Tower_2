using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movement;
    [SerializeField]
    private RandomIdleMovement _idle;
    [SerializeField]
    private GameObject player, boom, exclamation, enemy;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private ParticleSystem _particles;
    [SerializeField]
    private StatController statController;
    [SerializeField]
    private Enemy enemyController;

    [SerializeField]
    private float vel, distanceBoom, maxDistanceBoom, cooldown, maxCooldown;
    [SerializeField]
    private bool inRange, objectiveConfirmed;
    [SerializeField]
    private Vector3 oldPlayerPosition;
    [SerializeField]
    private int savedHealth;

    // Start is called before the first frame update
    void Awake()
    {
        cooldown = maxCooldown;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange == true)
        {
            ObjectiveChecked();
            cooldown -= Time.deltaTime;
            distanceBoom = Vector3.Distance(oldPlayerPosition, gameObject.transform.position);
            transform.LookAt(new Vector3(oldPlayerPosition.x, transform.position.y, oldPlayerPosition.z));
            exclamation.SetActive(true);

            if (cooldown <= 0)
            {
                _animator.SetInteger("State", 2);
                objectiveConfirmed = true;
                _movement.MoveLerp(oldPlayerPosition, vel);
                cooldown = 0;
                exclamation.SetActive(false);
            }

            if (distanceBoom <= maxDistanceBoom)
            {
                expire();
            }
        }
    }

    private void OnEnable()
    {
        Reseto();
    }

    public void DistanceCheck()
    {
        inRange = true;
    }

    public void DistanceCheckFalse()
    {
        inRange = false;
    }

    public void ObjectiveChecked()
    {
        if(objectiveConfirmed == false)
        {
            oldPlayerPosition = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
        }
    }

    public void expire()
    {
        _animator.SetInteger("State", 0);
        boom = PoolingManager.Instance.GetPooledObject("Boom");
        boom.transform.position = gameObject.transform.position;
        boom.SetActive(true);
        boom.GetComponent<ParticleSystem>().Play(true);
        enemy.SetActive(false);
    }

    public void Reseto()
    {
        _animator.SetInteger("State", 0);
        cooldown = maxCooldown;
        oldPlayerPosition = new Vector3(0, 0, 0);
        inRange = false;
        enemy.SetActive(true);
        _idle.IdleModeChange();
        statController.health = savedHealth;
        enemyController.maxHealth = savedHealth;
        enemyController.health = savedHealth;
    }
}
