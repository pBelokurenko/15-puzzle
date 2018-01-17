using UnityEngine.UI;

public class SettingsPanel : Panel
{
    public Slider sound, music;
    public Text language, difficulty;

    bool isOpen;
    string[] langStrings = { "English", "Русский", "Украiнська" };

    void Start()
    {
        sound.value = PreferenceManager.SoundVolume;
        music.value = PreferenceManager.MusicVolume;
        language.text = langStrings[(int)LanguageManager.Instance.Language];
    }

    public override void Show()
    {
        gameObject.SetActive(isOpen = true);
    }

    public override void Hide()
    {
        PreferenceManager.MusicVolume = music.value;
        PreferenceManager.SoundVolume = sound.value;
        gameObject.SetActive(isOpen = false);
    }

    public void OnTap()
    {
        if (isOpen = !isOpen)
            Show();
        else
            Hide();
    }

    public void ChangeLanguage()
    {
        SoundManager.Instance.PlaySound("Click");
        LANGUAGE lang = LanguageManager.Instance.Language;
        lang = lang == LANGUAGE.UA ? LANGUAGE.ENG : lang + 1;
        language.text = langStrings[(int)(LanguageManager.Instance.Language = lang)];
        PreferenceManager.Language = lang;
    }

    public void SetSoundVolume()
    {
        SoundManager.Instance.SoundVolume = sound.value;
    }

    public void SetMusicVolume()
    {
        SoundManager.Instance.MusicVolume = music.value;
    }

    public void ChangeDifficulty()
    {
        SoundManager.Instance.PlaySound("Click");
        DIFFICULTY diff = DifficultyManager.Instance.CurrentDifficulty;
        diff = diff == DIFFICULTY.HARD ? DIFFICULTY.EASY : diff + 1;
        DifficultyManager.Instance.CurrentDifficulty = diff;
        PreferenceManager.Difficulty = diff;
    }
}
