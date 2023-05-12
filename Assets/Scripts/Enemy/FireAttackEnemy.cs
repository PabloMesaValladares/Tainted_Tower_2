using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private ParticleSystem attackFire;


    [SerializeField]
    private Vector3 playerCheck;

    public bool attack = false;
    public float timer;
    public float timerSave;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            gameObject.transform.LookAt(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.transform.position.z));

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                playerCheck = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

                attackFire.Stop();
                attackFire.transform.position = playerCheck;
                attackFire.Play();

                timer = timerSave;
            }
        }
    }

    public void attackMode()
    {
        attack = true;

    }
    public void attackModeOff()
    {
        attack = false;

    }
}
