using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimeCut : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject player;
    [Header("Parameters")]
    public float attackCounter;
    [SerializeField] Vector3 playerPos;
    [Header("Debug")]
    [SerializeField] float counter;
    Rigidbody rb;

    public float dashCoold;
    public float dashForce;
    public float dashUpwardForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (counter > attackCounter)
        {
            Vector3 forceToApply = transform.forward * dashForce + transform.up * dashUpwardForce;
            rb.AddForce(forceToApply, ForceMode.Impulse);
            Invoke(nameof(StopDash), dashCoold);
        }
        else
        {
            playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.LookAt(playerPos);
            counter += Time.deltaTime;
        }
    }

    void StopDash()
    {
        rb.velocity = Vector3.zero;
        counter = 0;
    }
}
