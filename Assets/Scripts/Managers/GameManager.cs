using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Panel mainPanel;
    public Panel settingsPanel;
    public Panel gamePanel;
    public Sprite exitSprite;

    public Panel CurrentPanel { get; private set; }
    public bool isGameStarted { get; private set; }
    public Panel PrevPanel
    {
        get
        {
            return CurrentPanel.prevPanel;
        }
    }

    protected override void SingletonStarted()
    {
        DifficultyManager.Instance.onDifficultyChanges += ()=>
        {
            if (isGameStarted)
                StartGame();
        };
        CurrentPanel = mainPanel;
    }

    public void StartGame()
    {

    }

    public void PlayClick()
    {
        if (!isGameStarted)
        {
            gamePanel.Show();
            BoardManager.Instance.FillField();
            StartGame();
            isGameStarted = true;
        }
    }

    public void Back()
    {
        if (CurrentPanel.prevPanel == null)
            Application.Quit();
        else
        {
            CurrentPanel.Hide();
            PrevPanel.Show();
        }
    }

    public void SetPanel(Panel panel)
    {
        Debug.Log("Panel is " + panel);
        CurrentPanel = panel;
    }
}
