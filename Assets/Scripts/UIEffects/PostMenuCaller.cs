using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostMenuCaller : MonoBehaviour
{
    /// <summary>
	/// The menu that currently presents to player
    /// </summary>
    public GameObject thisMenu;

    /// <summary>
    /// The menu that player ought to be brought to
    /// </summary>
    public GameObject postMenu;
    
    public void OnStartClicked()
    {
        // Stop previous animation, otherwise animations towards different directions would stuck with each other
        postMenu.GetComponent<MenuExitAnimation>().enabled = false;
        thisMenu.GetComponent<MenuExitAnimation>().enabled = false;

        // Determine the sliding direction
        if (postMenu.GetComponent<RectTransform>().anchoredPosition.x > postMenu.GetComponent<MenuEnterAnimation>().destinationX)
            thisMenu.GetComponent<MenuEnterAnimation>().destinationX = thisMenu.GetComponent<MenuEnterAnimation>().destinationX < 0 ? thisMenu.GetComponent<MenuEnterAnimation>().destinationX : -thisMenu.GetComponent<MenuEnterAnimation>().destinationX;
        if (postMenu.GetComponent<RectTransform>().anchoredPosition.x < postMenu.GetComponent<MenuEnterAnimation>().destinationX)
            thisMenu.GetComponent<MenuEnterAnimation>().destinationX = thisMenu.GetComponent<MenuEnterAnimation>().destinationX > 0 ? thisMenu.GetComponent<MenuEnterAnimation>().destinationX : -thisMenu.GetComponent<MenuEnterAnimation>().destinationX;
        postMenu.GetComponent<MenuEnterAnimation>().enabled = true;
        thisMenu.GetComponent<MenuEnterAnimation>().enabled = true;
    }
}
