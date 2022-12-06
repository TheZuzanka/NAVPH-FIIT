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
        Sweetheart,
        Coffee
    }

    private void Start()
    {
        HighlightActive();
    }

    private void HighlightActive()
    {
        // highlights trait and person if selected when entering settings

        if (Settings.SelectedTrait != -1)
        {
            GameObject buttonsContainer = this.transform.Find("Attributes ScrollView/Viewport/Content").gameObject;
            Button selectedButton = buttonsContainer.transform.GetChild(Settings.SelectedTrait).gameObject
                .GetComponent<Button>();
            _selectedAttributeButton = selectedButton;
            Highlight(selectedButton);
        }

        if (Settings.SelectedPerson != -1)
        {
            GameObject buttonsContainer = this.transform.Find("Main Panel").gameObject;
            Button selectedButton = buttonsContainer.transform.GetChild(Settings.SelectedPerson).gameObject
                .GetComponent<Button>();
            _selectedPersonButton = selectedButton;
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

    private void RemovePreviousSelection(string buttonType)
    {
        // removes visual highlight from previously selected buttons

        if (buttonType == "attribute" && _selectedAttributeButton != null)
        {
            _selectedAttributeButton.GetComponent<Image>().enabled = false;
        }

        if ((buttonType == "person") & (_selectedPersonButton != null))
        {
            _selectedPersonButton.GetComponent<Image>().enabled = false;
        }
    }

    private void Highlight(Button button)
    {
        button.GetComponent<Image>().enabled = true;
    }

    public void SetAsSelectedAttributeButton(Button button)
    {
        // this method is called when attribute is selected
        
        RemovePreviousSelection("attribute");

        _selectedAttributeButton = button;
        selectedAttribute = (Attribute) Enum.Parse(typeof(Attribute), button.tag);

        Highlight(_selectedAttributeButton);
    }

    public void SetAsSelectedPersonButton(Button button)
    {
        // this method is called when person is selected

        RemovePreviousSelection("person");

        _selectedPersonButton = button;

        Highlight(_selectedPersonButton);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SetSuperSpeedAttribute()
    {
        Settings.SpeedMultiplier = 1.25f;
        Settings.FxTimeIntervalMultiplier = 1f;
        Settings.MaxHearts = 2;
        Settings.CoffeeTimeMultiplier = 1f;
    }

    private void SetLowerFxFrequencyAttribute()
    {
        Settings.SpeedMultiplier = 1f;
        Settings.FxTimeIntervalMultiplier = 1.5f;
        Settings.MaxHearts = 2;
        Settings.CoffeeTimeMultiplier = 1f;
    }

    private void AddExtraHeart()
    {
        Settings.SpeedMultiplier = 1f;
        Settings.FxTimeIntervalMultiplier = 1f;
        Settings.MaxHearts = 3;
        Settings.CoffeeTimeMultiplier = 1f;
    }

    private void SetExtraCoffeeTime()
    {
        Settings.SpeedMultiplier = 1f;
        Settings.FxTimeIntervalMultiplier = 1f;
        Settings.MaxHearts = 2;
        Settings.CoffeeTimeMultiplier = 1.2f;
    }

    public void SaveAttributes()
    {
        // this method is used on save settings

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
            case Attribute.Sweetheart:
                AddExtraHeart();
                break;
        }

        // save selected trait and person so it appears when reopening settings
        
        if (_selectedAttributeButton != null)
        {
            Settings.SelectedTrait = _selectedAttributeButton.transform.GetSiblingIndex();
        }

        if (_selectedPersonButton != null)
        {
            Settings.SelectedPerson = _selectedPersonButton.transform.GetSiblingIndex();
        }

        ReturnToMainMenu();
    }
}