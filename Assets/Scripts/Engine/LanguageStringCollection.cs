using System.Collections.Generic;

public enum LANGUAGE : byte { ENG, RUS, UA }

public class LanguageStringsCollection : Dictionary<LANGUAGE, Dictionary<string, string>>
{
    public LanguageStringsCollection() : this(BuildLanguageStrings()) { }

    public LanguageStringsCollection(Dictionary<LANGUAGE, Dictionary<string, string>> strings)
    {
        foreach (var item in strings)
            Add(item.Key, item.Value);
    }

    #region Strings 
    public static Dictionary<LANGUAGE, Dictionary<string, string>> BuildLanguageStrings()
    {
        return new Dictionary<LANGUAGE, Dictionary<string, string>>()
        {
            { 
                #region Eng 
                LANGUAGE.ENG, new Dictionary<string, string>()
                { 
                    #region Settings Menu 
                    { "SoundVolume", "Sound:" },
                    { "MusicVolume", "Music:" }
                    #endregion
                }
                #endregion
            },
            {
                LANGUAGE.RUS, new Dictionary<string, string>()
                { 
                    #region Settings Menu 
                    { "SoundVolume", "Звуки:" },
                    { "MusicVolume", "Музыка:" }
                    #endregion
                }
            },
            {
                LANGUAGE.UA, new Dictionary<string, string>()
                { 
                    #region Settings Menu 
                    { "SoundVolume", "Звуки:" },
                    { "MusicVolume", "Музика:" }
                    #endregion
                }
            }
        };
    }
    #endregion
}