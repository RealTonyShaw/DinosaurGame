using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuExitAnimation : MonoBehaviour
{
    public float destinationX;
    public float speed;

    public void Start()
    {
        gameObject.GetComponent<MenuEnterAnimation>().enabled = false;
    }

    // Vector2 position;
    public void Update()
    {
        if (gameObject.GetComponent<RectTransform>().anchoredPosition.x != destinationX)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition.x, destinationX, speed * Time.deltaTime), gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            if (Mathf.Abs(gameObject.GetComponent<RectTransform>().anchoredPosition.x - destinationX) <= 5)
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(destinationX, gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                this.enabled = false;
            }
        }
    }
}
