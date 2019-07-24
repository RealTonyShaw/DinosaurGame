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
        if(gameObject.GetComponent<Transform>().localScale.x < 0)
        {
            rotationSpeed = -rotationSpeed;
        }

        if (gameObject.GetComponent<Transform>().localScale.x > maxScale)
        {
            rotationSpeed = -rotationSpeed;
        }
        gameObject.GetComponent<Transform>().localScale = new Vector3(gameObject.GetComponent<Transform>().localScale.x - rotationSpeed, gameObject.GetComponent<Transform>().localScale.y, gameObject.GetComponent<Transform>().localScale.z);
    }
}
