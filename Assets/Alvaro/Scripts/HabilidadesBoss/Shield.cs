using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shield : MonoBehaviour
{
    UnityAction updateShieldNumber;

    public int sIndex;
    public int hitPoints;

    void OnEnable()
    {

    }

    void onHurt()
    {

    }

    void onBreakShield()
    {
        updateShieldNumber.Invoke();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
