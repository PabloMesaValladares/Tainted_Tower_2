using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField]
    private GroundCheck _groundedcheking;
    [SerializeField]
    private MovementBehavior _movement;

    [SerializeField]
    private Rigidbody _rigid;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float vel, velFly, timeFly, timeremaining, distAttack, maxdistAttack;
    [SerializeField]
    private bool flychange, inRange;


    // Start is called before the first frame update
    void Awake()
    {
        _groundedcheking.GetComponent<GroundCheck>();
        inRange = false;
        _rigid.GetComponent<Rigidbody>();
        timeremaining = timeFly;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timeremaining -= Time.deltaTime;
        DistanceAttackChecker();

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

        if(inRange == true)
        {
            if (distAttack >= maxdistAttack)
            {
                _movement.MoveVector(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), vel);
            }
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

    void flyChecker()
    {
        flychange = _groundedcheking.returnCheck();
    }

    void DistanceAttackChecker()
    {
        distAttack = Vector3.Distance(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), gameObject.transform.position);
    }

    public void DistanceCheck()
    {
        inRange = true;
    }

}
