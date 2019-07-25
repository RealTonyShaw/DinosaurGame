using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public GameObject firstPicture;
    public GameObject secondPicture;
    public float width;
    public SectionScroller attachedSectionScroller;
    public float moveRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool isFirst = true;
    private void FixedUpdate()
    {
        float speed = attachedSectionScroller.currentSpeed * moveRate;
        Vector2 dx = speed * Vector2.left * Time.fixedDeltaTime;
        firstPicture.transform.Translate(dx);
        secondPicture.transform.Translate(dx);

        GameObject leftPicture = isFirst ? firstPicture : secondPicture;
        if (leftPicture.transform.localPosition.x < -width)
        {
            leftPicture.transform.localPosition += 2 * width * Vector3.right;
            isFirst = !isFirst;
        }
    }
}
