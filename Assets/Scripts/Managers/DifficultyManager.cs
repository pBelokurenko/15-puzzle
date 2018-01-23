using UnityEngine.UI;
using UnityEngine;

public enum DIFFICULTY { EASY, NORAML, HARD }

public class DifficultyManager : Singleton<DifficultyManager>
{
    public Object buttonPrefab;

    public DIFFICULTY CurrentDifficulty {
        get
        {
            return currentDifficulty;
        }
    }
    public Object CurrentPrefab { get; private set; }
    public int CurrentFieldSize { get; private set; }
    public float scaleRate { get; private set; }
    public float fillStep { get; private set; }
    public Vector3 initialPoint { get; private set; }
    public string winCondition { get; private set; }
    DIFFICULTY currentDifficulty;

    public EngineEvent onDifficultyChanges;

    protected override void SingletonAwakened()
    {
        onDifficultyChanges = new EngineEvent();
        currentDifficulty = PreferenceManager.Difficulty;
        onDifficultyChanges.AddAction(ChangeSettingsVariables);
        CurrentPrefab = buttonPrefab;
        onDifficultyChanges.Execute();
    }

    public void SetDifficulty(DIFFICULTY diff)
    {
        currentDifficulty = diff;
        onDifficultyChanges.Execute();
    }

    public void ChangeSettingsVariables()
    {
        switch (currentDifficulty)
        {
            case DIFFICULTY.EASY:
                initialPoint = new Vector3(-158f, 80f, 0);
                fillStep = 158f;
                CurrentFieldSize = 3;
                scaleRate = 100f;
                winCondition = "12345678 ";
                break;
            case DIFFICULTY.NORAML:
                initialPoint = new Vector3(-176f, 77f, 0);
                fillStep = 118f;
                CurrentFieldSize = 4;
                scaleRate = 75f;
                winCondition = "123456789101112131415 ";
                break;
            case DIFFICULTY.HARD:
                initialPoint = new Vector3(-187f, 75f, 0);
                fillStep = 94f;
                CurrentFieldSize = 5;
                scaleRate = 60f;
                winCondition = "123456789101112131415161718192021222324 ";
                break;
        }
    }
}
