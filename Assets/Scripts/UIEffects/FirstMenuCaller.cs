using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMenuCaller : MonoBehaviour
{
    public GameObject calledMenu;
    public GameObject firstLevelMenu;

    public void OnStartClicked()
    {
        Debug.Log("Detected");
        calledMenu.GetComponent<MenuExitAnimation>().enabled = false;
        firstLevelMenu.GetComponent<MenuExitAnimation>().enabled = false;
        calledMenu.GetComponent<MenuExitAnimation>().enabled = true;
        firstLevelMenu.GetComponent<MenuExitAnimation>().enabled = true;
    }
}
