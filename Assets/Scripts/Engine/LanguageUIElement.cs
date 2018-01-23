using UnityEngine;
using UnityEngine.UI;

public class LanguageUIElement : MonoBehaviour
{
    public Text text;
    public string stringName;

    void Start()
    {
        SetLanguage();
        LanguageManager.Instance.onLanguageChanged.AddAction(SetLanguage);
    }

    public void SetLanguage()
    {
        text.text = LanguageManager.Instance.GetString(stringName);
    }
}