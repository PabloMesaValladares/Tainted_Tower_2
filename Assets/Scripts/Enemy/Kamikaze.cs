using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movement;
    [SerializeField]
    private GameObject player, boom;

    [SerializeField]
    private float vel, distanceBoom, maxDistanceBoom;
    [SerializeField]
    private bool inRange;
    [SerializeField]
    private Vector3 oldPlayerPosition;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange == true)
        {
            oldPlayerPosition = new Vector3(player.transform.position.x - gameObject.transform.position.x, player.transform.position.y - gameObject.transform.position.y, player.transform.position.z - gameObject.transform.position.z);
            distanceBoom = Vector3.Distance(player.transform.position, gameObject.transform.position);

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            if (distanceBoom <= maxDistanceBoom)
            {
                //_boomParticles = PoolingManager.Instance.GetPooledObject("boomPar");
                boom.transform.position = gameObject.transform.position;
                boom.SetActive(true);
                gameObject.SetActive(false);
            }
            else if(distanceBoom > maxDistanceBoom)
            {
                _movement.MoveGameObject(gameObject, oldPlayerPosition, vel);
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
