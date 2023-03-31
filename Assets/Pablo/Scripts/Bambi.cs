using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bambi : MonoBehaviour
{

    [SerializeField]
    private MovementBehavior _movement;

    [SerializeField]
    private float vel, attackVel, timeremaining, timeremainingSaved, atackTime, atackTimeSaved, dist, maxdist;
    [SerializeField]
    private bool farAway, fighting;
    [SerializeField]
    private Vector3 oldPlayerPosition;
    [SerializeField]
    private Vector3 thisPos;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        timeremaining -= Time.deltaTime;
        oldPlayerPosition = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);

        if (timeremaining <= 0)
        {
            //DistanceChecker();
            timeremaining = timeremainingSaved;
        }
        /*Volver al punto Original
        if (farAway)
        {
            _movement.MoveVector(originalPoint.transform.position, returnVel);
        }
        */
        if(fighting)
        {
            atackTime -= Time.deltaTime;
            //transform.LookAt(oldPlayerPosition);
            
            if(atackTime <= 0)
            {
                thisPos = transform.position;
                //oldPlayerPosition = new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z);
                atackTime = atackTimeSaved;
            }

            _movement.MoveVector(oldPlayerPosition, attackVel);
            //_movement.MoveGameObject(gameObject,oldPlayerPosition,attackVel);
        }
    }

    public void AttackCheck()
    {
        fighting = true;
        atackTime = 0;
    }
    /*
    void DistanceChecker()
    {
        dist = Vector3.Distance(originalPoint.transform.position, gameObject.transform.position);
        if (dist >= maxdist)
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
    */
}
