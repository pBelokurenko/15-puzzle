public class GameManager : Singleton<GameManager>
{
    public bool isGameStarted { get; private set; }
    
    protected override void SingletonStarted()
    {
        //DifficultyManager.Instance.onDifficultyChanges += ()=>
        //{
        //    if (!isGameStarted)
        //        StartGame();
        //};
    }

    public void StartGame()
    {

    }

    public void PlayClick()
    {

    }

    public void Back()
    {

    }
    
}
