using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField]
    private AudioDatabase database;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private bool _isPlayBackground = true;
    private bool _isPlaySoundEffect = true;
    public void PlayBackgroundSound(string id)
    {
        if (_isPlayBackground == false) return;

        AudioClip clip = database.GetBackgroundMusic(id);
        audioSource.GetComponent<AudioSource>().clip = clip;
        audioSource.GetComponent<AudioSource>().loop = true;
        audioSource.Play(); 
    }
    public void PlaySoundEffect(string id)
    {
        if (_isPlaySoundEffect == false) return;

        AudioClip clip = database.GetSoundEffect(id);
        audioSource.PlayOneShot(clip);
    }

    public void SetIsPlayBackground(bool isPlay)
    {
        _isPlayBackground = isPlay;
        audioSource.mute = !isPlay;
    }

    public void MuteBackgroundMusic()
    {
        SetIsPlayBackground(false);
    }

    public void SetIsPlaySoundEffect(bool isPlay)
    {
        _isPlaySoundEffect = isPlay;
    }
}
