using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Menus would fade in with this animation
/// </summary>
public class MenuEnterAnimation : MonoBehaviour
{
    /// <summary>
    /// Where a UI object is supposed to slide to
    /// </summary>
    public float destinationX;

    /// <summary>
    /// How fast a UI object slide
    /// </summary>
    public float speed;

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
