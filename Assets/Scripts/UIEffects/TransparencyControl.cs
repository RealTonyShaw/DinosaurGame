using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransparencyControl : MonoBehaviour
{
    /// <summary>
	/// The time interval to finish fading in / fading out
	/// </summary>
    public float alphaSpeed = 2.0f;
    /// <summary>
	/// The alpha at the end of fading in / fading out
	/// </summary>
    public float alpha = 1.0f;
    /// <summary>
    /// At which time the fading effect works
    /// </summary>
    public float startTime;

    private CanvasGroup cg;

    void Start()
    {
        cg = this.transform.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > startTime)
        {
            if (alpha != cg.alpha)
            {
                cg.alpha = Mathf.Lerp(cg.alpha, alpha, alphaSpeed * Time.deltaTime);
                if (Mathf.Abs(alpha - cg.alpha) <= 0.01)
                {
                    cg.alpha = alpha;
                }
            }
        }
    }

    /// <summary>
	/// Set the UI to be interactive
	/// </summary>
    public void Show()
    {
        alpha = 1;
        cg.blocksRaycasts = true;
    }

    /// <summary>
	/// Set the UI to be not interactive
	/// </summary>
    public void Hide()
    {
        alpha = 0;
        cg.blocksRaycasts = false;
    }
}