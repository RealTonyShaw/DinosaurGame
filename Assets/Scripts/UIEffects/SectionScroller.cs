using UnityEngine;

/*
 * Attached to the section so that everything will scroll sideways.
 * The player does not move in this game, the environment does.
 */
public class SectionScroller : MonoBehaviour
{
    public AnimationCurve Speed;
    public float currentSpeed;
    float startTime = 0f;
    private void Start()
    {
        startTime = Time.time;
    }
    /*
     * Use the Transform component attached to the section game object and
     * translate it based on delta time.
     */
    private void FixedUpdate()
    {
        currentSpeed = Speed.Evaluate(Time.time - startTime);
        transform.Translate(currentSpeed * Vector2.left * Time.fixedDeltaTime);
    }
}
