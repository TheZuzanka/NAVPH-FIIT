using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]private string selectedAttribute;
    private Button _selectedButton;
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
        _selectedButton = button;
        _selectedButton.GetComponent<Image>().enabled = true;
    }
    public void SetAsSelected(string attribute)
    {
        RemovePreviousSelection();
        
        selectedAttribute = attribute;
    }

    private void SetSuperSpeedAttribute()
    {
        Settings.Settings.SpeedMultiplier = 1.5f;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}