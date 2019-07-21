using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontAnimation : MonoBehaviour
{
    Color FontColor;

    // Start is called before the first frame update
    void Start()
    {
        FontColor = new Color();
        FontColor.a = 0;
        FontColor.r = 255;
        FontColor.g = 255;
        FontColor.b = 255;
        gameObject.GetComponent<Text>().color = FontColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (FontColor.a < 255)
        {
            FontColor.a++;
            gameObject.GetComponent<Text>().color = FontColor;
        }

    }
}
