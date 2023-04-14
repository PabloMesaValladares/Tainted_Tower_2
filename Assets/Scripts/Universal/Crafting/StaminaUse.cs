using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/StaminaUse")]

public class StaminaUse : ItemUses
{
    public override void Use(GameObject p)
    {
        p.GetComponent<StaminaController>().addStaminaPerc(quantity);
    }
}
