using UnityEngine;

// This fulfills the INHERITANCE and ABSTRACTION requirements
public abstract class Building : MonoBehaviour
{
    [Header("Building Metadata")]
    [SerializeField] private string buildingName;
    public virtual int constructionCost => 0;

    // Persistent Stats (Every 5 Seconds)
    public virtual int incomeGeneration => 0;   // Factory > Villa > House
    

    // One-Time Impact
    public virtual int  populationImpact => 0; // House > Villa > Park/Factory(0)
    public virtual float ecoScoreImpact => 0f;  // Park(+) > Factory(-) > House/Villa(small)

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