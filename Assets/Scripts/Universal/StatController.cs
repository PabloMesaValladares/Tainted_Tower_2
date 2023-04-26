using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public int health, totalMana, baseMana;

    [Header("Equipment")]
    public Weapon mainHand, secondaryHand;

    [Header("Stats")]
    public int strength; // daño fisico
    public int inteligence; // daño magico y mana
    public int stamina; // correr
    public int defense;

    [Header("Damage")]
    [SerializeField] public float damage;
    [SerializeField] private int damageDone;



    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.G))
        {
            print("strength: " + strength);
            print("strength after: " + TotalStat(strength));
        }*/
    }

    public int CalculateDmgBasic(int dmg)
    {
        strength = TotalStat(strength);
        damage = (1 + strength * 0.2f) * (dmg / 1.50f);

        damageDone = Mathf.FloorToInt(damage - defense * 0.1f);

        return damageDone;
    }

    public int CalculateDmg(int dmg, int defense)
    {
        switch(mainHand.mainStatType)
        {
            case Weapon.Type.physical:
                strength = TotalStat(strength);
                damage = (1 + strength * 0.2f) * (dmg / 1.50f);
                break;
            case Weapon.Type.magical:
                inteligence = TotalStat(inteligence);
                damage = (1 + inteligence * 0.15f) * (dmg / 1.50f);
                break;
            default:
                break;
        }

        damageDone = Mathf.FloorToInt(damage - defense * 0.1f);

        return damageDone;
    } 

    public void CalculateMana()
    {
        totalMana = Mathf.FloorToInt(baseMana + inteligence * 1.2f);
    }

    public void CalculateHealth(int dmg)
    {
        //health behaviour
        //health -= dmg;
    }

    public int GetDefense()
    {
        return defense;
    }

    private int TotalStat(int rS)
    {
        int stat = rS + mainHand.mainStat + secondaryHand.mainStat;

        return stat;
    }
}
