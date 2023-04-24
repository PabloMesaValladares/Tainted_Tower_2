using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnlockAbility : MonoBehaviour
{
    private PlayerController playerController;

    InputAction interactE;
    [SerializeField]
    UnityEngine.InputSystem.PlayerInput _config;

    [SerializeField]
    private bool inside;

    [SerializeField]
    private GameObject orb, door;
    [SerializeField]
    private Vector3 doorUp;
    [SerializeField]
    private float timeFade;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        interactE = _config.actions["Interact"];
    }

    private void Update()
    {
        if (interactE.triggered && inside)
        {
            //Llamar al GameManager
            playerController.GetComponent<DrugsMode>().enabled = true;
            orb.SetActive(false);
            gameObject.SetActive(false);
            gameObject.GetComponent<UnlockAbility>().enabled = false;
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

    public void OpenTheDoors()
    {
        StartCoroutine(startMove(door, new Vector3(door.transform.position.x, doorUp.y, door.transform.position.z), timeFade));
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
