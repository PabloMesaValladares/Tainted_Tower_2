using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrugsMode : MonoBehaviour
{
    public PlayerController playerController;
    public StatController statController;
    public RageEffects particlesController;
    public Slider sliderBar;

    public bool ready;
    public float cooldown, maxCooldown, skillCooldown, maxSkillCooldown;

    public float walkSpeed, walkSpeedBuff, walkSpeedDebuff;
    public float sprintSpeed, sprintSpeedBuff, sprintSpeedDebuff;

    public int damage, damageInt, damageBuff, damageDebuff, randomNumber, maxRange;

    InputAction interactX;
    [SerializeField]
    UnityEngine.InputSystem.PlayerInput _config;

    // Start is called before the first frame update
    void Start()
    {
        interactX = _config.actions["Drug"];

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        statController = GameObject.FindGameObjectWithTag("Player").GetComponent<StatController>();
        particlesController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Rage;
        sliderBar = GameObject.FindGameObjectWithTag("BerserkerCool").GetComponent<Slider>();

        walkSpeed = playerController.walkSpeed;
        sprintSpeed = playerController.sprintSpeed;

        damage = statController.strength;
        damageInt = statController.inteligence;
        sliderBar.maxValue = maxCooldown;
        sliderBar.value = maxCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(ready == false)
        {
            cooldown -= Time.deltaTime;
            skillCooldown += Time.deltaTime;

            sliderBar.value = skillCooldown;

            if (cooldown <= maxCooldown/2)
            {
                NormalMode();
                particlesController.Stop();
            }

            if(cooldown <= 0)
            {
                cooldown = maxCooldown;
                skillCooldown = maxCooldown;
                sliderBar.value = maxCooldown;
                ready = true;
            }
        }

        if (interactX.triggered && ready)
        {
            randomNumber = Random.Range(0, maxRange);
            BerserkerMode();
            skillCooldown = 0;
            particlesController.Play();
            ready = false;
        }
    }

    public void BerserkerMode()
    {
        if(randomNumber <= 0)
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
        playerController.walkSpeed = walkSpeed;
        playerController.sprintSpeed = sprintSpeed;

        statController.strength = damage;
        statController.inteligence = damageInt;

        playerController.changeState(playerController.movementSM.currentState);
    }
}
