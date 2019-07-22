using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondMenuCaller : MonoBehaviour
{
    public GameObject calledMenu;
    public GameObject firstLevelMenu;

    public void OnStartClicked()
    {
        Debug.Log("Detected");
        calledMenu.GetComponent<MenuExitAnimation>().enabled = false;
        firstLevelMenu.GetComponent<MenuExitAnimation>().enabled = false;
        calledMenu.GetComponent<MenuEnterAnimation>().enabled = true;
        firstLevelMenu.GetComponent<MenuEnterAnimation>().enabled = true;
    }
}
