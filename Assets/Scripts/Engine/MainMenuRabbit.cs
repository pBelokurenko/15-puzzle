using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MainMenuRabbit : MonoBehaviour
{
    public Transform hands, body, point;

    void Start()
    {
        StartCoroutine("Show");
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(2);
        hands.DOMove(point.transform.position, 3f)
            .OnComplete(() => body.DOMove(point.transform.position, 2f));
    }
}
