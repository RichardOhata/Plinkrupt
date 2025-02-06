using TMPro;
using UnityEngine;

public class ElementCardUI : MonoBehaviour
{
    public TextMeshProUGUI elementText;


    public void SetData(string elementName)
    {
        elementText.text = elementName;
    }
}
