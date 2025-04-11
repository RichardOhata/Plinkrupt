using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementsMultUI : MonoBehaviour
{
    public ElementMultiplierManager EMM;
    private List<ElementMultiplier> multList;
    private TMP_Text[] textComponents;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        multList = EMM.premanentMultipliers;
        textComponents = GetComponentsInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < multList.Count; i++)
        {
            textComponents[i].text = multList[i].multiplier.ToString() + "x";
        }
    }
}
