using UnityEngine;

// This fulfills the INHERITANCE and ABSTRACTION requirements
public abstract class Building : MonoBehaviour
{
    [Header("Base Building Stats")]
    [SerializeField] private string buildingName;
    
    // ENCAPSULATION: We use a Property with a public 'get' but private 'set'
    // This protects the building's name from being changed by other scripts.
    public string BuildingName 
    { 
        get { return buildingName; } 
    }

    // POLYMORPHISM: This 'abstract' method has no body here. 
    // It forces every child class to create its own unique version.
    public abstract void ApplyBuildingEffect();
}