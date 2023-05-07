using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttackMovement : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movement;

    [SerializeField]
    private Rigidbody _rigid;
    [SerializeField]
    private GameObject player, originalPoint, bullet;

    [SerializeField]
    private float vel, velFly, timeFly, timeremaining, distAttack, maxdistAttack, followDist, maxFollowDist, bulletVel, timeBetweenAttacks;
    [SerializeField]
    private bool flychange, inRange, returning;
    [SerializeField]
    private Vector3 oldPlayerPosition;


    // Start is called before the first frame update
    void Awake()
    {
        timeremaining = timeBetweenAttacks;
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
            DistanceAttackChecker();
            gameObject.transform.LookAt(player.transform.position);

            if (followDist >= maxFollowDist)
            {
                _movement.MoveVector(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z), vel);
            }

            if (timeremaining <= 0)
            {             
                bullet = PoolingManager.Instance.GetPooledObject("Darts");
                bullet.transform.LookAt(oldPlayerPosition);
                bullet.transform.position = gameObject.transform.position;
                bullet.SetActive(true);
                oldPlayerPosition = new Vector3(player.transform.position.x - bullet.transform.position.x, player.transform.position.y + 0.5f - bullet.transform.position.y, player.transform.position.z - bullet.transform.position.z);
                timeremaining = timeBetweenAttacks;
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
}
