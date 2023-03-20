using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    [SerializeField]
    private GroundCheck _groundedcheking;
    [SerializeField]
    private MovementBehavior _movement;
    [SerializeField]
    private ShotBehaviour _shot;
    [SerializeField]
    private PoolingManager _pool;
    [SerializeField]
    private Rigidbody _rigid;
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float vel, returnVel, attackVel, timeFly, bulletVel, timeremaining, timeBetweenAttacks, timeremaining2, dist, maxdist, distAttack, maxdistAttack;
    [SerializeField]
    private bool activated, flychange, farAway, fighting, attacking;
    [SerializeField]
    private Transform player, originalPoint;
    [SerializeField]
    private Vector3 oldPlayerPosition;
    // Start is called before the first frame update
    void Awake()
    {
        _groundedcheking.GetComponent<GroundCheck>();
        _rigid.GetComponent<Rigidbody>();
        _shot.GetComponent<ShotBehaviour>();
        _pool.GetComponent<PoolingManager>();
        timeremaining = timeFly;
        timeremaining2 = timeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        timeremaining -= Time.deltaTime;

        //Cambios de altura
        if(timeremaining <= 0)
        {
            flyChecker();
            DistanceChecker();
            timeremaining = timeFly;  
        }
        if(flychange)
        {
            _movement.MoveRigidBody(gameObject, Vector2.up, vel);
        }
        if(!flychange)
        {
            _movement.MoveRigidBody(gameObject, Vector2.down, vel);
        }
        //Volver al punto Original
        if(farAway)
        {
            _movement.MoveVector(originalPoint.transform.position, returnVel);
        }

        if(fighting)
        {          
            transform.LookAt(player);
            DistanceAttackChecker();
            if(!attacking)
            {
                if(distAttack >= maxdistAttack)
                {
                    _movement.MoveVector(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), attackVel);
                }
            }

            if(timeremaining2 <= 0)
            {
                timeremaining2 = timeBetweenAttacks;
                attacking = true;
                //bullet = _shot.EnableBullet();
                bullet = _pool.GetPooledObject("bullet");
                bullet.SetActive(true);
                bullet.transform.position = gameObject.transform.position;
                oldPlayerPosition = new Vector3(player.transform.position.x - bullet.transform.position.x, player.transform.position.y - bullet.transform.position.y, player.transform.position.z - bullet.transform.position.z);
            }

            if(bullet != null)
            {
                _movement.MoveGameObject(bullet, oldPlayerPosition, bulletVel);
            }

            timeremaining2 -= Time.deltaTime;
        }

    }

    public void ICantFlyAnyMore()
    {
        _rigid.useGravity = true;
    }

    public void ICanFlyAgain()
    {
        _rigid.useGravity = false;
    }

    public void AttackCheck()
    {
        fighting = true; 
    }

    void flyChecker()
    {
        flychange = _groundedcheking.returnCheck();
    }

    void DistanceChecker()
    {
        dist = Vector3.Distance(originalPoint.transform.position, gameObject.transform.position);
        if(dist >= maxdist)
        {
            farAway = true;
            transform.LookAt(originalPoint);
            fighting = false;
        }
        else
        {
            farAway = false;
        }
    }

    void DistanceAttackChecker()
    {
        distAttack = Vector3.Distance(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), gameObject.transform.position);
    }


    //Instrucciones/Recordatorio
    //ShotBehaviour es llamado por Wasp, para activar una bala,
    //despues la bala es dada a Wasp y esta llama a MovementBehaviour para que mueva la vala en la antigua posicion del player.


}
