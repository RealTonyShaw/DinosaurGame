using UnityEngine;

/*
 * Attached to the section so that everything will scroll sideways.
 * The player does not move in this game, the environment does.
 */
public class SectionScroller : MonoBehaviour
{
    public AnimationCurve EasySpeed;
    public AnimationCurve Speed;
    public AnimationCurve HardSpeed;
    public float currentSpeed;
    public float stopTime = 8f;
    /// <summary>
    /// 最大滑行距离
    /// </summary>
    public float MaxDistance;
    float startTime = 0f;
    float startX;
    bool isEnd = false;
    public static SectionScroller Instance { get; private set; }
    private void Start()
    {
        Instance = this;
        startX = transform.position.x;
        startTime = Time.time;
    }

    public void Stop()
    {
        isEnd = true;
    }

    private void Update()
    {
        if (isEnd)
            return;

        if (startX - transform.position.x > MaxDistance)
        {
            isEnd = true;
        }
    }

    /*
     * Use the Transform component attached to the section game object and
     * translate it based on delta time.
     */
    private void FixedUpdate()
    {
        if (!isEnd)
        {
            currentSpeed = Speed.Evaluate(Time.time - startTime);
        }
        else
        {
            currentSpeed -= stopTime * currentSpeed * Time.fixedDeltaTime;
        }
        transform.Translate(currentSpeed * Vector2.left * Time.fixedDeltaTime);
    }
}
