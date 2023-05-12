using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPosition : MonoBehaviour
{
    public int damage;
    [SerializeField]
    GameObject player;
    [SerializeField]
    bool move;
    ParticleSystem effect; 

    public int followDistance;
    public int followDrugs;
    public int followDistTotal;
    [SerializeField]
    private List<Vector3> storedPositions;
    // Start is called before the first frame update
    void Start()
    {
        storedPositions = new List<Vector3>(); //create a blank list
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (!player.GetComponent<DrugsMode>().buffed)
            {
                followDistTotal = followDistance;
            }
            else
            {
                followDistTotal = followDistance + followDrugs;
            }

            if (storedPositions.Count == 0)
            {
                Debug.Log("blank list");
                storedPositions.Add(player.transform.position); //store the players currect position
                return;
            }
            else if (storedPositions[storedPositions.Count - 1] != player.transform.position)
            {
                //Debug.Log("Add to list");
                storedPositions.Add(player.transform.position); //store the position every frame
            }

            if (storedPositions.Count > followDistTotal)
            {
                transform.position = storedPositions[0]; //move
                storedPositions.RemoveAt(0); //delete the position that player just move to
            }
            //transform.position = player.transform.position;
        }
    }

    public void Move(GameObject p)
    {
        player = p;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.TryGetComponent<PlayerController>(out PlayerController player))
            player.GetComponent<HealthBehaviour>().Hurt(damage);
    }
}
