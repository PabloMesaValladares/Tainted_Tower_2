using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movement;
    [SerializeField]
    private GameObject player, boom, exclamation;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float vel, distanceBoom, maxDistanceBoom, cooldown, maxCooldown;
    [SerializeField]
    private bool inRange, objectiveConfirmed;
    [SerializeField]
    private Vector3 oldPlayerPosition;

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
            _animator.SetBool("Attack", true);
            exclamation.SetActive(true);

            if (cooldown <= 0)
            {
                objectiveConfirmed = true;
                _movement.MoveLerp(oldPlayerPosition, vel);
                //_movement.MoveGameObject(gameObject, oldPlayerPosition, vel);
                cooldown = 0;
                exclamation.SetActive(false);
            }

            if (distanceBoom <= maxDistanceBoom)
            {
                boom.transform.position = gameObject.transform.position;
                boom.SetActive(true);
                cooldown = maxCooldown;
                gameObject.SetActive(false);
            }
        }
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
            //oldPlayerPosition = new Vector3(player.transform.position.x - gameObject.transform.position.x, player.transform.position.y - gameObject.transform.position.y, player.transform.position.z - gameObject.transform.position.z);
        }
    }



    /*
    [SerializeField]
    private MovementBehavior _movement;

    [SerializeField]
    private Rigidbody _rigid;

    [SerializeField]
    private float vel, attackVel, distanceBoom;
    [SerializeField]
    private bool fighting;
    [SerializeField]
    private Vector3 oldPlayerPosition;
    [SerializeField]
    private GameObject _boomParticles;


    [SerializeField]
    private GameObject player, enemy;
    // Start is called before the first frame update
    void Awake()
    {
        _rigid.GetComponent<Rigidbody>();
        enemy = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(fighting)
        {
            distanceBoom = Vector3.Distance(oldPlayerPosition, transform.position);

            if(distanceBoom <= 2)
            {
                _boomParticles = PoolingManager.Instance.GetPooledObject("boomPar");
                _boomParticles.transform.position = gameObject.transform.position;
                _boomParticles.SetActive(true);
                enemy.SetActive(false);
            }

            _rigid.AddForce(transform.forward * attackVel, ForceMode.Force);

        }
    }

    public void AttackCheck()
    {
        transform.LookAt(player.transform.position);
        _rigid.velocity = Vector3.zero;
        oldPlayerPosition = new Vector3(player.transform.position.x , transform.position.y, player.transform.position.z);
        fighting = true;
    }

    */
}
