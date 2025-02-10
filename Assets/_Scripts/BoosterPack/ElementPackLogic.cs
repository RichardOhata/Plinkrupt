using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ElementPackLogic : MonoBehaviour
{
    public ElementPack[] elements;
    public GameObject[] elementCardUI;


    // For Demo Purposes
    public void UpdateCards()
    {
        ElementPack[] randomElements = elements.OrderBy(x => Random.value).Take(3).ToArray();
        for (int index = 0; index < randomElements.Length; index++)
        {
            elementCardUI[index].GetComponent<ElementCardUI>().SetData(randomElements[index]);
        }
    }
}
