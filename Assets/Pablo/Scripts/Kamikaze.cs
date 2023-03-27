using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
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
}
