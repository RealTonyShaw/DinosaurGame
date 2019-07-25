using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject LevelPassMenu;
    public static TerminalTrigger Instance { get; private set; }
    public AudioClip PassAudio;
    public bool IsTriggered { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if (player == null)
        {
            player = PlayerController.Instance.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= player.transform.position.x)
        {
            // 玩家已经死了
            if (PlayerController.Instance.IsEnd)
            {
                return;
            }
            Debug.Log("You Passed!");
            IsTriggered = true;
            PlayerController.Instance.Pass();
            LevelPassMenu.GetComponent<MenuEnterAnimation>().enabled = true;
            AudioPlayer.PlayAudio(PassAudio, 0.8f);
            Destroy(this);
        }
    }
}
