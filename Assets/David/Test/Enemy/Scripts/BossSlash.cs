using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlash : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject player;
    public GameObject slash;
    [Header("Parameters")]
    public float SlashAttackCounter;
    [SerializeField] Vector3 playerPos;
    [Header("Debug")]
    [SerializeField]float counter;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(playerPos);

        if (counter > SlashAttackCounter)
        {
            GameObject attack = Instantiate(slash, transform, false);
            attack.SetActive(true);
            attack.transform.rotation = transform.rotation;
            attack.GetComponent<SlashMovement>().MoveDirection(playerPos);
            counter = 0;
        }
        else
            counter += Time.deltaTime;
    }
}
