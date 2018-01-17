using DG.Tweening;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    public GameButton[] buttons { get; private set; }
    public int buttonCount
    {
        get
        {
            return DifficultyManager.Instance.CurrentFieldSize * DifficultyManager.Instance.CurrentFieldSize;
        }
    }
    public float step
    {
        get
        {
            return DifficultyManager.Instance.fillStep;
        }
    }
    public int fieldSize
    {
        get
        {
            return DifficultyManager.Instance.CurrentFieldSize;
        }
    }
    public Vector3 initialPosition
    {
        get
        {
            return DifficultyManager.Instance.initialPoint;
        }
    }
    public Object CurrentPrefab
    {
        get
        {
            return DifficultyManager.Instance.CurrentPrefab;
        }
    }
    public int emptyButtonIndex { get; private set; }

    FieldGenerator<GameButton> fg;
    DIFFICULTY currentDiffculty, prevDifficulty;
    float scaleRate
    {
        get
        {
            return DifficultyManager.Instance.scaleRate;
        }
    }

    protected override void SingletonStarted()
    {
        buttons = new GameButton[25];
        InstantiateButtons();
        DifficultyManager.Instance.onDifficultyChanges += ChangeScale;
        DifficultyManager.Instance.onDifficultyChanges += FillField;
        prevDifficulty = currentDiffculty = DifficultyManager.Instance.CurrentDifficulty;
    }

    void InstantiateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = Instantiate((GameObject)CurrentPrefab, ((GamePanel)GameManager.Instance.gamePanel).game.transform).GetComponent<GameButton>();
            buttons[i].name = "Button" + i;
            buttons[i].transform.position = new Vector3(15, 0, 0);
            buttons[i].transform.DOScale(new Vector3(scaleRate, scaleRate, 1), 0f);
            if (i != 0)
            {
                buttons[i].label.text = i.ToString();
                buttons[i].IsCollisionEnabled = true;
            }
            else
                buttons[i].IsCollisionEnabled = false;
            buttons[i].SetIndex(i);
        }
    }

    public void FillField()
    {
        #region
        //when difficulty changes from hard to easy we need to hide spare buttons
        if (DifficultyManager.Instance.CurrentDifficulty == DIFFICULTY.EASY)
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].transform.position = new Vector3(15, 0, 0);

        for (int i = 0; i < buttonCount; i++)
        {
            if (i == 0)
            {
                buttons[i].transform.localPosition = initialPosition;
                continue;
            }
            else if (i % Mathf.Sqrt(buttonCount) == 0 && i - 1 != 0)
                buttons[i].transform.localPosition = new Vector3(initialPosition.x, buttons[i - 1].transform.localPosition.y - step, initialPosition.z);
            else
                buttons[i].transform.localPosition = new Vector3(buttons[i - 1].transform.localPosition.x + step, buttons[i - 1].transform.localPosition.y, initialPosition.z);
        }
        #endregion

        Shuffle();
    }

    void ChangeScale()
    {
        float scaleRate = DifficultyManager.Instance.scaleRate;
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].transform.DOScale(new Vector3(scaleRate, scaleRate, 1), 0f);
    }

    bool HasSolution()
    {
        int pairs = 0;
        int firstDigit = 0, secondDigit = 0;

        for (int i = 0; i < buttonCount; i++)
        {
            if (int.TryParse(buttons[i].label.text, out firstDigit))
            {
                for (int j = i + 1; j < buttonCount; j++)
                {
                    if (int.TryParse(buttons[j].label.text, out secondDigit))
                    {
                        if (firstDigit > secondDigit)
                            pairs++;
                    }
                    else
                        continue;
                }
            }
        }
        return (pairs % 2) + 1 == 1;
    }

    void Shuffle()
    {

        for (int i = 0; i < 25; i++)
        {
            buttons[i].SetIndex(i);
            buttons[i].label.text = i.ToString();
            buttons[i].IsCollisionEnabled = true;
        }
        buttons[0].IsCollisionEnabled = false;

        do
        {
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            int temp;

            for (int i = 0; i < buttonCount; i++)
                    list.Add(buttons[i].label.text);

            for (int i = 0; i < buttonCount; i++)
            {
                temp = Random.Range(0, list.Count - 1);
                buttons[i].label.text = list[temp];
                if (buttons[i].label.text == " ")
                    emptyButtonIndex = i;
                list.RemoveAt(temp);
            }
        } while (!HasSolution());

        if (buttons[0] != buttons[emptyButtonIndex])
        {
            buttons[emptyButtonIndex].IsCollisionEnabled = false;
            buttons[0].IsCollisionEnabled = true;
        }

    }

    void Check()
    {
        string str = "";
        for (int i = 0; i < buttonCount; i++)
            str += buttons[i].label.text;

        if (str != DifficultyManager.Instance.winCondition)
            return;
        else
            Debug.Log("win");

    }
    
    public void MoveButtons(GameButton button, SLIDE_DIRECTION dir)
    {
        int row = (buttonCount - (buttonCount - button.index)) / fieldSize;
        int[] indexes = new int[(int)Mathf.Sqrt(buttonCount)];
        int currentIndex = 0;
        for (int i = 0; i < indexes.Length; i++)
            indexes[i] = -1;

        #region
        switch (dir)
        {
            //    case SLIDE_DIRECTION.TOP:
            //        for (int i = button.index; i >= 0; i -= fieldSize)
            //        {
            //            if (i - fieldSize < 0 && (i != 0 && !buttons[i].isEmptyCell))
            //                return;
            //            else if (!buttons[i].isEmptyCell)
            //                indexes[currentIndex++] = buttons[i].index;
            //            if (buttons[i].isEmptyCell)
            //            {
            //                GameButton temp = buttons[emptyButtonIndex];
            //                for (int j = indexes.Length - 1; j >= 0; j--)
            //                    if (indexes[j] >= 0)
            //                    {
            //                        buttons[indexes[j] - fieldSize] = buttons[indexes[j]];
            //                        buttons[indexes[j] - fieldSize].index = indexes[j] - fieldSize;
            //                        buttons[indexes[j]].Move(dir);
            //                    }
            //                if (indexes[0] >= 0)
            //                    buttons[indexes[0]] = temp;
            //                emptyButtonIndex = temp.index = indexes[0];
            //                break;
            //            }
            //        }
            //        break;
            //    case SLIDE_DIRECTION.DOWN:
            //        for (int i = button.index; i < buttonCount; i += fieldSize)
            //        {
            //            if (i + fieldSize >= buttonCount && (i != 0 && !buttons[i].isEmptyCell))
            //                return;
            //            else if (!buttons[i].isEmptyCell)
            //                indexes[currentIndex++] = buttons[i].index;
            //            if (buttons[i].isEmptyCell)
            //            {
            //                GameButton temp = buttons[emptyButtonIndex];
            //                for (int j = indexes.Length - 1; j >= 0; j--)
            //                    if (indexes[j] >= 0)
            //                    {
            //                        buttons[indexes[j] + fieldSize] = buttons[indexes[j]];
            //                        buttons[indexes[j] + fieldSize].index = indexes[j] + fieldSize;
            //                        buttons[indexes[j]].Move(dir);
            //                    }
            //                if (indexes[0] >= 0)
            //                    buttons[indexes[0]] = temp;
            //                emptyButtonIndex = temp.index = indexes[0];
            //                break;
            //            }
            //        }
            //        break;
            //    case SLIDE_DIRECTION.LEFT:
            //        for (int i = button.index; i >= row * fieldSize; i--)
            //        {
            //            if ((i - 1 < 0 || i % fieldSize == 0) && (i != 0 && !buttons[i].isEmptyCell))
            //                return;
            //            else if (!buttons[i].isEmptyCell)
            //                indexes[currentIndex++] = buttons[i].index;
            //            if (buttons[i].isEmptyCell)
            //            {
            //                GameButton temp = buttons[emptyButtonIndex];
            //                for (int j = indexes.Length - 1; j >= 0; j--)
            //                    if (indexes[j] >= 0)
            //                    {
            //                        buttons[indexes[j] - 1] = buttons[indexes[j]];
            //                        buttons[indexes[j] - 1].index = indexes[j] - 1;
            //                        buttons[indexes[j]].Move(dir);
            //                    }
            //                if (indexes[0] >= 0)
            //                    buttons[indexes[0]] = temp;
            //                emptyButtonIndex = temp.index = indexes[0];
            //                break;
            //            }
            //        }
            //        break;
            //    case SLIDE_DIRECTION.RIGHT:
            //        for (int i = button.index; i <= ((row + 1) * fieldSize) - 1; i++)
            //        {
            //            if ((i + 1 > buttonCount || (i + 1) % fieldSize == 0) && (i != 0 && !buttons[i].isEmptyCell))
            //                return;
            //            else if (!buttons[i].isEmptyCell)
            //                indexes[currentIndex++] = buttons[i].index;
            //            if (buttons[i].isEmptyCell)
            //            {
            //                GameButton temp = buttons[emptyButtonIndex];
            //                for (int j = indexes.Length - 1; j >= 0; j--)
            //                    if (indexes[j] >= 0)
            //                    {
            //                        buttons[indexes[j] + 1] = buttons[indexes[j]];
            //                        buttons[indexes[j] + 1].index = indexes[j] + 1;
            //                        buttons[indexes[j]].Move(dir);
            //                    }
            //                if (indexes[0] >= 0)
            //                    buttons[indexes[0]] = temp;
            //                emptyButtonIndex = temp.index = indexes[0];
            //                break;
            //            }
            //        }
            //        break;
        }
        #endregion
        Debug.Log(button.index);
        Debug.Log(emptyButtonIndex);
        switch (dir)
        {
            case SLIDE_DIRECTION.TOP:
                for (int i = button.index; i >= 0; i -= fieldSize)
                {
                    if (i - fieldSize < 0) //out of bounds
                        if (buttons[i].isEmptyCell)
                        {
                            GameButton temp = buttons[emptyButtonIndex];
                            for (int j = indexes.Length - 1; j >= 0; j--)
                                if (indexes[j] >= 0)
                                {
                                    buttons[indexes[j] - fieldSize] = buttons[indexes[j]];
                                    buttons[indexes[j] - fieldSize].SetIndex(indexes[j] - fieldSize);
                                    buttons[indexes[j]].Move(dir);
                                }
                            if (indexes[0] >= 0)
                            {
                                buttons[indexes[0]] = temp;
                                emptyButtonIndex = indexes[0];
                                temp.SetIndex(emptyButtonIndex);
                            }
                            break;
                        }
                        else
                            break;
                    else if (!buttons[i].isEmptyCell)
                        indexes[currentIndex++] = buttons[i].index;
                    else if (buttons[i].isEmptyCell)
                    {
                        GameButton temp = buttons[emptyButtonIndex];
                        for (int j = indexes.Length - 1; j >= 0; j--)
                            if (indexes[j] >= 0)
                            {
                                buttons[indexes[j] - fieldSize] = buttons[indexes[j]];
                                buttons[indexes[j] - fieldSize].SetIndex(indexes[j] - fieldSize);
                                buttons[indexes[j]].Move(dir);
                            }
                        if (indexes[0] >= 0)
                        {
                            buttons[indexes[0]] = temp;
                            emptyButtonIndex = indexes[0];
                            temp.SetIndex(emptyButtonIndex);
                        }
                        break;
                    }
                }
                break;
            case SLIDE_DIRECTION.DOWN:
                for (int i = button.index; i < buttonCount; i += fieldSize)
                {
                    if (i + fieldSize >= buttonCount)
                        if (buttons[i].isEmptyCell)
                        {
                            GameButton temp = buttons[emptyButtonIndex];
                            for (int j = indexes.Length - 1; j >= 0; j--)
                                if (indexes[j] >= 0)
                                {
                                    buttons[indexes[j] + fieldSize] = buttons[indexes[j]];
                                    buttons[indexes[j] + fieldSize].SetIndex(indexes[j] + fieldSize);
                                    buttons[indexes[j]].Move(dir);
                                }
                            if (indexes[0] >= 0)
                            {
                                buttons[indexes[0]] = temp;
                                emptyButtonIndex = indexes[0];
                                temp.SetIndex(emptyButtonIndex);
                            }
                            break;
                        }
                        else
                            break;
                    else if (!buttons[i].isEmptyCell)
                        indexes[currentIndex++] = buttons[i].index;
                    if (buttons[i].isEmptyCell)
                    {
                        GameButton temp = buttons[emptyButtonIndex];
                        for (int j = indexes.Length - 1; j >= 0; j--)
                            if (indexes[j] >= 0)
                            {
                                buttons[indexes[j] + fieldSize] = buttons[indexes[j]];
                                buttons[indexes[j] + fieldSize].SetIndex(indexes[j] + fieldSize);
                                buttons[indexes[j]].Move(dir);
                            }
                        if (indexes[0] >= 0)
                        {
                            buttons[indexes[0]] = temp;
                            emptyButtonIndex = indexes[0];
                            temp.SetIndex(emptyButtonIndex);
                        }
                        break;
                    }
                }
                break;
            case SLIDE_DIRECTION.LEFT:
                for (int i = button.index; i >= row * fieldSize; i--)
                {
                    if ((i - 1 < 0 || i % fieldSize == 0))
                        if (buttons[i].isEmptyCell)
                        {
                            GameButton temp = buttons[emptyButtonIndex];
                            for (int j = indexes.Length - 1; j >= 0; j--)
                                if (indexes[j] >= 0)
                                {
                                    buttons[indexes[j] - 1] = buttons[indexes[j]];
                                    buttons[indexes[j] - 1].SetIndex(indexes[j] - 1);
                                    buttons[indexes[j]].Move(dir);
                                }
                            if (indexes[0] >= 0)
                                buttons[indexes[0]] = temp;
                            emptyButtonIndex = indexes[0];
                            temp.SetIndex(emptyButtonIndex);
                            break;
                        }
                        else
                            break;
                    else if (!buttons[i].isEmptyCell)
                        indexes[currentIndex++] = buttons[i].index;
                    if (buttons[i].isEmptyCell)
                    {
                        GameButton temp = buttons[emptyButtonIndex];
                        for (int j = indexes.Length - 1; j >= 0; j--)
                            if (indexes[j] >= 0)
                            {
                                buttons[indexes[j] - 1] = buttons[indexes[j]];
                                buttons[indexes[j] - 1].SetIndex(indexes[j] - 1);
                                buttons[indexes[j]].Move(dir);
                            }
                        if (indexes[0] >= 0)
                            buttons[indexes[0]] = temp;
                        emptyButtonIndex = indexes[0];
                        temp.SetIndex(emptyButtonIndex);
                        break;
                    }
                }
                break;
            case SLIDE_DIRECTION.RIGHT:
                for (int i = button.index; i <= ((row + 1) * fieldSize) - 1; i++)
                {
                    if ((i + 1 > buttonCount || (i + 1) % fieldSize == 0))
                        if (buttons[i].isEmptyCell)
                        {
                            GameButton temp = buttons[emptyButtonIndex];
                            for (int j = indexes.Length - 1; j >= 0; j--)
                                if (indexes[j] >= 0)
                                {
                                    buttons[indexes[j] + 1] = buttons[indexes[j]];
                                    buttons[indexes[j] + 1].SetIndex(indexes[j] + 1);
                                    buttons[indexes[j]].Move(dir);
                                }
                            if (indexes[0] >= 0)
                                buttons[indexes[0]] = temp;
                            emptyButtonIndex = indexes[0];
                            temp.SetIndex(emptyButtonIndex);
                            break;
                        }
                        else
                            break;
                    else if (!buttons[i].isEmptyCell)
                        indexes[currentIndex++] = buttons[i].index;
                    if (buttons[i].isEmptyCell)
                    {
                        GameButton temp = buttons[emptyButtonIndex];
                        for (int j = indexes.Length - 1; j >= 0; j--)
                            if (indexes[j] >= 0)
                            {
                                buttons[indexes[j] + 1] = buttons[indexes[j]];
                                buttons[indexes[j] + 1].SetIndex(indexes[j] + 1);
                                buttons[indexes[j]].Move(dir);
                            }
                        if (indexes[0] >= 0)
                            buttons[indexes[0]] = temp;
                        emptyButtonIndex = indexes[0];
                        temp.SetIndex(emptyButtonIndex);
                        break;
                    }
                }
                break;
        }

        Check();
    }
}
