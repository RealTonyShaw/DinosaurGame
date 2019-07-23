using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    Camera myCamera;
    GameObject Player;
    public Vector2 Offset = new Vector2(0.5f, 1.8f);
    public AnimationCurve cameraSizeCurve;
    // 规定哪些Layer属于地形，会用于玩家高度的计算。
    public LayerMask terrainMask = ~(1 << 8);
    public float smoothTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        myCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray2D ray = new Ray2D(Player.transform.position, Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(Player.transform.position, Vector2.down, 100f, terrainMask);
        float height = hit.distance;
        Vector3 target = Player.transform.position;

        target.x += Offset.x;
        target.y += Offset.y;
        target.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, target, smoothTime * Time.fixedDeltaTime);

        myCamera.orthographicSize += (cameraSizeCurve.Evaluate(height) - myCamera.orthographicSize) * smoothTime * Time.fixedDeltaTime;
    }
}
