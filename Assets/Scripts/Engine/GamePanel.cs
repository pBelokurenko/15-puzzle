using System;
using DG.Tweening;
using UnityEngine;

public class GamePanel : Panel
{
    public Transform game;

    const float time = 2f; //show or hide time

    public override event Action onCompleteHide;
    public override event Action onCompleteShow;

    public override void Hide()
    {
        game.DOKill(false);
        game.DOMoveX(4.8f, Mathf.Abs(4.8f - game.position.x) / 2).OnComplete(()=>
        {
            if (onCompleteHide != null)
                onCompleteHide();
        }).SetEase(Ease.Linear);
    }

    public override void Show()
    {
        game.DOKill(false);
        GameManager.Instance.SetPanel(this);
        gameObject.SetActive(true);
        game.DOMoveX(0, Mathf.Abs(0 - game.position.x) / 2).OnComplete(() =>
        {
            if (onCompleteShow != null)
                onCompleteShow();
        }).SetEase(Ease.Linear);
    }

    void Start()
    {
        onCompleteShow += GameManager.Instance.mainPanel.Hide;
    }
}
