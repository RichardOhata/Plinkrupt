using System;
using System.Collections.Generic;
using Esper.ESave;
using UnityEngine;
using UnityEngine.Assertions;


public class ElementMultiplierManager: MonoBehaviour
{
    public static ElementMultiplierManager Instance;
    
    //a list that holds all the premanent element multipliers from the cards
    public List<ElementMultiplier> premanentMultipliers;
    
    public event Action<ElementMultiplier> MultiplierAddedEvent;
    public event Action<ElementMultiplier> MultiplierRemovedEvent;

    //save class

    private SaveFile _saveFile;
    
    public void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Debug.Log("Element Multiplier Manager instance already exists, destroying this one.");
            Destroy(this.gameObject);
        }
    }
    private void Awake()
    {
        _saveFile = GetComponent<SaveFileSetup>().GetSaveFile();
        Assert.IsNotNull(_saveFile, "no Save File Setup attached to Element Multiplier Manager");
        
        //load permanent multipliers
        LoadPremanentMultipliters();
    }

    /// <summary>
    /// Loads all permanent multipliers from the list and triggers the OnMultiplierAddedEvent for each one.
    /// </summary>
    public void LoadPremanentMultipliters()
    {
        if(_saveFile.HasData("PermanentMultipliers")){
            premanentMultipliers = _saveFile.GetData<List<ElementMultiplier>>("PermanentMultipliers");
            foreach (var multipliers in premanentMultipliers)
            {
                OnMultiplierAddedEvent(multipliers);
            }
        }
        else{
            premanentMultipliers = new List<ElementMultiplier>();
        }
        

    }
/// <summary>
/// Adds the given element multiplier to the list of permanent multipliers 
/// and invokes the MultiplierAddedEvent.
/// </summary>
/// <param name="element">The element multiplier to be added.</param>
    public void OnMultiplierAddedEvent(ElementMultiplier element)
    {
        premanentMultipliers.Add(element);
        Debug.Log(string.Format("Element Multiplier {0} added to the list of permanent multipliers", element.ElementBuffName));
        MultiplierAddedEvent?.Invoke(element);
    }

    public void OnMultiplierAddedEvent(ScoringClass.ElementType elementType, float multiplier)
    {
        ElementMultiplier existingElement = premanentMultipliers.Find(e => e.elementType == elementType);

        if (existingElement != null)
        {
            existingElement.multiplier += multiplier;
            Debug.Log($"Updated Element Multiplier {existingElement.ElementBuffName}: New Multiplier = {existingElement.multiplier}");
        }
        else
        {
            ElementMultiplier newElement = new ElementMultiplier(elementType, multiplier);
            premanentMultipliers.Add(newElement);
            Debug.Log($"Element Multiplier {newElement.ElementBuffName} added to the list of permanent multipliers");
        }

        // Invoke event if needed
        MultiplierAddedEvent?.Invoke(existingElement ?? premanentMultipliers[^1]);

        // Save new permanent multipliers
        _saveFile.AddOrUpdateData<List<ElementMultiplier>>("PermanentMultipliers", premanentMultipliers);
        _saveFile.Save();
    }
    

    public void OnMultiplierRemovedEvent(ElementMultiplier element)
    {
        //remove all element multipliers
        premanentMultipliers.Remove(element);
        Debug.Log(string.Format("Element Multiplier {0} added to the list of permanent multipliers", element.ElementBuffName));
        MultiplierRemovedEvent?.Invoke(element);
        
        //save permanent multipliers
        _saveFile.AddOrUpdateData<List<ElementMultiplier>>("PermanentMultipliers", premanentMultipliers);
        _saveFile.Save();
    }

    public void OnMultiplierRemovedEvent(ScoringClass.ElementType elementType, float multiplierModifier)
    {
        //remove element multiplier by deducting the multiplier with the given modifier
        ElementMultiplier element = new ElementMultiplier(elementType, multiplierModifier);

        //Find the target element multiplier and deduct the modifier
        premanentMultipliers.Find(e => e.elementType == elementType).multiplier -= multiplierModifier;
        Debug.Log(string.Format("Element Multiplier {0} added to the list of permanent multipliers", element.ElementBuffName));
        MultiplierRemovedEvent?.Invoke(element);

        //save permanent multipliers
        _saveFile.AddOrUpdateData<List<ElementMultiplier>>("PermanentMultipliers", premanentMultipliers);
        _saveFile.Save();
    }
}
