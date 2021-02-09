using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SoundManager : MonoBehaviour
{
    public GameObject SoundsOff;
    public GameObject SoundsOn;
    public void TurnSoundsOff()
    {
        SoundsOn.SetActive(false);
        SoundsOff.SetActive(true);
    }

    public void TurnSoundsOn()
    {
        SoundsOff.SetActive(false);
        SoundsOn.SetActive(true);
    }
}
