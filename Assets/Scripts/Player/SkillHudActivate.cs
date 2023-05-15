using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHudActivate : MonoBehaviour
{
    public GameObject grab, fire, berserk, pilar;

    private void Start()
    {
        grab.SetActive(GameManager.instance.grapple);

        fire.SetActive(GameManager.instance.fireball);

        berserk.SetActive(GameManager.instance.drugs);
   
        pilar.SetActive(GameManager.instance.pilar);
    }

    public void activateGrab()
    {
        grab.SetActive(true);
    }
    public void activateFire()
    {
        fire.SetActive(true);
    }
    public void activateBers()
    {
        berserk.SetActive(true);
    }
    public void activatePilar()
    {
        GameManager.instance.pilar = true;
        pilar.SetActive(true);
    }
}
