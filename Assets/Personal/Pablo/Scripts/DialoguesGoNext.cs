using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialoguesGoNext : MonoBehaviour
{
    [SerializeField]
    private DialoguesBehaviour _Dialogues;
    [SerializeField]
    private BoxCollider range;
    [SerializeField]
    private GameObject ChatOff;

    [SerializeField]
    private int counter, maxCounter;

    [SerializeField]
    private float timer, maxTimer;

    [SerializeField]
    private bool inside, fade;

    InputAction e_x;
    [SerializeField]
    UnityEngine.InputSystem.PlayerInput _config;

    // Start is called before the first frame update
    void Start()
    {
        e_x = _config.actions["Interact"];
    }

    private void Update()
    {
        if (e_x.triggered && inside)
        {
            if (counter < maxCounter)
            {
                ChatOff.SetActive(true);
                _Dialogues.GetText(counter);
                counter++;
                fade = true;
                timer = maxTimer;

                if (counter >= maxCounter)
                {
                    counter = 0;
                }
            }
            
            Debug.Log(counter);
        }

        if(timer > 0 && fade)
        {
            timer -= Time.deltaTime;
            
        }
        if(timer <= 0 && fade)
        {
            ChatOff.SetActive(false);
            fade = false;
        }
        
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        inside = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        inside = false;
    }

}
