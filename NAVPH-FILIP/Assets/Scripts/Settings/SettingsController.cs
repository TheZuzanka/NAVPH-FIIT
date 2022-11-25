using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public void DisplayTooltip(Button button)
    {
        GameObject tooltip = button.transform.Find("Tooltip").gameObject;
        tooltip.SetActive(true);
    }
    
    void Update () {
        //transform.position = Input.mousePosition + offset;
    }
}
