using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private int dmgDone;
    private StatController _sc;

    void Start()
    {
        _sc = GetComponentInParent<StatController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StatController _enemyStat))
        {
            dmgDone = _sc.CalculateDmg(_enemyStat.GetDefense(), weapon.damage);
            _enemyStat.CalculateHealth(dmgDone);
        }
    }
}
