using UnityEngine.UI;

public class SettingsPanel : Panel
{
    public Slider sound, music;
    public Text language;
    
    bool isOpen;
    string[] langStrings = { "English", "Русский", "Украiнська" };
    
    void Start()
    {
        sound.value = PreferenceManager.SoundVolume;
        music.value = PreferenceManager.MusicVolume;
        language.text = langStrings[(int)LanguageManager.Instance.Language];
    }

    public void Show()
    {
        //previous = UIScroller.Instance.currentPanel;
        //UIScroller.Instance.currentPanel = this;
        gameObject.SetActive(isOpen = true);
    }

    public void Hide()
    {
        //UIScroller.Instance.currentPanel = previous;
        PreferenceManager.MusicVolume = music.value;
        PreferenceManager.SoundVolume = sound.value;
        gameObject.SetActive(isOpen = false);
        //previous = null;
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

    public override string ToString()
    {
        return "Settings panel";
    }
}
