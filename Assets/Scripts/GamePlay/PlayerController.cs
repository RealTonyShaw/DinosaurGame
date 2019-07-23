using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Behaviour to handle keyboard input and also store the player's
 * current health.
 */
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private int health;
    private bool canJump = false;
    public Animation AnimationPlayer;
    public AnimationClip hitGroundAnim;
    public AnimationClip jumpAnim;
    public float AdditionalJumpForceBonus = 1f;
    public float MaxBonusTime = 1f;
    public float Torque = 10f;
    public float Force = 100f;
    public float DampedDrag = 0.5f;

    public AnimationCurve ChargingBonus;
    // 玩家与地面允许的最大角度（超过则判为死亡）
    public const float MAX_SLIP_ANGLE = 75f;

    /*
     * Apply initial health and also store the Rigidbody2D reference for
     * future because GetComponent<T> is relatively expensive.
     */
    private void Start()
    {
        health = 6;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    /*
     * Remove one health unit from player and if health becomes 0, change
     * scene to the end game scene.
     */
    public void Damage()
    {
        health -= 1;

        if (health < 1)
        {
            // This method is deprecated.
            //Application.LoadLevel("EndGame");
            // Use SceneManager instead.
            SceneManager.LoadSceneAsync("EndGame");
        }
    }
    public void Damage(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            // This method is deprecated.
            //Application.LoadLevel("EndGame");
            // Use SceneManager instead.
            SceneManager.LoadSceneAsync("EndGame");
        }
    }

    /*
     * Accessor for health variable, used by he HUD to display health.
     */
    public int GetHealth()
    {
        return health;
    }

    float pressedTime = 0f;

    private void Update()
    {
        if (canJump)
        {
            //if (ground != null)
            //{
            //    //rigidbody2d.AddForce(Input.GetAxis("Horizontal") * Force * transform.right);
            //    if (Vector2.Angle(ground.transform.up, transform.up) > MAX_SLIP_ANGLE)
            //    {
            //        Debug.Log("Angle " + Vector2.Angle(ground.transform.up, transform.up));
            //        Damage(10000);
            //        return;
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pressedTime = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Jump();
            }

        }

    }

    private void FixedUpdate()
    {
        float torque = -Input.GetAxis("Horizontal") * Torque;
        Debug.Log("Torque is " + torque);
        rigidbody2d.AddTorque(torque, ForceMode2D.Force);
        RestrainXAxis(Time.fixedDeltaTime);
    }

    /*
     * If the player has collided with the ground, set the canJump flag so that
     * the player can trigger another jump.
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        HitGround(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Layer == Terrian
        if (other.gameObject.layer == 0x8)
        {
            Damage(10000);
        }
    }

    GameObject ground;

    // 角色落到地面
    private void HitGround(Collision2D other)
    {
        if (!canJump)
        {
            PlayAnim(hitGroundAnim);
            pressedTime = Time.time;
            canJump = true;
        }
    }
    // 角色起跳，起跳前长按有额外加成
    private void Jump()
    {
        canJump = false;
        float bonus = ChargingBonus.Evaluate(Time.time - pressedTime);
        Debug.Log(string.Format("Time {0} with bonus {1}%", Time.time - pressedTime, bonus * 100));
        rigidbody2d.AddForce(new Vector2(0, 500 * (1 + bonus)));
        PlayAnim(jumpAnim);
    }
    // 长按增加角色跳跃力度

    void PlayAnim(AnimationClip clip)
    {
        if (AnimationPlayer.isPlaying)
        {
            AnimationPlayer.Stop();
        }
        AnimationPlayer.clip = clip;
        AnimationPlayer.Play();
    }

    void RestrainXAxis(float dt)
    {
        Vector3 target = transform.position;
        target.x = 0f;
        transform.position = Vector3.Lerp(transform.position, target, 3f * dt);
        target = -rigidbody2d.velocity;
        target.y = 0f;
        target.z = 0f;
        rigidbody2d.AddForce(target * DampedDrag, ForceMode2D.Force);
        rigidbody2d.AddTorque(-rigidbody2d.angularVelocity * Mathf.Deg2Rad * DampedDrag, ForceMode2D.Force);
    }
}
