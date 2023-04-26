using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrugsMode : MonoBehaviour
{
    public PlayerController playerController;
    public StatController statController;
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
        _config = GetComponent<PlayerInput>();
        interactX = _config.actions["Drug"];

        playerController = GetComponent<PlayerController>();
        statController = GetComponent<StatController>();

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
        if (ready == false)
        {
            cooldown -= Time.deltaTime;
            skillCooldown += Time.deltaTime;

            sliderBar.value = skillCooldown;
            //if (cooldown <= maxCooldown / 2)
            //{
            //    NormalMode();
            //}

            if (cooldown <= 0)
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
            ready = false;

            Invoke(nameof(NormalMode), maxCooldown / 2);
        }
    }

    public void BerserkerMode()
    {
        playerController.animator.SetTrigger("drogas");
        GetComponent<StaminaController>().drugs = true;
        playerController.Rage.Play();
        if (randomNumber < maxRange/2)
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
        playerController.Rage.Stop();

        playerController.walkSpeed = walkSpeed;
        playerController.sprintSpeed = sprintSpeed;

        statController.strength = damage;
        statController.inteligence = damageInt;

        playerController.changeState(playerController.movementSM.currentState);
    }
}
