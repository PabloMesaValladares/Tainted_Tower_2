using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField]
    private GroundCheck _groundedcheking;
    [SerializeField]
    private MovementBehavior _movement;

    [SerializeField]
    private Rigidbody _rigid;

    [SerializeField]
    private float vel, returnVel, attackVel, timeFly, timeremaining, dist, maxdist;
    [SerializeField]
    private bool activated, flychange, farAway, fighting, attacking;
    [SerializeField]
    private Transform player, originalPoint;
    [SerializeField]
    private Vector3 oldPlayerPosition;
    [SerializeField]
    private GameObject enemy;
    // Start is called before the first frame update
    void Awake()
    {
        _groundedcheking.GetComponent<GroundCheck>();
        _rigid.GetComponent<Rigidbody>();
        timeremaining = timeFly;
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
            _movement.MoveGameObject(gameObject, oldPlayerPosition, attackVel);
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
        oldPlayerPosition = new Vector3(player.transform.position.x - gameObject.transform.position.x, player.transform.position.y - gameObject.transform.position.y, player.transform.position.z - gameObject.transform.position.z);
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<PL>(out PL _pl))
        {
            enemy.SetActive(false);
        }
    }
}
