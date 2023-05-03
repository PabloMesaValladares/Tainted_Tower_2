using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttackMode : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movement;
    [SerializeField]
    private RandomIdleMovement _idle;
    [SerializeField]
    private StatController statController;

    [SerializeField]
    private Rigidbody _rigid;
    [SerializeField]
    private GameObject player, forceObject, enemy;

    [SerializeField]
    private float vel, force, timeremaining, distAttack, maxdistAttack, timeBetweenAttacks;
    [SerializeField]
    private bool inRange, returning;
    [SerializeField]
    private Vector3 oldPlayerPosition;
    [SerializeField]
    private Transform originalPoint;
    [SerializeField]
    private int savedHealth;


    // Start is called before the first frame update
    void Awake()
    {
        timeremaining = timeBetweenAttacks / 2;
        _rigid = GetComponent<Rigidbody>();
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
            oldPlayerPosition = new Vector3(0, 0, 0);
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
         
            if (timeremaining < 4 && timeremaining > 0)
            {
                oldPlayerPosition = new Vector3(player.transform.position.x , gameObject.transform.position.y, player.transform.position.z);
                transform.LookAt(new Vector3(oldPlayerPosition.x, transform.position.y, oldPlayerPosition.z));
                _rigid.AddForce(transform.forward * force, ForceMode.Force);
                forceObject.GetComponent<PushPlayer>().dirToGo = transform.forward;
            }

            if (timeremaining <= 0)
            {
                forceObject.SetActive(false);
                timeremaining = timeBetweenAttacks;
            }

            if(timeremaining <= 4.2 && timeremaining >= 4)
            {
                forceObject.SetActive(true);
            }

        }
    }

    private void OnEnable()
    {
        Reseto();
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

    public void ChangeTime()
    {
        timeremaining = 0.2f;
    }

    public void Expire()
    {
        timeremaining = 0;
        enemy.SetActive(false);
    }

    public void Reseto()
    {
        timeremaining = 5;
        forceObject.SetActive(true);
        oldPlayerPosition = new Vector3(0, 0, 0);
        inRange = false;
        enemy.SetActive(true);
        _idle.IdleModeChange();
        statController.health = savedHealth;
    }


}
