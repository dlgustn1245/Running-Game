using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmPlayer;
    public AudioSource sfxPlayer;
    public AudioClip gameBGM;
    public AudioClip[] sfxClips;

    Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();

    /// <summary>
    /// singleton pattern
    /// </summary>
    static SoundManager instance;
    public static SoundManager Instace
    {
        get
        {
            if (!instance) return null;
            return instance;
        }
    }

    void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

        foreach(AudioClip audio in sfxClips)
        {
            audioClipDic.Add(audio.name, audio);
        }
    }

    public void PlayGameBGM(float volume = 1.0f)
    {
        bgmPlayer.loop = true;
        bgmPlayer.volume = volume;

        bgmPlayer.clip = gameBGM;
        bgmPlayer.Play();
    }

    public void PlayGameSFX(string name, float volume = 1.0f)
    {
        if (!audioClipDic.ContainsKey(name))
        {
            print(name + " is not contained audioClipDic");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipDic[name], volume);
    }

    public void StopGameBGM()
    {
        bgmPlayer.Stop();
    }
}
