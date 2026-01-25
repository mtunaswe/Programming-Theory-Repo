using UnityEngine;

// This fulfills the INHERITANCE and ABSTRACTION requirements
public abstract class Building : MonoBehaviour
{
    [Header("Building Metadata")]
    [SerializeField] private string buildingName;
    [HideInInspector] public int constructionCost;

    [Header("Persistent Stats (Every 5 Seconds)")]
    [HideInInspector] public int incomeGeneration;   // Factory > Villa > House
    

    [Header("One-Time Impact")]
    [HideInInspector] public int  populationImpact; // House > Villa > Park/Factory(0)
    [HideInInspector] public float ecoScoreImpact;  // Park(+) > Factory(-) > House/Villa(small)

    // POLYMORPHISM: This 'abstract' method has no body here. 
    // It forces every child class to create its own unique version.
    public abstract void ApplyBuildingEffect();
    
    // ENCAPSULATION: We use a Property with a public 'get' but private 'set'
    // This protects the building's name from being changed by other scripts.
    public string BuildingName 
    { 
        get { return buildingName; } 
    }

   
}