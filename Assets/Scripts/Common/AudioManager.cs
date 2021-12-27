using UnityEngine;

public class AudioManager : MonoBehaviour {

    static AudioManager _instance;

    public static AudioManager Instance {
        get => _instance;
    }

    [SerializeField] AudioSource audioSource;

    float bgmVolume = 1.0f;
    float soundEffectVolume = 1.0f;

    public float BgmVolume {
        get => bgmVolume;
        set {
            bgmVolume = Mathf.Clamp01(value);
            audioSource.volume = bgmVolume;
        }
    }

    public float SoundEffectVolume {
        get => soundEffectVolume;
        set {
            soundEffectVolume = Mathf.Clamp01(value);
        }
    }

    private void Awake() {
        _instance = this;
    }

    public void PlayBGM(AudioClip clip) {
        if ((audioSource.clip == null || audioSource.clip.name != clip.name)) {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else if (audioSource.clip != null && audioSource.clip.name == clip.name && !audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    public void StopBGM() {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    public void PlayOneShot(AudioClip clip) {
        audioSource.PlayOneShot(clip, soundEffectVolume);
    }


}