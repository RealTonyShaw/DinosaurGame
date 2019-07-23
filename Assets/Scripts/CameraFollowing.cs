using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    Camera myCamera;
    GameObject Player;
    public Vector2 Offset = new Vector2(0.5f, 1.8f);
    public AnimationCurve cameraSizeCurve;
    public float smoothTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        myCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = Player.transform.position;
        target.x += Offset.x;
        target.y += Offset.y;
        target.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, target, smoothTime * Time.deltaTime);

        myCamera.orthographicSize = cameraSizeCurve.Evaluate(transform.position.y);
    }
}
