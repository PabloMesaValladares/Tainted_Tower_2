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
    public GameObject AddMenu;
    public GameObject DeathMenu;
    [Header("Selected")]
    [SerializeField]GameObject SelectedButton;
    [SerializeField] int menIndex;
    [SerializeField] int index;
    [Header("Buttons")]
    public List<GameObject> MenuButtons;
    public List<GameObject> BagButtons;
    public List<GameObject> AddButtons;
    public List<GameObject> QuickButtons;
    public List<GameObject> DeathButtons;
    [Header("PlayerInputs")]
    public PlayerInput playerControls;
    InputAction up, down, right, left, Use, pause, bag, back;

    public bool hideMouse;


    public delegate void Unlock();
    public static Unlock mouseUnlock;
    public delegate void Lock();
    public static Lock mouseLock;
    private void Start()
    {
        index = 0;
        up = playerControls.actions["First"];
        right = playerControls.actions["Second"];
        down = playerControls.actions["Third"];
        left = playerControls.actions["Fourth"];
        Use = playerControls.actions["Jump"];
        pause = playerControls.actions["Pause"];
        bag = playerControls.actions["Bag"];
        back = playerControls.actions["Attack"];

        //BagButtons = new GameObject[GameObject.FindGameObjectsWithTag("BagButtons").Length];
        //BagButtons = GameObject.FindGameObjectsWithTag("BagButtons");
    }

    // Update is called once per frame
    void Update()
    {
        if (InputDetecter.Instance.gamepad)
        {
            if(AddMenu.activeInHierarchy)
            {
                if (up.triggered)
                {
                    AddButtons[0].GetComponent<Button>().onClick.Invoke();
                    AddMenu.SetActive(false);
                }

                if (down.triggered)
                {

                    AddButtons[2].GetComponent<Button>().onClick.Invoke();
                    AddMenu.SetActive(false);
                }

                if (left.triggered)
                {

                    AddButtons[3].GetComponent<Button>().onClick.Invoke();
                    AddMenu.SetActive(false);
                }

                if (right.triggered)
                {

                    AddButtons[1].GetComponent<Button>().onClick.Invoke();
                    AddMenu.SetActive(false);
                }

                if (back.triggered)
                    AddMenu.SetActive(false);
            }
            else if(BagMenu.activeInHierarchy)
            {
                SelectedButton = BagButtons[index];
                SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.highlightedColor;

                if (up.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    index -= 6;
                    if(index < 0)
                    {
                        index += BagButtons.Count;
                    }
                }

                if (down.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    index += 6;
                    if(index >= BagButtons.Count)
                        index -= BagButtons.Count;
                }

                if (left.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    index --;
                    if(index < 0)
                        index = BagButtons.Count - 1;
                }

                if (right.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    index++;
                    if(index >= BagButtons.Count)
                        index = 0;
                }

                if (back.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    BagMenu.SetActive(false);
                }

                if (Use.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    SelectedButton.GetComponent<Button>().onClick.Invoke();
                }

            }
            else if(Menu.activeInHierarchy)
            {
                SelectedButton = MenuButtons[menIndex];
                SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.highlightedColor;
                if (up.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    menIndex--;
                    if(menIndex < 0)
                        menIndex = MenuButtons.Count - 1;

                }

                if (down.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    menIndex++;
                    if(menIndex >= MenuButtons.Count)
                        menIndex = 0;
                }

                if (back.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    Menu.SetActive(false);
                    SelectedButton = null;
                    BagMenu.SetActive(false);
                    mouseLock();
                }

                if (Use.triggered)
                {
                    SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    SelectedButton.GetComponent<Button>().onClick.Invoke();
                }
            }
  

        }
        
        if (pause.triggered)
        {
            switch(Menu.activeInHierarchy)
            {
                case true:
                    Menu.SetActive(false);
                    BagMenu.SetActive(false);
                    if (SelectedButton != null)
                        SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    SelectedButton = null;
                    mouseLock();
                    break;
                case false:
                    Menu.SetActive(true);
                    index = 0;
                    menIndex = 0;
                    mouseUnlock();
                    break;
            }
        }
        if (bag.triggered)
        {
            switch (BagMenu.activeInHierarchy)
            {
                case true:
                    Menu.SetActive(false);
                    BagMenu.SetActive(false);
                    if (SelectedButton != null)
                        SelectedButton.GetComponent<Image>().color = SelectedButton.GetComponent<Button>().colors.normalColor;
                    SelectedButton = null;
                    mouseLock();
                    break;
                case false:
                    Menu.SetActive(true);
                    BagMenu.SetActive(true);
                    index = 0;
                    menIndex = 0;
                    mouseUnlock();
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
