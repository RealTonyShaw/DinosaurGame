using UnityEngine;

/*
 * Provide the obstacles with a way of damaging the player.
 */
public class PlayerCollider : MonoBehaviour
{
    public AudioClip collisionAudio;
    public float MinCollisionForce = 1f;
    public float MaxCollisionForce = 5f;

    /*
     * A trigger callback to detect when the player's collider has
     * entered the obstacle's. Then simply obtain the PlayerController
     * reference can apply damage. Then remove the obstacle for feedback.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Obtain a reference to the Player's PlayerController
        PlayerController playerController =
          other.attachedRigidbody.gameObject.GetComponent<PlayerController>();

        // Physical Effect
        Vector2 dir = (other.transform.position - transform.position).normalized;
        float force = Random.Range(MinCollisionForce, MaxCollisionForce);
        other.attachedRigidbody.AddForceAtPosition(force * dir, transform.position, ForceMode2D.Impulse);

        // Register damage with player
        playerController.Damage();

        AudioPlayer.PlayAudio(collisionAudio, 0.6f);
        // Make this object disappear
        GameObject.Destroy(gameObject);
    }
}
