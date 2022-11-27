using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public void DisplayTooltip(Button button)
    {
        // this method is called when hover over tooltip

        GameObject tooltip = button.transform.Find("Tooltip").gameObject;
        tooltip.SetActive(true);
    }

    public void HideTooltip(Button button)
    {
        // this method is called when no hover over tooltip

        GameObject tooltip = button.transform.Find("Tooltip").gameObject;
        tooltip.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}