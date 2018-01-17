using UnityEngine;
using System;

public abstract class Panel : MonoBehaviour
{
    public Panel prevPanel;

    public virtual event Action onCompleteShow, onCompleteHide;

    public abstract void Show();
    public abstract void Hide();
}
