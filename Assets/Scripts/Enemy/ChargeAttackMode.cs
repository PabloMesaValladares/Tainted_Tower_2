using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttackMode : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movement;

    [SerializeField]
    private Rigidbody _rigid;
    [SerializeField]
    private GameObject player, originalPoint;

    [SerializeField]
    private float vel, timeremaining, distAttack, maxdistAttack, timeBetweenAttacks;
    [SerializeField]
    private bool inRange, returning;
    [SerializeField]
    private Vector3 oldPlayerPosition;


    // Start is called before the first frame update
    void Awake()
    {
        timeremaining = timeBetweenAttacks;
        //inRange = false;
        _rigid.GetComponent<Rigidbody>();
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
            oldPlayerPosition = new Vector3(0,0,0);
            timeremaining = 0;
        }

        if(returning == true)
        {
            if(distAttack != 0)
            {
                _movement.MoveVector(new Vector3(originalPoint.transform.position.x, originalPoint.transform.position.y, originalPoint.transform.position.z), vel * 3);
            }
            else if(distAttack == 0)
            {
                returning = false;
            }
        }

        if (inRange == true)
        {
            timeremaining -= Time.deltaTime;
         
            if (timeremaining <= 0)
            {
                oldPlayerPosition = new Vector3(player.transform.position.x - gameObject.transform.position.x, player.transform.position.y - gameObject.transform.position.y, player.transform.position.z - gameObject.transform.position.z);
                gameObject.transform.LookAt(oldPlayerPosition);
                timeremaining = timeBetweenAttacks;
            }

            if(oldPlayerPosition != null)
            {
                _movement.MoveGameObject(gameObject, oldPlayerPosition, vel);
            }

        }
    }

    void DistanceOriginalPoint()
    {
        distAttack = Vector3.Distance(new Vector3(originalPoint.transform.position.x, gameObject.transform.position.y, originalPoint.transform.position.z), gameObject.transform.position);
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
