using DG.Tweening;
using UnityEngine;

public abstract class Panel : MonoBehaviour, IShowable, IHideable
{
    public Panel next, previous;

    const Ease ease = Ease.Linear;
    public const float slideTime = 1f;
    
    public virtual void Show(SLIDE_DIRECTION dir)
    {
        gameObject.SetActive(true);
        transform.DOKill();
        transform.localPosition = new Vector3(dir == SLIDE_DIRECTION.LEFT ? 480f : -480f, 40f, 10);
        transform.DOLocalMoveX(0, slideTime).SetEase(ease);
    }

    public virtual void Hide(SLIDE_DIRECTION dir)
    {
        transform.DOKill();
        transform.DOLocalMoveX(dir == SLIDE_DIRECTION.LEFT ? -480f : 480f, slideTime).OnComplete(() => gameObject.SetActive(false)).SetEase(ease);
    }

    public override string ToString()
    {
        return "Panel";
    }
    
}
