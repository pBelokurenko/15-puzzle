using UnityEngine;

public static class PreferenceManager
{
    const string soundVolume = "SoundVolume";
    const string musicVolume = "MusicVolume";
    const string language = "Language";
    const string isMusicOn = "IsMusicOn";
    const string difficulty = "Difficulty";

    public static float SoundVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(soundVolume, 1.0f);
        }
        set
        {
            PlayerPrefs.SetFloat(soundVolume, value);
        }
    }

    public static float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(musicVolume, 1.0f);
        }
        set
        {
            PlayerPrefs.SetFloat(musicVolume, value);
        }
    }

    public static LANGUAGE Language
    {
        get
        {
            return (LANGUAGE)PlayerPrefs.GetInt(language, 0);
        }
        set
        {
            PlayerPrefs.SetInt(language, (int)value);
        }
    }

    public static bool IsMusicOn
    {
        get
        {
            return PlayerPrefs.GetInt("isMusicOn", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("isMusicOn", value ? 1 : 0);
        }
    }

    public static DIFFICULTY Difficulty
    {
        get
        {
            return (DIFFICULTY)PlayerPrefs.GetInt(language, 0);
        }
        set
        {
            PlayerPrefs.SetInt(language, (int)value);
        }
    }
}