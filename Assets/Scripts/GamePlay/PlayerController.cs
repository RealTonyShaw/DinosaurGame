using System.Collections;
using System.Collections.Generic;
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
    public LayerMask terrainMask = ~(1 << 8);
    /// <summary>
    /// 从死亡到加载结束界面的延迟
    /// </summary>
    public float LoadDelay = 4f;
    public AudioClip EndAudio;
    public Animation AnimationPlayer;
    public AnimationClip hitGroundAnim;
    public AnimationClip jumpAnim;
    public AudioClip hitSnowGround;
    public float AdditionalJumpForceBonus = 1f;
    public float MaxBonusTime = 1f;
    // 角色AD键调平衡的力矩大小
    public float Torque = 10f;
    // 角色速度和角速度的阻尼
    public float DampedDrag = 0.5f;
    /// <summary>
    /// 撞到的石头数量
    /// </summary>
    public int HitStonesNumber { get; private set; } = 0;
    /// <summary>
    /// 收集到的星星数量
    /// </summary>
    public int CollectedStarsNumber { get; private set; } = 0;
    /// <summary>
    /// 玩家的单例
    /// </summary>
    public static PlayerController Instance { get; private set; }
    /// <summary>
    /// 收集到的星星列表，用于星星跟随
    /// </summary>
    public List<GameObject> starList = new List<GameObject>();
    /// <summary>
    /// 每个星星的间隔
    /// </summary>
    public float StarInterval = 1f;
    public float StarApproachingTime = 5f;

    // 角色成功转了一圈之后的音效
    public AudioClip RotateAudio;
    /// <summary>
    /// 转圈数
    /// </summary>
    public int RotationTimes { get; private set; } = 0;
    public float rotationAngle = 0f;
    private float prevAngle = 0f;

    public AnimationCurve ChargingBonus;

    #region 生命周期

    private void Awake()
    {
        Instance = this;
    }

    /*
     * Apply initial health and also store the Rigidbody2D reference for
     * future because GetComponent<T> is relatively expensive.
     */
    private void Start()
    {
        health = 6;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }


    float pressedTime = 0f;

    private void Update()
    {
        if (canJump && !IsEnd)
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

    private void FixedUpdate()
    {
        // 星星跟随
        if (CollectedStarsNumber > 0)
        {
            StarFollow(starList[0].transform, transform, Time.fixedDeltaTime);
            int n = starList.Count;
            for (int i = 1; i < n; i++)
            {
                StarFollow(starList[i].transform, starList[i - 1].transform, Time.fixedDeltaTime);
            }
        }


        if (IsEnd)
            return;
        float torque = -Input.GetAxis("Horizontal") * Torque;
        rigidbody2d.AddTorque(torque, ForceMode2D.Force);
        RestrainXAxis(Time.fixedDeltaTime);


        // 统计旋转
        if (!canJump)
        {
            float dAngle = transform.eulerAngles.z - prevAngle;
            if (dAngle > 180f)
            {
                dAngle -= 360f;
            }
            else if (dAngle < -180f)
            {
                dAngle += 360f;
            }
            rotationAngle += dAngle;
            if (rotationAngle >= 320f)
            {
                Debug.Log("360 !");
                Heal();
                rotationAngle -= 360f;
                AudioPlayer.PlayAudio(RotateAudio, 0.7f);
                RotationTimes++;
                GameDB.rotationTimes++;
            }
            else if (rotationAngle <= -320f)
            {
                Debug.Log("-360 !");
                Heal();
                rotationAngle += 360f;
                AudioPlayer.PlayAudio(RotateAudio, 0.7f);
                RotationTimes++;
                GameDB.rotationTimes++;
            }
        }
        else
        {
            rotationAngle = 0f;
        }
        prevAngle = transform.eulerAngles.z;
    }

    /*
     * If the player has collided with the ground, set the canJump flag so that
     * the player can trigger another jump.
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        HitGround(other);
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    canJump = true;
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    canJump = false;
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Layer == Terrian
        if (other.gameObject.layer == 0x8)
        {
            Damage(10000);
        }
    }

    #endregion

    #region Player Methods

    /*
     * Remove one health unit from player and if health becomes 0, change
     * scene to the end game scene.
     */
    public void Damage()
    {
        if (IsEnd)
            return;
        health -= 1;
        HitStonesNumber++;
        GameDB.crashedStones++;
        if (health < 1)
        {
            // This method is deprecated.
            //Application.LoadLevel("EndGame");
            // Use SceneManager instead.
            End();
            StartCoroutine(DelayedLoad());
        }
    }

    public void Damage(int damage)
    {
        if (IsEnd)
            return;
        health -= damage;

        if (health < 1)
        {
            // This method is deprecated.
            //Application.LoadLevel("EndGame");
            // Use SceneManager instead.
            End();
            StartCoroutine(DelayedLoad());
        }
    }

    IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(LoadDelay);
        SceneManager.LoadSceneAsync("EndGame");
    }

    public void Heal()
    {
        if (health < 6)
        {
            health++;
        }
    }

    /*
     * Accessor for health variable, used by he HUD to display health.
     */
    public int GetHealth()
    {
        return health;
    }
    /// <summary>
    /// 玩家收集到一颗小星星
    /// </summary>
    public void CollectStar(CollectableStar star)
    {
        CollectedStarsNumber++;
        GameDB.collectedStars++;
        starList.Add(star.gameObject);
    }

    void StarFollow(Transform star, Transform target, float dt)
    {
        if ((target.position - star.position).magnitude < StarInterval)
        {
            return;
        }
        Vector3 targetPos = target.position;
        targetPos -= (target.position - star.position).normalized * StarInterval;
        star.position = Vector3.Lerp(star.position, targetPos, StarApproachingTime * dt);
    }

    GameObject ground;

    // 角色落到地面
    private void HitGround(Collision2D other)
    {
        if (!canJump)
        {
            PlayAnim(hitGroundAnim);
            AudioPlayer.PlayAudio(hitSnowGround, 0.9f);
            pressedTime = Time.time;
        }
        canJump = true;
    }
    // 角色起跳，起跳前长按有额外加成
    private void Jump()
    {
        float bonus = ChargingBonus.Evaluate(Time.time - pressedTime);
        Debug.Log(string.Format("Time {0} with bonus {1}%", Time.time - pressedTime, bonus * 100));
        rigidbody2d.AddForce(new Vector2(0, 500 * (1 + bonus)));
        AudioPlayer.PlayAudio(hitSnowGround, 0.9f);
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

    void RestrainXAxis(float dt)
    {
        Vector3 target = transform.position;
        if (canJump)
        {
            Ray2D ray = new Ray2D(transform.position, Vector2.down);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, terrainMask);
            if (hit.collider != null)
            {
                Vector3 fwd = hit.normal;
                fwd = Quaternion.AngleAxis(90f, Vector3.forward) * fwd;
                target -= fwd * (transform.position.x / fwd.x);
            }
            else
            {
                target.x = 0f;
            }
        }
        else
        {
            target.x = 0f;
        }
        transform.position = Vector3.Lerp(transform.position, target, 3f * dt);
        target = -rigidbody2d.velocity;
        target.y = 0f;
        target.z = 0f;
        rigidbody2d.AddForce(target * DampedDrag, ForceMode2D.Force);
        rigidbody2d.AddTorque(-rigidbody2d.angularVelocity * Mathf.Deg2Rad * DampedDrag, ForceMode2D.Force);
    }
    public bool IsEnd { get; private set; } = false;
    public void End()
    {
        IsEnd = true;
        AudioPlayer.PlayAudio(EndAudio, 0.8f);
        SectionScroller.Instance.Stop();
    }

    public void Pass()
    {
        IsEnd = true;
    }

    #endregion
}
