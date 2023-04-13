using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame1Administrator : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    public int sequenceCorrect;
    public void CorrectWay(int i)
    {
        if (i == 0 && sequenceCorrect == 0)
        {
            sequenceCorrect = 1;
        }
        else if(i == 0 && sequenceCorrect != 0)
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
            OpenTheDoors();
        }
    }

    public void BadWay()
    {
        sequenceCorrect = 0;
    }

    public void OpenTheDoors()
    {
        door.SetActive(false);
    }

    public void CloseTheDoors()
    {
        door.SetActive(true);
    }
}
