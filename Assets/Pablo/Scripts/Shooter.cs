using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private MovementBehavior _movement;
    [SerializeField]
    private GameObject bullet, player;

    [SerializeField]
    private float bulletVel, timeremaining, timeBetweenAttacks;
    [SerializeField]
    private bool fighting;
    [SerializeField]
    private Vector3 oldPlayerPosition;
    // Start is called before the first frame update
    void Awake()
    {
        timeremaining = timeBetweenAttacks;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (fighting)
        {          
            transform.LookAt(player.transform.position);
  
            if(timeremaining <= 0)
            {
                timeremaining = timeBetweenAttacks;
                bullet = PoolingManager.Instance.GetPooledObject("bullet");
                bullet.transform.LookAt(oldPlayerPosition);
                bullet.transform.position = gameObject.transform.position;
                bullet.SetActive(true);
            }

            timeremaining -= Time.deltaTime;
        }

        if (timeremaining <= 0)
        {
            oldPlayerPosition = new Vector3(player.transform.position.x - bullet.transform.position.x, player.transform.position.y - bullet.transform.position.y, player.transform.position.z - bullet.transform.position.z);
        }


        if (bullet != null)
        {
            _movement.MoveGameObject(bullet, oldPlayerPosition, bulletVel);
        }
    }

    public void AttackCheck()
    {
        fighting = true; 
    }

    public void AttackCheckFalse()
    {
        fighting = false;
    }

}
