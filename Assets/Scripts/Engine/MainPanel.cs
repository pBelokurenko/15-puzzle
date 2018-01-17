using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MainPanel : Panel
{
    public Animator anim;

    void Start()
    {
        StartCoroutine("Play");
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
        StopAllCoroutines();
        anim.DOKill(true);
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        GameManager.Instance.SetPanel(this);
        StartCoroutine("Play");
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(5);
        anim.Play("Main");
    }
}
