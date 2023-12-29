using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;
    // Start is called before the first frame update

    public void PlaySound(int index)
    {
        _audioSource.PlayOneShot(_audioClips[index]);
    }
}
