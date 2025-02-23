using UnityEngine;



public class ShopWindow : MonoBehaviour
{
    public GameObject useDropPanel;
    public void ShowUsePanel()
    {
        useDropPanel.SetActive(true);
    }

    public void HideUsePanel()
    {
        useDropPanel.SetActive(false);
    }
}
