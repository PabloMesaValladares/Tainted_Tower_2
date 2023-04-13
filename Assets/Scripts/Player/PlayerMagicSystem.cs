using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMagicSystem : MonoBehaviour
{
    [SerializeField] private Spell spellToCast;

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 2f;
    [SerializeField] private float timeBetweenCast = 1.5f;
    private float currentCastTimer;

    [SerializeField] private Transform castPoint;

    private bool castingMagic = false;

    PlayerInput playerControls;

    InputAction ShootBall;

    private void Awake()
    {
        playerControls = GetComponent<PlayerInput>();

        ShootBall = playerControls.actions["Ball"];
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        currentMana = maxMana;
    }

    private void Update()
    {
        bool isSpellCastHeldDown = ShootBall.IsPressed();
        bool hasEnoughMana = currentMana - spellToCast.SpellToCast.ManaCost >= 0f; ;

        if (!castingMagic && isSpellCastHeldDown && hasEnoughMana)
        {
            castingMagic = true;
            currentMana -= spellToCast.SpellToCast.ManaCost;

            currentCastTimer = 0;
            CastSpell();
        }

        if (castingMagic)
        {
            currentCastTimer += Time.deltaTime;

            if (currentCastTimer > timeBetweenCast)
            {
                castingMagic = false;
            }
        }

        if (currentMana < maxMana && !castingMagic && !isSpellCastHeldDown)
        {
            currentMana += manaRechargeRate * Time.deltaTime;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
        }
    }

    void CastSpell()
    {
        //CAST OUR SPELL

        Instantiate(spellToCast, castPoint.position, castPoint.rotation);
    }
}
