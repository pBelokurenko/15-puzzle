using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    Toggle t;

    void Start()
    {
        t = GetComponent<Toggle>();
        t.isOn = !(SoundManager.Instance.IsMusicOn = PreferenceManager.IsMusicOn);
    }

    public void OnClick()
    {
        PreferenceManager.IsMusicOn = SoundManager.Instance.IsMusicOn = !t.isOn;
    }
}