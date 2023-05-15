using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SkillGetter : MonoBehaviour
{

    public Vector3 posUpDoor;
    public float duration;
    public UnityEvent Activate;
    Animator anim;
    public PlayerInput inputs;
    InputAction activate;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if(GameManager.instance.grapple)
        {
            Activate.Invoke();
        }
        activate = inputs.actions["Interact"];
    }
    public void GetSkillGrapple()
    {
        GameManager.instance.grapple = true;
    }

    public void GetSkillFire()
    {
        GameManager.instance.fireball = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.TryGetComponent<PlayerController>(out PlayerController controller))
        {
            if (activate.triggered)
            {
                Activate.Invoke(); 
                activateAnimation();
                GetComponent<SphereCollider>().enabled = false;
            }
        }
    }

    public void activateAnimation()
    {
        anim.SetTrigger("open");
    }

    public void StartMove(GameObject g)
    {
        StartCoroutine(startMove(g, new Vector3(g.transform.position.x, posUpDoor.y, g.transform.position.z), duration));
    }
    public static IEnumerator startMove(GameObject door, Vector3 newPos, float duration)
    {
        float currentTime = 0;
        Vector3 start = door.transform.position;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            door.transform.position = Vector3.Lerp(start, newPos, currentTime);
            yield return null;
        }
        yield break;
    }
}
