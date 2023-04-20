using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using I2.Loc;
using UnityEngine.InputSystem;

public class DialoguesBehaviour : MonoBehaviour
{
    [SerializeField]
    private DialoguesBox dialoguesBox;
    [SerializeField]
    private TMP_Text fieldText;


    [Header("Debug")]
    public int totalCharacters;
    public int capacity;
    public int counter;
    public int visiblecount;

    [SerializeField]
    private string actualString;
    [SerializeField]
    private float typingSpeed;

    // Start is called before the first frame update
    public void GetText(int j)
    {
        actualString = dialoguesBox.GetSavedText(j);
        StartCoroutine(DisplayLetters());
    }

    private IEnumerator DisplayLetters() //Corutina para ir poniendo las letras 1 a 1.
    {
        fieldText.text = actualString;

        fieldText.maxVisibleCharacters = 0;

        totalCharacters = actualString.Length;
        counter = 0;
        while (counter <= totalCharacters)
        {
            fieldText.maxVisibleCharacters = counter;

            //if (counter <= totalCharacters)
                counter += 1;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(0.05f);
    }


}
