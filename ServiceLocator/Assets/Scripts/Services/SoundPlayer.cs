using UnityEngine;
using Zenject;

public class SoundPlayer : ISoundPlayer
{
    private readonly AudioSource _audioSource;
    private AudioClip _openClip;
    private AudioClip _closeClip;
    private AudioClip _shootClip;
    private AudioClip _hitClip;

    public SoundPlayer(AudioSource audioSource, AudioClip openClip, AudioClip closeClip, AudioClip shootClip, AudioClip hitClip)
    {
        _audioSource = audioSource;
        _openClip = openClip;
        _closeClip = closeClip;
        _shootClip = shootClip;
        _hitClip = hitClip;
    }

    [Inject]
    public SoundPlayer(AudioSource audioSource,
        [Inject(Id = "ShootSound", Optional = true)] AudioClip shootClip = null,
        [Inject(Id = "HitSound", Optional = true)] AudioClip hitClip = null)
    {
        _audioSource = audioSource;
        _shootClip = shootClip;
        _hitClip = hitClip;
    }

    public void PlayOpenSound() => Play(_openClip);
    public void PlayCloseSound() => Play(_closeClip);
    public void PlayShootSound() => Play(_shootClip);
    public void PlayHitSound() => Play(_hitClip);

    private void Play(AudioClip clip)
    {
        if (clip != null && _audioSource != null)
            _audioSource.PlayOneShot(clip);
    }
}