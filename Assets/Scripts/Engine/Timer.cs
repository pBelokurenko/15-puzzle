using DG.Tweening;
using UnityEngine;

public enum TIMER_MODE { INCREASING, DECREASING, FREE }

public class Timer : Singleton<Timer>
{
    public TIMER_MODE Mode { get; set; }

    public bool IsActive
    {
        get
        {
            return isTimerActive;
        }
    }

    public float currentTimerValue { get; private set; }
    float maxTime
    {
        get
        {
            switch (DifficultyManager.Instance.CurrentDifficulty)
            {
                case DIFFICULTY.EASY:
                    return 20f;
                case DIFFICULTY.NORAML:
                    return 50f;
                case DIFFICULTY.HARD:
                    return 120f;
                default:
                    throw new UnityException("Something goes wrong");
            }
        }
    }

    bool isTimerActive;

    public void StartTimer()
    {
        switch (Mode)
        {
            case TIMER_MODE.INCREASING:
                currentTimerValue = 0;
                break;
            case TIMER_MODE.DECREASING:
                currentTimerValue = maxTime;
                break;
        }
        isTimerActive = true;
    }

    public void SetPause(bool isPaused)
    {
        isTimerActive = !isPaused;
    }

    public void StopTimer()
    {

    }

    void Update()
    {
        if (isTimerActive)
        {
            switch (Mode)
            {
                case TIMER_MODE.INCREASING:
                    if (currentTimerValue < maxTime)
                        currentTimerValue += Time.deltaTime;
                    break;
                case TIMER_MODE.DECREASING:
                    if (currentTimerValue > 0)
                        currentTimerValue -= Time.deltaTime;
                    break;
                case TIMER_MODE.FREE:
                    currentTimerValue += Time.deltaTime;
                    break;
            }
        }
    }
}
