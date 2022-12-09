using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchiemevemtsContoller : MonoBehaviour
{
    [SerializeField] private List<Image> imageElements;

    private void SetCompleted()
    {
        var index = 0;
        
        foreach (var element in imageElements)
        {
            if (Achievements.achievements.ElementAt(index).Value)
            {
                element.color = Color.green;
            }

            index++;
        }
        
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        SetCompleted();
    }
}
