using UnityEngine;

public class UIScroller : Singleton<UIScroller>
{
    public Panel startPanel;

    public Panel currentPanel { get; private set; }
    public bool IsActive { get; private set; }

    float minDistToCompleteSlide = 0.3f;
    const float disableTime = Panel.slideTime;

    float timeLeft;

    protected override void SingletonStarted()
    {
        currentPanel = startPanel;
        IsActive = true;
    }

    private void MoveItem(float angle)
    {
        if (angle < 225 && angle >= 135) // left slide
        {
            currentPanel.Hide(SLIDE_DIRECTION.LEFT);
            currentPanel.next.Show(SLIDE_DIRECTION.LEFT);
            currentPanel = currentPanel.next;
        }
        else if (angle < 45 && angle >= 0 || angle >= 315) // right slide
        {
            currentPanel.Hide(SLIDE_DIRECTION.RIGHT);
            currentPanel.previous.Show(SLIDE_DIRECTION.RIGHT);
            currentPanel = currentPanel.previous;
        }
        if (currentPanel as GamePanel != null)
            minDistToCompleteSlide = 3f;
        else
            minDistToCompleteSlide = 0.3f;
        //if (angle < 315 && angle >= 225) - DOWN
        //if (angle < 135 && angle >= 45) - TOP
    }

    #region Slide
    Vector3 touchPosition, currentPosition;
    void Update()
    {
        if (IsActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(touchPosition, currentPosition) > minDistToCompleteSlide)
                {
                    StartTimer();
                    float angle = Vector2.Angle(currentPosition - touchPosition, Vector2.right);
                    Vector3 cross = Vector3.Cross(currentPosition - touchPosition, Vector2.right);
                    if (cross.z > 0)
                        angle = 360 - angle;
                    MoveItem(angle);
                }
            }
        }
        else if ((timeLeft -= Time.deltaTime) < 0)
            StopTimer();
    }
    #endregion

    void StartTimer()
    {
        IsActive = false;
        timeLeft = disableTime;
    }

    void StopTimer()
    {
        IsActive = true;
    }
}
