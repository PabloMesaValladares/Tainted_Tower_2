using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMagicSystem : MonoBehaviour
{
    /*[SerializeField] private Spell spellToCast;

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 2f;
    [SerializeField] private float timeBetweenCast = 1.5f;
    private float currentCastTimer;

    [SerializeField] private Transform castPoint;

    private bool castingMagic = false;

    private PlayerController playerControls;
    

    private void Awake()
    {
        playerControls = new PlayerController();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();

        currentMana = maxMana;
    }

    private void Update()
    {
        bool isSpellCastHeldDown = playerControls.Controls.SpellCast.ReadValue<float>() > 0.1;
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
    }*/
}
