﻿using UnityEngine.UI;
using UnityEngine;

public enum DIFFICULTY { EASY, NORAML, HARD }

public class DifficultyManager : Singleton<DifficultyManager>
{
    public Text difficultyText;
    public Object buttonPrefab;

    public DIFFICULTY CurrentDifficulty {
        get
        {
            return currentDifficulty;
        }
        set
        {
            currentDifficulty = value;
            if (onDifficultyChanges != null)
                onDifficultyChanges();
        }
    }
    public Object CurrentPrefab { get; private set; }
    public int CurrentFieldSize { get; private set; }
    public float scaleRate { get; private set; }
    public float fillStep { get; private set; }
    public Vector3 initialPoint { get; private set; }
    public string winCondition { get; private set; }
    DIFFICULTY currentDifficulty;

    public event System.Action onDifficultyChanges;

    protected override void SingletonAwakened()
    {
        CurrentDifficulty = PreferenceManager.Difficulty;
        LanguageManager.Instance.onLanguageChanged += ChangeDifficultyLabel;
        onDifficultyChanges += ChangeDifficultyLabel;
        onDifficultyChanges += ChangeSettingsVariables;
        CurrentPrefab = buttonPrefab;
        if (onDifficultyChanges != null)
            onDifficultyChanges();
    }

    public void ChangeDifficultyLabel()
    {
        switch (LanguageManager.Instance.Language)
        {
            case LANGUAGE.ENG:
                switch (currentDifficulty)
                {
                    case DIFFICULTY.EASY:
                        difficultyText.text = "Easy";
                        break;
                    case DIFFICULTY.NORAML:
                        difficultyText.text = "Normal";
                        break;
                    case DIFFICULTY.HARD:
                        difficultyText.text = "Hard";
                        break;
                    default:
                        break;
                }
                break;
            case LANGUAGE.RUS:
                switch (currentDifficulty)
                {
                    case DIFFICULTY.EASY:
                        difficultyText.text = "Легко";
                        break;
                    case DIFFICULTY.NORAML:
                        difficultyText.text = "Нормально";
                        break;
                    case DIFFICULTY.HARD:
                        difficultyText.text = "Тяжело";
                        break;
                    default:
                        break;
                }
                break;
            case LANGUAGE.UA:
                switch (currentDifficulty)
                {
                    case DIFFICULTY.EASY:
                        difficultyText.text = "Легко";
                        break;
                    case DIFFICULTY.NORAML:
                        difficultyText.text = "Нормально";
                        break;
                    case DIFFICULTY.HARD:
                        difficultyText.text = "Тяжко";
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    public void ChangeSettingsVariables()
    {
        switch (currentDifficulty)
        {
            case DIFFICULTY.EASY:
                initialPoint = new Vector3(-1.58f, 0.8f, 0);
                fillStep = 1.58f;
                CurrentFieldSize = 3;
                scaleRate = 1.0f;
                winCondition = "12345678 ";
                break;
            case DIFFICULTY.NORAML:
                initialPoint = new Vector3(-1.76f, 0.77f, 0);
                fillStep = 1.18f;
                CurrentFieldSize = 4;
                scaleRate = 0.75f;
                winCondition = "123456789101112131415 ";
                break;
            case DIFFICULTY.HARD:
                initialPoint = new Vector3(-1.87f, 0.75f, 0);
                fillStep = 0.94f;
                CurrentFieldSize = 5;
                scaleRate = 0.6f;
                winCondition = "123456789101112131415161718192021222324 ";
                break;
        }
    }
}