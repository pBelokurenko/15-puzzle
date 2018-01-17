using UnityEngine;
using DG.Tweening;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource music;

    AudioClip[] soundsCache;

    public bool IsMusicOn
    {
        get
        {
            return !music.mute;
        }
        set
        {
            music.mute = !value;
        }
    }
    public float MusicVolume
    {
        get
        {
            return music.volume;
        }
        set
        {
            music.volume = value;
        }
    }
    public float SoundVolume { get; set; }

    protected override void SingletonStarted()
    {
        base.SingletonStarted();
        Init();
    }

    void Init()
    {
        soundsCache = Resources.LoadAll<AudioClip>("Sounds");
        music.loop = true;
        music.mute = !PreferenceManager.IsMusicOn;
        SoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        Play();
    }

    public void PlaySound(string name, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(FindByName(name), Camera.main.transform.position, volume == 1 ? SoundVolume : volume);
    }

    AudioClip FindByName(string name)
    {
        for (int i = 0; i < soundsCache.Length; i++)
            if ((soundsCache[i]).name.Equals(name))
                return soundsCache[i];
        return null;
    }

    void Play()
    {
        if (MusicVolume != 0)
        {
            music.volume = 0;
            music.DOFade(PreferenceManager.MusicVolume, 4f);
        }
        music.Play();
    }
}

