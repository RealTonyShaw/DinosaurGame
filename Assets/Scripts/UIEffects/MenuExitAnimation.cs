using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Menus would fade out with this animation
/// </summary>
public class MenuExitAnimation : MonoBehaviour
{
    /// <summary>
    /// Where a UI object is supposed to slide to
    /// </summary>
    public float destinationX;

    /// <summary>
    /// How fast a UI object slide
    /// </summary>
    public float speed;

    public void Start()
    {
        // If a menu enter animation is running, stop it as exit animation starts
        gameObject.GetComponent<MenuEnterAnimation>().enabled = false;
    }

    // Vector2 position;
    public void Update()
    {
        if (gameObject.GetComponent<RectTransform>().anchoredPosition.x != destinationX)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(gameObject.GetComponent<RectTransform>().anchoredPosition.x, destinationX, speed * Time.deltaTime), gameObject.GetComponent<RectTransform>().anchoredPosition.y);
            // If the object and its destination are too close, stop updating position with .Lerp
            if (Mathf.Abs(gameObject.GetComponent<RectTransform>().anchoredPosition.x - destinationX) <= 0.1)
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(destinationX, gameObject.GetComponent<RectTransform>().anchoredPosition.y);
                this.enabled = false;
            }
        }
    }
}
