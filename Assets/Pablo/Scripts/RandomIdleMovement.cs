using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleMovement : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movement;
    [SerializeField]
    public float idleVel, timeremaining, maxTime, distFromOrignalPoint, maxDistFromOriginalPoint;
    [SerializeField]
    private int maxRange, randomNumber;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private GameObject originalPoint;
    [SerializeField]
    private bool IdleMode;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(IdleMode == true)
        {
            timeremaining -= Time.deltaTime;

            if (timeremaining <= 0)
            {
                randomNumber = Random.Range(0, maxRange);
                DistanceOriginalPointkChecker();
                directionChecker();
                timeremaining = maxTime;
            }

            if (distFromOrignalPoint >= maxDistFromOriginalPoint)
            {
                gameObject.GetComponent<MovementBehavior>().MoveLerp(originalPoint.transform.position, idleVel);
            }
            else if (distFromOrignalPoint < maxDistFromOriginalPoint)
            {
                gameObject.GetComponent<MovementBehavior>().MoveGameObject(direction, idleVel);
            }
        }
    }

    public void directionChecker()
    {
        if (randomNumber == 0)
        {
            direction = Vector3.forward;
        }
        else if (randomNumber == 1)
        {
            direction = Vector3.left;
        }
        else if (randomNumber == 2)
        {
            direction = Vector3.right;
        }
        else if (randomNumber == 3)
        {
            direction = Vector3.back;
        }
        else if (randomNumber != 0 && randomNumber != 1 && randomNumber != 2 && randomNumber != 3)
        {
            direction = Vector3.zero;
        }
    }

    void DistanceOriginalPointkChecker()
    {
        distFromOrignalPoint = Vector3.Distance(new Vector3(originalPoint.transform.position.x, originalPoint.transform.position.y, originalPoint.transform.position.z), gameObject.transform.position);
    }

    public void IdleModeChange()
    {
        IdleMode = true;
    }

    public void AttackModeChange()
    {
        IdleMode = false;
    }
}
