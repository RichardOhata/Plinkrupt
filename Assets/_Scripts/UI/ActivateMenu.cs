using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActivateMenu : MonoBehaviour
{
    public GameObject tmpPanel;
    public Button toggleButton;

    void Start()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(TogglePanel);
        }

        tmpPanel.SetActive(false);
    }

    public void TogglePanel()
    {
        if (tmpPanel != null)
        {
            tmpPanel.SetActive(!tmpPanel.activeSelf);
        }
    }
}
