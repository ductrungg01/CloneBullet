using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct MusicInfo
{
    public string id;
    public AudioClip clip;
}

[CreateAssetMenu(menuName = "Systems/Databases/Audio", fileName = "AudioDatabase")]
public class AudioDatabase : ScriptableObject
{
    [SerializeField] private MusicInfo[] _backgroundMusics;
    [SerializeField] private MusicInfo[] _soundEffects;

    public AudioClip GetBackgroundMusic(string id)
    {
        return _backgroundMusics.FirstOrDefault((music) => music.id == id).clip;
    }

    public AudioClip GetSoundEffect(string id)
    {
        return _soundEffects.FirstOrDefault((music) => music.id == id).clip;

    }
}
