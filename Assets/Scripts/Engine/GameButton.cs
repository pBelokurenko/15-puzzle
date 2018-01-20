using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : SlideBehaviour
{
    public Text label;

    public int index { get; private set; } //current button index in Array
    public int startIndex { get; private set; }
    public bool isEmptyCell { get; set; }
    public bool IsCollisionEnabled
    {
        get
        {
            return isCollisionEnabled;
        }
        set
        {
            isCollisionEnabled = value;
            if (value)
                Activate();
            else
                Deactivate();
        }
    }

    const float time = 0.25f;
    bool isCollisionEnabled = true;
    BoardManager controller;

    float size
    {
        get
        {
            return DifficultyManager.Instance.fillStep;
        }
    }

    void Activate()
    {
        controller = BoardManager.Instance;
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        GetComponent<Collider2D>().enabled = true;
        isEmptyCell = false;
        onAnySlide.AddAction(NotifyController);
    }

    public void Deactivate()
    {
        label.text = " ";
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GetComponent<Collider2D>().enabled = false;
        isEmptyCell = true;
    }

    public void Move(SLIDE_DIRECTION dir)
    {
        switch (dir)
        {
            case SLIDE_DIRECTION.TOP:
                transform.DOKill(true);
                transform.DOLocalMoveY(transform.localPosition.y + size, time);
                break;
            case SLIDE_DIRECTION.DOWN:
                transform.DOKill(true);
                transform.DOLocalMoveY(transform.localPosition.y - size, time);
                break;
            case SLIDE_DIRECTION.LEFT:
                transform.DOKill(true);
                transform.DOLocalMoveX(transform.localPosition.x - size, time);
                break;
            case SLIDE_DIRECTION.RIGHT:
                transform.DOKill(true);
                transform.DOLocalMoveX(transform.localPosition.x + size, time);
                break;
        }
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void SetIndex(string str)
    {
        int result = -1;
        if (int.TryParse(str, out result))
            index = result;

    }

    void NotifyController()
    {
        controller.MoveButtons(this, slideDirection);
    }
}
