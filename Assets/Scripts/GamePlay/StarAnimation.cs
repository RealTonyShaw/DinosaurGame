using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    public float rotationSpeed;

    private float maxScale;
    // Start is called before the first frame update
    void Start()
    {
        maxScale = gameObject.GetComponent<Transform>().localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Star is becoming bigger when it's too small
        if(gameObject.GetComponent<Transform>().localScale.x < 0)
        {
            rotationSpeed = -rotationSpeed;
        }

        // Star is becoming smaller when it's too large
        if (gameObject.GetComponent<Transform>().localScale.x > maxScale)
        {
            rotationSpeed = -rotationSpeed;
        }
        gameObject.GetComponent<Transform>().localScale = new Vector3(gameObject.GetComponent<Transform>().localScale.x - rotationSpeed, gameObject.GetComponent<Transform>().localScale.y, gameObject.GetComponent<Transform>().localScale.z);
    }
}
