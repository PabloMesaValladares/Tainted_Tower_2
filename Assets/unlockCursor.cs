using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Press the space bar to apply no locking to the Cursor
        if (Input.GetKeyDown(KeyCode.J))
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
    }
}
