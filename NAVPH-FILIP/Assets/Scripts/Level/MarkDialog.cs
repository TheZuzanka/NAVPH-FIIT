using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MarkDialog : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI markText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button endButton;

    private Action onEndAction;
    private Action onContinueAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        onContinueAction?.Invoke();
        //Close();
    }

    public void End()
    {
        onEndAction?.Invoke();
        //Close();
    }

    public void SetMarkText(string mark)
    {
        markText.SetText(mark);
    }
}
