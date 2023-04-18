using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using I2.Loc;

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
    private int visiblecount;
    


    [SerializeField]
    private string actualString;
    [SerializeField]
    private float typingSpeed;

    [SerializeField]
    private int i;

    [SerializeField] bool ChangeText = false;

    // Start is called before the first frame update
    void Start()
    {
        GetText(2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetText(int j)
    {
        fieldText.text = dialoguesBox.GetSavedText(j);
        DisplayLetters();
    }

    private IEnumerator DisplayLetters() //Corutina para ir poniendo las letras 1 a 1.
    {
        fieldText.text = actualString;

        fieldText.maxVisibleCharacters = 0;

        totalCharacters = fieldText.textInfo.characterCount;
        counter = 0;
        while (counter < totalCharacters)
        {
            visiblecount = counter % (totalCharacters + 1);
            fieldText.maxVisibleCharacters = visiblecount;

            if (visiblecount >= totalCharacters)
            counter += 1;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (i + 1 < capacity)
            i = i + 1;
        yield return new WaitForSeconds(0.05f);
    }


}
