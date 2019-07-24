using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStar : MonoBehaviour
{
    public bool IsCollected { get; private set; } = false;
    public Collider2D[] colliders;
    public AudioClip CollectedPromptAudioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsCollected && other.attachedRigidbody != null && other.attachedRigidbody.gameObject.tag == "Player")
        {
            IsCollected = true;

            // 禁用碰撞
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }

            // Collect this star
            PlayerController.Instance.CollectStar(this);
            AudioPlayer.PlayAudio(CollectedPromptAudioClip, 0.6f);
        }
    }
}
