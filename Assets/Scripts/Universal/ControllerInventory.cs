using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControllerInventory : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject Menu;
    public GameObject BagMenu;
    [Header("Selected")]
    [SerializeField]GameObject SelectedButton;
    [SerializeField] int menIndex;
    [SerializeField] int index;
    [Header("Buttons")]
    public List<GameObject> MenuButtons;
    public GameObject[] BagButtons;
    [Header("PlayerInputs")]
    public PlayerInput playerControls;
    InputAction up;
    InputAction down;
    InputAction right;
    InputAction left;
    InputAction Use;
    InputAction pause;

    public bool hideMouse;

    private void Start()
    {
        index = 0;
        up = playerControls.actions["First"];
        down = playerControls.actions["Second"];
        right = playerControls.actions["Third"];
        left = playerControls.actions["Fourth"];
        Use = playerControls.actions["Interact"];
        pause = playerControls.actions["Pause"];

        //BagButtons = new GameObject[GameObject.FindGameObjectsWithTag("BagButtons").Length];
        //BagButtons = GameObject.FindGameObjectsWithTag("BagButtons");
    }

    // Update is called once per frame
    void Update()
    {
        bool controllers = Gamepad.all.Count > 0;

        Debug.Log("Controllers es " + controllers);
        if (controllers)
        {
            if(BagMenu.activeInHierarchy)
            {
                SelectedButton = MenuButtons[index];

                if(up.triggered && index - 6 > 0)
                {
                    index -= 6;
                }
                else
                {
                    index = BagButtons.Length - (6 - index);
                }

                if (down.triggered && index + 6 < BagButtons.Length)
                {
                    index += 6;
                }
                else
                {
                    index = 6 + index;
                }

                if (left.triggered && index - 1 > 0)
                {
                    index --;
                }
                else
                {
                    index = BagButtons.Length - 1;
                }

                if (right.triggered && index + 1 < BagButtons.Length)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }

                if (Use.triggered)
                    SelectedButton.GetComponent<Button>().onClick.Invoke();
            }
            else if(Menu.activeInHierarchy)
            {
                SelectedButton = MenuButtons[index];

                if (up.triggered && index - 1 > 0)
                {
                    index--;
                }
                else
                {
                    index = BagButtons.Length - 1;
                }

                if (down.triggered && index + 1 < BagButtons.Length)
                {
                    index ++;
                }
                else
                {
                    index = 0;
                }

                if (Use.triggered)
                    SelectedButton.GetComponent<Button>().onClick.Invoke();

            }

        }
        
        if (pause.triggered)
        {
            switch(Menu.activeInHierarchy)
            {
                case true:
                    Menu.SetActive(false);
                    BagMenu.SetActive(false);
                    index = 0;
                    menIndex = 0;
                    break;
                case false:
                    Menu.SetActive(true);
                    break;
            }
        }
    }

    public void ResetInd()
    {
        index = 0;
    }
    public void ResetMenInd()
    {
        menIndex = 0;
    }
}
