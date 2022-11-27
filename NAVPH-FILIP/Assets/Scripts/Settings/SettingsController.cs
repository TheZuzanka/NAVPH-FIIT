using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Attribute selectedAttribute;
    private Button _selectedButton;

    public enum Attribute
    {
        Fitness,
        Logical,
        Heart,
        Coffee
    }

    public void DisplayTooltip(Button button)
    {
        // this method is called when hover over tooltip

        GameObject tooltip = button.transform.Find("Image/Tooltip").gameObject;
        tooltip.SetActive(true);
    }

    public void HideTooltip(Button button)
    {
        // this method is called when no hover over tooltip

        GameObject tooltip = button.transform.Find("Image/Tooltip").gameObject;
        tooltip.SetActive(false);
    }

    private void RemovePreviousSelection()
    {
        if (_selectedButton == null)
        {
            return;
        }

        _selectedButton.GetComponent<Image>().enabled = false;
    }

    public void Highlight(Button button)
    {
        // this method is called when attribute is selected

        _selectedButton = button;
        _selectedButton.GetComponent<Image>().enabled = true;
    }

    public void SetAsSelected(Attribute attribute)
    {
        // this method is called when attribute is selected

        RemovePreviousSelection();

        selectedAttribute = attribute;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SetAttributesToDefault()
    {
        Settings.Settings.SpeedMultiplier = 1f;
    }
    private void SetSuperSpeedAttribute()
    {
        Settings.Settings.SpeedMultiplier = 1.5f;
    }

    public void SaveAttributes()
    {
        // this method is used on save settings
        
        SetAttributesToDefault();

        switch (selectedAttribute)
        {
            case Attribute.Fitness:
                SetSuperSpeedAttribute();
                break;
            case Attribute.Coffee:
                break;
            case Attribute.Logical:
                break;
            case Attribute.Heart:
                break;
        }
        
        ReturnToMainMenu();
    }
}