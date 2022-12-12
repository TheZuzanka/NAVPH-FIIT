using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MarkDialog : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI markText;

    private Boss _boss;

    void Start()
    {
        _boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
    }

    public void DisplayMark(string mark)
    {
        markText.SetText(mark);
    }

    // Continue fighting the boss to get a better mark
    public void Continue()
    {
        Destroy(gameObject);
        Time.timeScale = 1.0f;
    }

    // End the battle with boss with just obtained mark
    public void End()
    {
        Destroy(gameObject);
        Time.timeScale = 1.0f;
        
        _boss.SetFinalMark();
        Destroy(_boss.gameObject);
    }
}
