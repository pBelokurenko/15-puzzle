using System;
using DG.Tweening;
using UnityEngine;

public class GamePanel : Panel
{
    void Start()
    {
        BoardManager.Instance.FillField();
    }

    public override string ToString()
    {
        return "Game panel";
    }
}
