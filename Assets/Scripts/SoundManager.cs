using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource[] source;

    private void Awake()
    {
        instance = this;
        source = GetComponents<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        source[1].PlayOneShot(sound);
    }

    public void StopAll()
    {
        source[0].Stop();
    }
}
