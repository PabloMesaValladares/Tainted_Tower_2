using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bambi : MonoBehaviour
{

    [SerializeField]
    private MovementBehavior _movement;

    [SerializeField]
    private float vel, returnVel, attackVel, timeremaining, timeremainingSaved, atackTime, atackTimeSaved, dist, maxdist;
    [SerializeField]
    private bool activated, farAway, fighting, attacking;
    [SerializeField]
    private Transform player, originalPoint;
    [SerializeField]
    private Vector3 oldPlayerPosition;
    [SerializeField]
    private GameObject enemy;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        timeremaining -= Time.deltaTime;

        if (timeremaining <= 0)
        {
            DistanceChecker();
            timeremaining = timeremainingSaved;
        }
        //Volver al punto Original
        if (farAway)
        {
            _movement.MoveVector(originalPoint.transform.position, returnVel);
        }

        if(fighting)
        {
            atackTime -= Time.deltaTime;
            transform.LookAt(player);
            
            if(atackTime <= 0)
            {
                oldPlayerPosition = new Vector3(player.transform.position.x - gameObject.transform.position.x, gameObject.transform.position.y, player.transform.position.z - gameObject.transform.position.z);
                atackTime = atackTimeSaved;
            }

            _movement.MoveGameObject(gameObject, oldPlayerPosition, attackVel);
        }
    }

    public void AttackCheck()
    {
        fighting = true;
        atackTime = 0;
    }

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

    /*
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<PL>(out PL _pl))
        {
            enemy.SetActive(false);
        }
    }
    */
}
