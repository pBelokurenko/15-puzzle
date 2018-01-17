public class LanguageManager :  Singleton<LanguageManager>
{
    LANGUAGE language = LANGUAGE.ENG;
    LanguageStringsCollection collection = null;

    public event System.Action onLanguageChanged;

    protected override void SingletonAwakened()
    {
        Load();
    }

    public string GetString(string name)
    {
        return collection[language].ContainsKey(name) ? collection[language][name] : "";
    }

    public LANGUAGE Language
    {
        get
        {
            return language;
        }
        set
        {
            language = value;
            if (onLanguageChanged != null)
                onLanguageChanged();
        }
    }

    void Load()
    {
        collection = new LanguageStringsCollection();
        Language = PreferenceManager.Language;
    }
}