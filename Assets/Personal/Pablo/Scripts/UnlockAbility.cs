using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnlockAbility : MonoBehaviour
{
    private PlayerController playerController;
    private GameManager playerManager;

    [SerializeField]
    private BoxCollider colliderBox;
    InputAction interactE;
    [SerializeField]
    UnityEngine.InputSystem.PlayerInput _config;

    [SerializeField]
    private bool inside, activated;

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
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        interactE = _config.actions["Interact"];
        activated = false;
    }

    private void Update()
    {
        if (interactE.triggered && inside && activated == false)
        {
            //Llamar al GameManager
            OpenTheDoors();
            playerController.GetComponent<DrugsMode>().enabled = true;
            orb.SetActive(false);
            colliderBox.enabled = false;
            playerManager.drugs = true;
            activated = true;
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
