using DG.Tweening;
using UnityEngine;

public class FieldGenerator<T> where T: MonoBehaviour
{
    public T[] objects { get; private set; }
    public bool isFieldCreated { get; private set; }
    public int fieldSize { get; private set; }
    public int stepCount { get; private set; }

    public event System.Action onCompleteInstantiate, onCompleteFill, onReset;

    float step;
    float scaleRate;
    Vector3 initialPosition;
    Object CurrentPrefab;

    void InstantiateButtons(Transform parent = null)
    {
        if (parent == null)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i] = MonoBehaviour.Instantiate((GameObject)CurrentPrefab).GetComponent<T>();
                objects[i].name = "Element" + i;
                objects[i].transform.position = new Vector3(15, 0, -0.01f);
                objects[i].transform.DOScale(new Vector3(scaleRate, scaleRate, 1), 0f);
            }
        }
        else
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i] = MonoBehaviour.Instantiate((GameObject)CurrentPrefab, parent).GetComponent<T>();
                objects[i].name = "Element" + i;
                objects[i].transform.position = new Vector3(15, 0, -0.01f);
                objects[i].transform.DOScale(new Vector3(scaleRate, scaleRate, 1), 0f);
            }
        }
        if (onCompleteInstantiate != null)
            onCompleteInstantiate();
    }

    void FillField()
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].transform.position = new Vector3(15, 0, 0);

        for (int i = 0; i < objects.Length; i++)
        {
            if (i < fieldSize * fieldSize)
            {
                objects[i].gameObject.SetActive(true);
                if (i == 0)
                {
                    objects[i].transform.localPosition = initialPosition;
                    continue;
                }
                else if (i % fieldSize == 0 && i - 1 != 0)
                    objects[i].transform.localPosition = new Vector3(initialPosition.x, objects[i - 1].transform.localPosition.y - step, initialPosition.z);
                else
                    objects[i].transform.localPosition = new Vector3(objects[i - 1].transform.localPosition.x + step, objects[i - 1].transform.localPosition.y, initialPosition.z);
            }
            else
                objects[i].gameObject.SetActive(false);
        }

        if (onCompleteFill != null)
            onCompleteFill();
    }

    void ChangeScale()
    {
        for (int i = 0; i < objects.Length; i++)
            objects[i].transform.DOScale(new Vector3(scaleRate, scaleRate, 1), 0f);
    }

    public void CreateField(string prefabName, int fieldSize, int buttonsCount = 36, Transform parent = null) //размер поля
    {
        if (!isFieldCreated)
        {
            if (fieldSize * fieldSize > buttonsCount)
                throw new UnityException("Too big field size. Try another fieldsize (max fieldsize = 6)");
            isFieldCreated = true;
            CurrentPrefab = Resources.Load(prefabName);
            objects = new T[buttonsCount];
            InstantiateButtons(parent);
        }
        UpdateField(fieldSize);
    }

    public void UpdateField(int fieldSize)
    {
        SetVariables(fieldSize);
        FillField();
    }

    public void Reset()
    {
        FillField();
        if (onReset != null)
            onReset();
    }

    void SetVariables(int fieldSize)
    {
        switch (fieldSize)
        {
            case 3:
                step = 1.58f;
                this.fieldSize = fieldSize;
                if (scaleRate != 1)
                {
                    scaleRate = 1;
                    ChangeScale();
                }
                initialPosition = new Vector3(-1.57f, 0.58f, -0.01f);
                break;
            case 4:
                step = 1.18f;
                if (scaleRate != 0.75f)
                {
                    scaleRate = 0.75f;
                    ChangeScale();
                }
                this.fieldSize = fieldSize;
                initialPosition = new Vector3(-1.76f, 0.77f, -0.01f);
                break;
            case 5:
                step = 0.94f;
                this.fieldSize = fieldSize;
                if (scaleRate != 0.6f)
                {
                    scaleRate = 0.6f;
                    ChangeScale();
                }
                initialPosition = new Vector3(-1.87f, 0.75f, -0.01f);
                break;
            case 6:
                step = 0.78f;
                this.fieldSize = fieldSize;
                if (scaleRate != 0.5f)
                {
                    scaleRate = 0.5f;
                    ChangeScale();
                }
                initialPosition = new Vector3(-1.95f, 0.91f, -0.01f);
                break;
        }
    }
}
