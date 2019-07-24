using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    public GameObject scorePad;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (score != PlayerController.CollectedStarsNumber)
        {
            score = PlayerController.CollectedStarsNumber;
            this.gameObject.GetComponent<Text>().text = score.ToString();
            scorePad.GetComponent<Text>().text = score.ToString();
        }
        
    }
}
