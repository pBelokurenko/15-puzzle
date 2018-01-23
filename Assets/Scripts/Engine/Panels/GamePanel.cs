using System;
using DG.Tweening;
using UnityEngine;

public class GamePanel : Panel
{
    public override void Show(SLIDE_DIRECTION dir)
    {
        base.Show(dir);
        BoardManager.Instance.RefreshField();
    }

    public override string ToString()
    {
        return "Game panel";
    }
}
