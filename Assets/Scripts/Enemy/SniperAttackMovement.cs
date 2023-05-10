using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttackMovement : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movement;
    [SerializeField]
    private RandomIdleMovement _idle;
    [SerializeField]
    private StatController statController;
    [SerializeField]
    private Enemy enemyController;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Rigidbody _rigid;
    [SerializeField]
    private GameObject player, originalPoint, bullet, enemy;

    [SerializeField]
    private int savedHealth;
    [SerializeField]
    private float vel, savedvel,timeremaining, distAttack, maxdistAttack, followDist, maxFollowDist, minFollowDist, bulletVel, timeBetweenAttacks, dieFloat;
    [SerializeField]
    private bool inRange, returning, nioMode;
    [SerializeField]
    private Vector3 oldPlayerPosition;


    // Start is called before the first frame update
    void Awake()
    {
        timeremaining = timeBetweenAttacks;
        //inRange = false;
        _rigid.GetComponent<Rigidbody>();
        enemyController = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DistanceOriginalPoint();

        if (distAttack >= maxdistAttack)
        {
            returning = true;
            DistanceCheckFalse();
            timeremaining = 0;
        }

        if (returning == true)
        {
            if (distAttack != 0)
            {
                if (nioMode) _animator.SetInteger("State", 4);
                else if(nioMode == false) _animator.SetInteger("State", 0);
                _movement.MoveVector(new Vector3(originalPoint.transform.position.x, originalPoint.transform.position.y, originalPoint.transform.position.z), vel * 3);
            }
            else if (distAttack == 0)
            {
                returning = false;
            }
        }

        if (inRange == true)
        {
            timeremaining -= Time.deltaTime;
            DistanceAttackChecker();
            gameObject.transform.LookAt(player.transform.position);

            if (followDist >= maxFollowDist && followDist >= minFollowDist)
            {
                if(nioMode) _animator.SetInteger("State", 4);
                _movement.MoveVector(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), vel);
            }

            else if(followDist <= minFollowDist)
            {
                if(nioMode)_animator.SetInteger("State", 4);
                _movement.MoveVector(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), -vel / 4);
            }

            if (timeremaining <= 0 && followDist <= maxFollowDist && followDist >= minFollowDist)
            {
                _animator.SetInteger("State", 2);
                bullet = PoolingManager.Instance.GetPooledObject("Rocky");
                bullet.transform.LookAt(oldPlayerPosition);
                bullet.transform.position = gameObject.transform.position;
                bullet.SetActive(true);
                oldPlayerPosition = new Vector3(player.transform.position.x - bullet.transform.position.x, player.transform.position.y + 0.5f - bullet.transform.position.y, player.transform.position.z - bullet.transform.position.z);
                timeremaining = timeBetweenAttacks;
                //_animator.SetBool("Attack", false);
            }
            else
            {
                _animator.SetInteger("State", 0);
            }
        }

        if (bullet != null)
        {
            _movement.MoveGameObject(bullet, oldPlayerPosition, bulletVel);
        }
    }

    void DistanceOriginalPoint()
    {
        distAttack = Vector3.Distance(new Vector3(originalPoint.transform.position.x, gameObject.transform.position.y, originalPoint.transform.position.z), gameObject.transform.position);
    }

    void DistanceAttackChecker()
    {
        followDist = Vector3.Distance(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), gameObject.transform.position);
    }

    public void DistanceCheck()
    {
        inRange = true;
    }

    public void DistanceCheckFalse()
    {
        inRange = false;
    }

    public void Expire()
    {
        _animator.SetInteger("State", 3);
        vel = 0;
        inRange = false;      
    }

    public void Deactivate()
    {
        timeremaining = timeBetweenAttacks;
        _animator.SetInteger("State", 0);
        enemy.SetActive(false);
    }

    private void OnEnable()
    {
        Reseto();
    }

    public void Hitted()
    {
        _animator.SetInteger("State", 2);
    }

    public void Reseto()
    {
        _animator.SetInteger("State", 0);
        timeremaining = timeBetweenAttacks;
        oldPlayerPosition = new Vector3(0, 0, 0);
        vel = savedvel;
        inRange = false;
        enemy.SetActive(true);
        _idle.IdleModeChange();
        statController.health = savedHealth;
        enemyController.maxHealth = savedHealth;
        enemyController.health = savedHealth;
    }

    public static IEnumerator DieMove(GameObject enemy, float duration)
    {
        Debug.Log("Entre");
        float currentTime = 0;
        while (currentTime < duration)
        {
            Debug.Log("Medio");
            currentTime += Time.deltaTime;
            if (currentTime >= duration)
            {
                Debug.Log("Abajo");
                enemy.SetActive(false);
                yield return null;
            }
        }
        yield break;
    }

}
