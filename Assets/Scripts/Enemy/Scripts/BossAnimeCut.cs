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
    [SerializeField] Vector3 posToGo;
    [Header("Debug")]
    [SerializeField] float counter;
    Rigidbody rb;

    public float dashCoold;
    public float dashForce;
    public float dashUpwardForce;

    [SerializeField]
    bool look;
    [SerializeField]
    bool touched;
    [SerializeField]
    float distEffEn;
    public int damage;

    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        look = true;
        rb = GetComponent<Rigidbody>();
        //player = GameObject.FindGameObjectWithTag("Player");
        counter = 0;
        playerPos = player.transform.position;
    }

    private void OnEnable()
    {
        CutAttack.MoveEffect += DoSlash;
    }

    float currentTime;
    Vector3 startingPos;
    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(playerPos);

        if (!look)
        {
            
            if(currentTime < duration)
            {
                currentTime += Time.deltaTime;
                rb.MovePosition(Vector3.Lerp(startingPos, posToGo, currentTime / duration));
            }
            else
            {
                LookAround();
                StopDash();
            }
        }

    }

    public void DoSlash()
    {
        currentTime = 0;
        look = false;
        posToGo = playerPos;
        transform.LookAt(playerPos);
        startingPos = transform.position;
        touched = false;
    }

    public void LookAround()
    {
        look = true;
        rb.velocity = Vector3.zero;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > dashForce)
        {
            Vector3 limitedVel = flatVel.normalized * dashForce;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    void StopDash()
    {
        rb.velocity = Vector3.zero;
        counter = 0;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!touched)
            other.GetComponentInParent<HealthBehaviour>().Hurt(damage);

        touched = true;
        Debug.Log("Tocadito");
    }
}
