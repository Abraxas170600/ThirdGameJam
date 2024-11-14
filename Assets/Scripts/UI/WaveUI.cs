using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public TMP_Text waveText;
    public void ChangeWaveText(int waveIndex)
    {
        waveText.text = $"Wave {waveIndex}";
    }
}
