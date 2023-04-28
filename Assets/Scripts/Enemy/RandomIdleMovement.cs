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
    private int maxRange, randomNumber, randomGrade, maxRangeGrade, speedRotation;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private GameObject originalPoint;
    [SerializeField]
    private bool IdleMode, canMove;

    private void Start()
    {
        _movement = GetComponent<MovementBehavior>();
    }

    void Update()
    {
        if(IdleMode == true)
        {
            timeremaining -= Time.deltaTime;

            if (timeremaining <= 0)
            {
                randomNumber = Random.Range(0, maxRange);
                randomGrade = Random.Range(0, maxRangeGrade);
                DistanceOriginalPointkChecker();
                directionChecker();
                timeremaining = maxTime;
            }

            if (distFromOrignalPoint >= maxDistFromOriginalPoint)
            {
                gameObject.transform.LookAt(new Vector3(originalPoint.transform.position.x, gameObject.transform.position.y, originalPoint.transform.position.z));
                _movement.MoveLerp(new Vector3(originalPoint.transform.position.x, gameObject.transform.position.y, originalPoint.transform.position.z), idleVel * 2);
            }

            else if (distFromOrignalPoint < maxDistFromOriginalPoint)
            {
                gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(0, randomGrade, 0), timeremaining / speedRotation);
                if(canMove)_movement.MoveGameObject(transform.forward, idleVel);

            }
        }
    }

    public void directionChecker()
    {
        if (randomNumber == 0)
        {
            canMove = true;
        }
        else if(randomNumber != 0)
        {
            canMove = false;
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
