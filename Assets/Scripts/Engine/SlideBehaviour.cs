﻿using UnityEngine.EventSystems;
using UnityEngine;

public enum SLIDE_DIRECTION { NONE, TOP, DOWN, LEFT, RIGHT }

public abstract class SlideBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected EngineEvent onDownSlide, onTopSlide, onLeftSlide, onRightSlide, onAnySlide;
    protected SLIDE_DIRECTION slideDirection { get; private set; }

    const float minDistToCompleteSlide = 3f;

    Vector3 touchPosition, currentPosition;

    protected virtual void OnEnable()
    {
        onAnySlide = new EngineEvent();
        onDownSlide = new EngineEvent();
        onTopSlide = new EngineEvent();
        onRightSlide = new EngineEvent();
        onLeftSlide = new EngineEvent();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchPosition = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(touchPosition, currentPosition) > 0.2f)
        {
            float angle = Vector2.Angle(currentPosition - touchPosition, Vector2.right);
            Vector3 cross = Vector3.Cross(currentPosition - touchPosition, Vector2.right);
            if (cross.z > 0)
                angle = 360 - angle;
            MoveItem(angle);
        }
    }
    
    private void MoveItem(float angle)
    {
        if (angle < 135 && angle >= 45)
        {
            slideDirection = SLIDE_DIRECTION.TOP;
            if (onTopSlide != null)
                onTopSlide.Execute();
        }
        else if (angle < 225 && angle >= 135)
        {
            slideDirection = SLIDE_DIRECTION.LEFT;
            if (onLeftSlide != null)
                onLeftSlide.Execute();
        }
        else if (angle < 315 && angle >= 225)
        {
            slideDirection = SLIDE_DIRECTION.DOWN;
            if (onDownSlide != null)
                onDownSlide.Execute();
        }
        else if (angle < 45 && angle >= 0 || angle >= 315)
        {
            slideDirection = SLIDE_DIRECTION.RIGHT;
            if (onRightSlide != null)
                onRightSlide.Execute();
        }
        if (onAnySlide != null)
            onAnySlide.Execute();
    }
}
