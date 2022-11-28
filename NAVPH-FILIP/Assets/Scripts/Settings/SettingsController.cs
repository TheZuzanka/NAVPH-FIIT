using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // attributes used as placeholders before save, on save selected trait is saved to settings
    [SerializeField] private Attribute selectedAttribute;
    private Button _selectedAttributeButton;
    private Button _selectedPersonButton;

    private enum Attribute
    {
        Fitness,
        Logical,
        Heart,
        Coffee
    }

    private void Start()
    {
        HighlightActive();
    }

    private void HighlightActive()
    {
        // highlights trait and person if selected when entering settings
        
        if (Settings.Settings.SelectedTrait != -1)
        {
            GameObject buttonsContainer = this.transform.Find("Attributes ScrollView/Viewport/Content").gameObject;
            Button selectedButton = buttonsContainer.transform.GetChild(Settings.Settings.SelectedTrait).gameObject.GetComponent<Button>();
            Highlight(selectedButton);
        }
        
        if (Settings.Settings.SelectedPerson != -1)
        {
            GameObject buttonsContainer = this.transform.Find("Main Panel").gameObject;
            Button selectedButton = buttonsContainer.transform.GetChild(Settings.Settings.SelectedTrait).gameObject.GetComponent<Button>();
            Highlight(selectedButton);
        }
    }

    public void DisplayTooltip(Button button)
    {
        // this method is called when hover over tooltip (event trigger - Pointer Enter)

        GameObject tooltip = button.transform.Find("Image/Tooltip").gameObject;
        tooltip.SetActive(true);
    }

    public void HideTooltip(Button button)
    {
        // this method is called when no hover over tooltip (event trigger - Pointer xit)

        GameObject tooltip = button.transform.Find("Image/Tooltip").gameObject;
        tooltip.SetActive(false);
    }

    private void RemovePreviousSelection()
    {
        // removes visual highlight from previously selected buttons
        
        if (_selectedAttributeButton != null)
        {
            _selectedAttributeButton.GetComponent<Image>().enabled = false;
        }
        
        if (_selectedPersonButton != null)
        {
            _selectedPersonButton.GetComponent<Image>().enabled = false;
        }
    }

    public void Highlight(Button button)
    {
        button.GetComponent<Image>().enabled = true;
    }

    public void SetAsSelectedAttribute(string attribute)
    {
        // this method is called when attribute is selected
        // method does not appear if String and Button as parameters (??)

        RemovePreviousSelection();

        selectedAttribute = (Attribute) Enum.Parse(typeof(Attribute), attribute);
    }

    public void SetAsSelectedAttributeButton(Button button)
    {
        // this method is called when attribute is selected
        _selectedAttributeButton = button;

        Highlight(_selectedAttributeButton);
    }
    
    public void SetAsSelectedPersonButton(Button button)
    {
        // this method is called when person is selected
        
        RemovePreviousSelection();
        
        _selectedPersonButton = button;

        Highlight(_selectedPersonButton);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SetAttributesToDefault()
    {
        // sets attributes to values when no attribute was selected
        
        Settings.Settings.SpeedMultiplier = 1f;
        Settings.Settings.FxTimeIntervalMultiplier = 1f;
        Settings.Settings.MaxHearts = 2;
        Settings.Settings.CoffeeTimeMultiplier = 1f;
    }
    private void SetSuperSpeedAttribute()
    {
        Settings.Settings.SpeedMultiplier = 1.25f;
    }

    private void SetLowerFxFrequencyAttribute()
    {
        Settings.Settings.FxTimeIntervalMultiplier = 1.5f;
    }

    private void AddExtraHeart()
    {
        Settings.Settings.MaxHearts = 3;
    }

    private void SetExtraCoffeeTime()
    {
        Settings.Settings.CoffeeTimeMultiplier = 1.2f;
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
                SetExtraCoffeeTime();
                break;
            case Attribute.Logical:
                SetLowerFxFrequencyAttribute();
                break;
            case Attribute.Heart:
                AddExtraHeart();
                break;
        }

        // save selected trait so it appears when reopening settings
        Settings.Settings.SelectedTrait = _selectedAttributeButton.transform.GetSiblingIndex();
        
        ReturnToMainMenu();
    }
}