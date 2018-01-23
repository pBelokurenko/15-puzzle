public class LanguageManager :  Singleton<LanguageManager>
{
    LANGUAGE language = LANGUAGE.ENG;
    LanguageStringsCollection collection = null;

    public EngineEvent onLanguageChanged;

    protected override void SingletonAwakened()
    {
        onLanguageChanged = new EngineEvent();
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
            onLanguageChanged.Execute();
        }
    }

    void Load()
    {
        collection = new LanguageStringsCollection();
        Language = PreferenceManager.Language;
    }
}