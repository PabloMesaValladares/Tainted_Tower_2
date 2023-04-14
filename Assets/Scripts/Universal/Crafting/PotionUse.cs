using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/PotionUse")]

public class PotionUse : ItemUses
{
    public override void Use(GameObject p)
    {
        p.GetComponent<HealthBehaviour>().AddHealthPercent(quantity);
    }
}
