using UnityEngine;
using UnityEngine.EventSystems;
public class ConstructionPlot : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject greenFieldModel; 

    [Header("Available Models in this Slot")]
    public GameObject[] buildableOptions; 
    
    // ENCAPSULATION: The private variable holds the data
    private bool isOccupied = false;

    // ENCAPSULATION: The public property allows reading but not writing from outside
    public bool IsOccupied 
    {
        get { return isOccupied; }
    }

    void OnMouseDown()
    {
        // ABSTRACTION: This check hides the complexity of UI/3D interaction
        // If the mouse is over a UI button, do NOTHING here.
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; 
        }

        if (isOccupied) return;

        RadialMenuManager.Instance.Open(this);
    }
    public void BuildAtIndex(int index)
    {
        if (index >= buildableOptions.Length || isOccupied) return;
        
        GameObject selectedBuilding = buildableOptions[index];
        Building buildingScript = selectedBuilding.GetComponent<Building>();

        // ABSTRACTION: Check if player has enough money
        if (DataManager.Instance.CanAfford(buildingScript.constructionCost))
        {
            // Spend the money
            DataManager.Instance.SpendMoney(buildingScript.constructionCost);
            
            // 1. Activate the building
            selectedBuilding.SetActive(true);
            isOccupied = true;

            // 2. Deactivate the Green Field model
            if (greenFieldModel != null) greenFieldModel.SetActive(false);
            
            // 3. Polymorphism: Trigger unique building logic
            if (buildingScript != null)
            {
            buildingScript.ApplyBuildingEffect();
            }
        }
        else
        {
            Debug.Log("Not enough money to build this!");
        }
    }

}