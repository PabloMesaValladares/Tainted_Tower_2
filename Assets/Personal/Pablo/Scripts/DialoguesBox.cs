using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using I2.Loc;

public class DialoguesBox : MonoBehaviour
{ 
    [SerializeField]
    private int reviseWords;
    [SerializeField]
    string[] saveTexts;

    [SerializeField]
    private string wordsID, wordsIDCombinated, fieldText;

    // Start is called before the first frame update
    void Awake()
    {
        GetTerms();
    }

    public void GetTerms()
    { 
        for (int i = 0; i < reviseWords; i++)
        {
            wordsIDCombinated = wordsID + i.ToString();
            fieldText = I2.Loc.LocalizationManager.GetTranslation(wordsIDCombinated);
            saveTexts[i] = fieldText;
        }
    }

    public string GetSavedText(int i)
    {
        return saveTexts[i];
    }
}
