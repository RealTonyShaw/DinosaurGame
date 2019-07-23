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
    public float AdditionalJumpForceBonus = 0.5f;
    public float MaxBonusTime = 0.5f;

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

    /*
     * If the player has collided with the ground, set the canJump flag so that
     * the player can trigger another jump.
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        HitGround();
    }

    // 角色落到地面
    private void HitGround()
    {
        PlayAnim(hitGroundAnim);
        canJump = true;
    }
    // 角色起跳，起跳前长按有额外加成
    private void Jump()
    {
        float bonus = AdditionalJumpForceBonus * Mathf.Clamp01((Time.time - pressedTime) / MaxBonusTime);
        rigidbody2d.AddForce(new Vector2(0, 500 * (1 + bonus)));
        PlayAnim(jumpAnim);
        canJump = false;
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
}
