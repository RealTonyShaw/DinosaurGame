using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousMenuCaller : MonoBehaviour
{
    /// <summary>
	/// The menu that currently presents to player
	/// </summary>
    public GameObject thisMenu;

    /// <summary>
	/// The menu that this menu comes from
	/// </summary>
    public GameObject previousMenu;

    public void OnStartClicked()
    {
        // Stop previous animation, otherwise animations towards different directions would stuck with each other
        thisMenu.GetComponent<MenuEnterAnimation>().enabled = false;
        previousMenu.GetComponent<MenuEnterAnimation>().enabled = false;

        thisMenu.GetComponent<MenuExitAnimation>().enabled = true;
        previousMenu.GetComponent<MenuExitAnimation>().enabled = true;
    }
}
