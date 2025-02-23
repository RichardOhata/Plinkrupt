using System;
using System.Collections.Generic;
using UnityEngine;


public class ElementMultiplierManager: MonoBehaviour
{
    public static ElementMultiplierManager Instance;
    
    //a list that holds all the premanent element multipliers from the cards
    public List<ElementMultiplier> premanentMultipliers = new List<ElementMultiplier>();
    
    public event Action<ElementMultiplier> MultiplierAddedEvent;
    public event Action<ElementMultiplier> MultiplierRemovedEvent;
    
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

    /// <summary>
    /// Loads all permanent multipliers from the list and triggers the OnMultiplierAddedEvent for each one.
    /// </summary>
    public void LoadPremanentMultipliters()
    {
        foreach (var multipliers in premanentMultipliers)
        {
            OnMultiplierAddedEvent(multipliers);
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
    }
    
/// <summary>
/// Removes the specified element multiplier from the list of permanent multipliers 
/// and invokes the MultiplierRemovedEvent.
/// </summary>
/// <param name="element">The element multiplier to be removed.</param>
    public void OnMultiplierRemovedEvent(ElementMultiplier element)
    {
        premanentMultipliers.Remove(element);
        Debug.Log(string.Format("Element Multiplier {0} added to the list of permanent multipliers", element.ElementBuffName));
        MultiplierRemovedEvent?.Invoke(element);
    }

    public void OnMultiplierRemovedEvent(ScoringClass.ElementType elementType, float multiplier)
    {
        ElementMultiplier element = new ElementMultiplier(elementType, multiplier);
        premanentMultipliers.Remove(element);
        Debug.Log(string.Format("Element Multiplier {0} added to the list of permanent multipliers", element.ElementBuffName));
        MultiplierRemovedEvent?.Invoke(element);
    }
}
