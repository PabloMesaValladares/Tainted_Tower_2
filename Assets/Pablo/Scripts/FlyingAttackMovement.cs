using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAttackMovement : MonoBehaviour
{
    [SerializeField]
    private GroundCheck _groundedcheking;
    [SerializeField]
    private MovementBehavior _movement;

    [SerializeField]
    private Rigidbody _rigid;
    [SerializeField]
    private GameObject player, originalPoint, bullet;

    [SerializeField]
    private float vel, velFly, timeFly, timeremaining, distAttack, maxdistAttack, followDist, maxFollowDist, bulletVel, timeremaining2, timeBetweenAttacks;
    [SerializeField]
    private bool flychange, inRange, returning;
    [SerializeField]
    private Vector3 oldPlayerPosition;


    // Start is called before the first frame update
    void Awake()
    {
        timeremaining2 = timeBetweenAttacks;
        _groundedcheking.GetComponent<GroundCheck>();
        //inRange = false;
        _rigid.GetComponent<Rigidbody>();
        timeremaining = timeFly;
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
            timeremaining2 = 0;
        }

        if (returning == true)
        {
            if (distAttack != 0)
            {
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
            timeremaining2 -= Time.deltaTime;
            DistanceAttackChecker();
            gameObject.transform.LookAt(player.transform.position);

            if (timeremaining <= 0)
            {
                flyChecker();
                timeremaining = timeFly;
            }
            if (flychange)
            {
                _movement.MoveRigidBody(gameObject, Vector2.up, velFly);
            }
            if (!flychange)
            {
                _movement.MoveRigidBody(gameObject, Vector2.down, velFly);
            }

            if (followDist >= maxFollowDist)
            {
                _movement.MoveVector(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), vel);
            }

            if (timeremaining2 <= 0)
            {             
                bullet = PoolingManager.Instance.GetPooledObject("Darts");
                bullet.transform.LookAt(oldPlayerPosition);
                bullet.transform.position = gameObject.transform.position;
                bullet.SetActive(true);
                oldPlayerPosition = new Vector3(player.transform.position.x - bullet.transform.position.x, player.transform.position.y - bullet.transform.position.y, player.transform.position.z - bullet.transform.position.z);
                timeremaining2 = timeBetweenAttacks;
            }
        }

        if (bullet != null)
        {
            _movement.MoveGameObject(bullet, oldPlayerPosition, bulletVel);
            //_movement.MoveGameObject(bullet, player.transform.position, bulletVel);
        }
    }

    void flyChecker()
    {
        flychange = _groundedcheking.returnCheck();
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
}
