using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MainPanel : Panel
{
    public Animator anim;

    private void Start()
    {
        BoardManager.Instance.InstantiateButtons();
    }

    public override void Hide(SLIDE_DIRECTION dir)
    {
        base.Hide(dir);
        StopAllCoroutines();
        anim.DOKill(true);
    }

    public override void Show(SLIDE_DIRECTION dir)
    {
        base.Show(dir);
        if (isActiveAndEnabled)
            StartCoroutine("Play");
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(5);
        anim.Play("Main");
    }

    public override string ToString()
    {
        return "Main panel";
    }
}
