using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
    public int health, mana;

    [Header("Equipment")]
    public Weapon mainHand, secondaryHand;

    [Header("Stats")]
    public int strength; // da�o fisico
    public int inteligence; // da�o magico y mana
    public int stamina; // vida
    public int defense;

    //[Header("Damage")]
    [SerializeField] public float damage { get; private set; }
    [SerializeField] private int damageDone;

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.G))
        {
            print("strength: " + strength);
            print("strength after: " + TotalStat(strength));
        }*/
    }

    public int CalculateDmg(int defense, int dmg)
    {

        if(mainHand.mainStatType == Weapon.Type.physical)
        {
            strength = TotalStat(strength);
            damage = (1 + strength * 0.2f) * (dmg / 1.50f);
        }
        else if(mainHand.mainStatType == Weapon.Type.magical)
        {
            inteligence = TotalStat(inteligence);
            damage = (1 + inteligence * 0.2f) * (dmg / 1.50f);
        }

        damageDone = Mathf.FloorToInt(damage - defense * 0.1f);

        return damageDone;
    }

    public void CalculateHealth(int dmg)
    {
        health -= dmg;
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
