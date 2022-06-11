using System;
using UnityEngine;
using TMPro;

public class Display : MonoBehaviour
{
    private TextMeshPro timeTMP;

    private void Awake()
    {
        timeTMP = GetComponent<TextMeshPro>();
    }
    
    public void SetTime(DateTime time)
    {
        timeTMP.text = time.ToString("HH:mm:ss") + "\n" + time.ToString("d");

    }
}
