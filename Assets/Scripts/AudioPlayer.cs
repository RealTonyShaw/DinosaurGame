using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    List<AudioSource> sources = new List<AudioSource>();
    GameObject emptyAudioSource;
    static AudioPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 空闲中的音源
    /// </summary>
    public static AudioSource AvailableAudioSource
    {
        get
        {
            foreach (AudioSource source in Instance.sources)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }
            AudioSource s = Instantiate(Instance.emptyAudioSource, Instance.transform).GetComponent<AudioSource>();
            Instance.sources.Add(s);
            return s;
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="clip">音效</param>
    /// <param name="volume">音量，0到1之间</param>
    public static void PlayAudio(AudioClip clip, float volume)
    {
        AudioSource s = AvailableAudioSource;
        Instance.sources.Add(s);
        s.clip = clip;
        s.volume = volume;
        s.Play();
    }


}
