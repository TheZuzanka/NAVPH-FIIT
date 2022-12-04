using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MarkDialog : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI markText;
    [SerializeField] Canvas canvas;

    private GameObject _boss;

    void Start()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss");
    }

    public void DisplayMark(string mark)
    {
        markText.SetText(mark);
    }

    // Continue fighting the boss to get a better mark
    public void Continue()
    {
        canvas.enabled = false;
        Time.timeScale = 1.0f;
    }

    // End the battle with boss with just obtained mark
    public void End()
    {
        canvas.enabled = false;
        Time.timeScale = 1.0f;
        Destroy(_boss);
    }
}
