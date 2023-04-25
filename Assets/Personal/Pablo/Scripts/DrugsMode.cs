using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrugsMode : MonoBehaviour
{
    public PlayerController playerController;
    public StatController statController;

    public bool ready;
    public float cooldown, maxCooldown;

    public float walkSpeed, walkSpeedBuff, walkSpeedDebuff;
    public float sprintSpeed, sprintSpeedBuff, sprintSpeedDebuff;

    public int damage, damageInt, damageBuff, damageDebuff, randomNumber, maxRange;

    InputAction interactX;
    [SerializeField]
    UnityEngine.InputSystem.PlayerInput _config;

    // Start is called before the first frame update
    void Start()
    {
        _config = GetComponent<PlayerInput>();
        interactX = _config.actions["Drug"];

        playerController = GetComponent<PlayerController>();
        statController = GetComponent<StatController>();

        walkSpeed = playerController.walkSpeed;
        sprintSpeed = playerController.sprintSpeed;

        damage = statController.strength;
        damageInt = statController.inteligence;

    }

    // Update is called once per frame
    void Update()
    {
        if (ready == false)
        {
            cooldown -= Time.deltaTime;

            //if (cooldown <= maxCooldown / 2)
            //{
            //    NormalMode();
            //}

            if (cooldown <= 0)
            {
                cooldown = maxCooldown;
                ready = true;
            }
        }

        if (interactX.triggered && ready)
        {
            randomNumber = Random.Range(0, maxRange);
            BerserkerMode();
            
            ready = false;

            Invoke(nameof(NormalMode), maxCooldown / 2);
        }
    }

    public void BerserkerMode()
    {
        GetComponent<StaminaController>().drugs = true;
        GetComponent<PlayerController>().torsoAnimator.SetTrigger("drogas");
        GetComponent<PlayerController>().Rage.Play();
        if (randomNumber <= 0)
        {
            playerController.walkSpeed = walkSpeedDebuff;
            playerController.sprintSpeed = sprintSpeedDebuff;

            statController.strength = damage + damageDebuff;
            statController.inteligence = damageInt + damageDebuff;
        }
        else
        {
            playerController.walkSpeed = walkSpeedBuff;
            playerController.sprintSpeed = sprintSpeedBuff;

            statController.strength = damage + damageBuff;
            statController.inteligence = damageInt + damageBuff;
        }

        playerController.changeState(playerController.movementSM.currentState);
    }

    public void NormalMode()
    {
        GetComponent<StaminaController>().drugs = false;
        GetComponent<PlayerController>().torsoAnimator.ResetTrigger("drogas");
        GetComponent<PlayerController>().Rage.Stop();

        playerController.walkSpeed = walkSpeed;
        playerController.sprintSpeed = sprintSpeed;

        statController.strength = damage;
        statController.inteligence = damageInt;

        playerController.changeState(playerController.movementSM.currentState);
    }
}
