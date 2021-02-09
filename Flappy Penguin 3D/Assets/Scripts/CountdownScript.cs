using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    public delegate void CountdownFinished();
    public static event CountdownFinished OnCountdownFinished;

    public GameObject CountdownTemplate;
    public TMP_Text ReadyTMP;
    public TMP_Text GoTMP;

    private readonly WaitForSeconds waiter = new WaitForSeconds(1);

    private void OnEnable()
    {
        ReadyTMP.gameObject.SetActive(true);
        List<TMP_Text> TMPlist = new List<TMP_Text> { ReadyTMP, GoTMP };
        StartCoroutine("Countdown", TMPlist);      
    }

    private IEnumerator Countdown(List<TMP_Text> list)
    {
        yield return waiter;
        for (var i = 1; i < 2; i++)
        {
            list[i - 1].gameObject.SetActive(false);
            list[i].gameObject.SetActive(true);
            yield return waiter;
        }
        GoTMP.gameObject.SetActive(false);
        CountdownTemplate.SetActive(false);
        OnCountdownFinished();
    }
}
