using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame1Administrator : MonoBehaviour
{
    [SerializeField]
    private GameObject door, peladin;

    [SerializeField]
    private Vector3 doorUp;
    [SerializeField]
    private bool finished = false;
    [SerializeField]
    private float timeFade;

    public delegate void ResetGame();
    public static ResetGame reset;

    public int sequenceCorrect;
    public void CorrectWay(int i)
    {
        if(finished == false)
        {
            if (i == 0 && sequenceCorrect == 0)
            {
                sequenceCorrect = 1;
            }
            else if (i == 0 && sequenceCorrect != 0)
            {
                sequenceCorrect = 0;
            }

            if (i == 1 && sequenceCorrect == 1)
            {
                sequenceCorrect = 2;
            }
            else if (i == 1 && sequenceCorrect != 1)
            {
                sequenceCorrect = 0;
            }

            if (i == 2 && sequenceCorrect == 2)
            {
                sequenceCorrect = 3;
            }
            else if (i == 2 && sequenceCorrect != 2)
            {
                sequenceCorrect = 0;
            }

            if (i == 3 && sequenceCorrect == 3)
            {
                finished = true;
                peladin.GetComponent<BoxCollider>().enabled = false;
                peladin.SetActive(false);
                OpenTheDoors();
            }
        }  
    }

    public void BadWay()
    {
        sequenceCorrect = 0;
        reset();
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
