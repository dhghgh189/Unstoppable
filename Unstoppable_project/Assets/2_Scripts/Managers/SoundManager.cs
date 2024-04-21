using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources;

    Transform sound_Root = null; 

    public void Init()
    {
        if (sound_Root != null)
            return;

        GameObject root = new GameObject("@Sound_Root");
        UnityEngine.Object.DontDestroyOnLoad(root);
        sound_Root = root.transform;

        _audioSources = new AudioSource[(int)Define.ESoundType.Max];

        for (int i = 0; i < _audioSources.Length; i++)
        {
            GameObject go = new GameObject($"@{(Define.ESoundType)i}");
            go.transform.SetParent(sound_Root);
            _audioSources[i] = go.AddComponent<AudioSource>();
            _audioSources[i].playOnAwake = false;
        }

        _audioSources[(int)Define.ESoundType.Bgm].loop = true;
        _audioSources[(int)Define.ESoundType.Bgm].volume = 0.7f;
    }

    public void PlaySound(Define.ESoundType type, string path)
    {
        AudioClip clip = LoadAudioClip(path);
        if (clip == null)
            return;

        PlaySound(type, clip);
    }

    public void PlaySound(Define.ESoundType type, AudioClip clip)
    {
        AudioSource audioSource = _audioSources[(int)type];

        if (type == Define.ESoundType.Bgm)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void StopSound(Define.ESceneType type)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Stop();
    }

    public AudioClip LoadAudioClip(string path)
    {
        AudioClip clip = null;

        clip = theApp.Res.Load<AudioClip>(path);
        if (clip == null)
            return null;

        return clip;
    }
}
