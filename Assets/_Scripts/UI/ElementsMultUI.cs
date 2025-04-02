using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementsMultUI : MonoBehaviour
{
    public ElementMultiplierManager EMM;
    private List<ElementMultiplier> multList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        multList = EMM.premanentMultipliers;
    }

    // Update is called once per frame
    void Update()
    {
         TMP_Text[] textComponents = GetComponentsInChildren<TMP_Text>();

        for (int i = 0; i < multList.Count; i++)
        {
            textComponents[i].text = multList[i].multiplier.ToString();
        }
    }
}
