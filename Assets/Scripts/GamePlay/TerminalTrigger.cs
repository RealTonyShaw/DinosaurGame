using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject LevelPassMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<Transform>().position.x <= player.GetComponent<Transform>().position.x)
        {
            Debug.Log("You Passed!");
            LevelPassMenu.GetComponent<MenuEnterAnimation>().enabled = true;
            Destroy(this);
        }
    }
}
