using UnityEngine.UI;

public class SettingsPanel : Panel
{
    public Slider sound, music;
    public Text language, difficultyText;

    bool isOpen;
    DIFFICULTY diff;
    DIFFICULTY currentDifficulty
    {
        get
        {
            return DifficultyManager.Instance.CurrentDifficulty;
        }
    }
    string[] langStrings = { "English", "Русский", "Украiнська" };
    
    void Start()
    {
        diff = currentDifficulty;
        ChangeDifficultyLabel();
        LanguageManager.Instance.onLanguageChanged.AddAction(ChangeDifficultyLabel);
        sound.value = PreferenceManager.SoundVolume;
        music.value = PreferenceManager.MusicVolume;
        language.text = langStrings[(int)LanguageManager.Instance.Language];
    }

    public void Show()
    {
        gameObject.SetActive(isOpen = true);
        //previous = UIScroller.Instance.currentPanel;
        //UIScroller.Instance.currentPanel = this;
    }

    public void Hide()
    {
        PreferenceManager.MusicVolume = music.value;
        PreferenceManager.SoundVolume = sound.value;
        DifficultyManager.Instance.SetDifficulty(diff);
        PreferenceManager.Difficulty = diff;
        gameObject.SetActive(isOpen = false);
        //UIScroller.Instance.currentPanel = previous;
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
        diff = diff == DIFFICULTY.HARD ? DIFFICULTY.EASY : diff + 1;
        ChangeDifficultyLabel();
    }

    void ChangeDifficultyLabel()
    {
        switch (LanguageManager.Instance.Language)
        {
            case LANGUAGE.ENG:
                switch (diff)
                {
                    case DIFFICULTY.EASY:
                        difficultyText.text = "Easy";
                        break;
                    case DIFFICULTY.NORAML:
                        difficultyText.text = "Normal";
                        break;
                    case DIFFICULTY.HARD:
                        difficultyText.text = "Hard";
                        break;
                    default:
                        break;
                }
                break;
            case LANGUAGE.RUS:
                switch (diff)
                {
                    case DIFFICULTY.EASY:
                        difficultyText.text = "Легко";
                        break;
                    case DIFFICULTY.NORAML:
                        difficultyText.text = "Нормально";
                        break;
                    case DIFFICULTY.HARD:
                        difficultyText.text = "Тяжело";
                        break;
                    default:
                        break;
                }
                break;
            case LANGUAGE.UA:
                switch (diff)
                {
                    case DIFFICULTY.EASY:
                        difficultyText.text = "Легко";
                        break;
                    case DIFFICULTY.NORAML:
                        difficultyText.text = "Нормально";
                        break;
                    case DIFFICULTY.HARD:
                        difficultyText.text = "Тяжко";
                        break;
                    default:
                        break;
                }
                break;
        }
    }


    public override string ToString()
    {
        return "Settings panel";
    }
}
