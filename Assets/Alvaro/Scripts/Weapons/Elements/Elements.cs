using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Items/Elements"))]
public class Elements : ScriptableObject
{
    public enum Element
    {
        normal,
        water,
        earth,
        wind,
        fire
    };

    [SerializeField] public float buffPercentage;
    [SerializeField] public float debuffPercentage;

    private int damageLeft;

    public Element currentElement;
    public Element compareElementBuff;
    public Element compareElementDebuff;

    public int DamageBuff(Element rE, int dmg)
    {
        if (compareElementBuff == rE && rE != Element.normal)
        {
            damageLeft = Mathf.FloorToInt(dmg * (buffPercentage / 100.0f));
        }

        return damageLeft;
    }

    public int DamageDebuff(Element rE, int dmg)
    {
        if (compareElementDebuff == rE && rE != Element.normal)
        {
            damageLeft = Mathf.FloorToInt(dmg * (debuffPercentage / 100.0f));
        }

        return damageLeft;
    }

}
