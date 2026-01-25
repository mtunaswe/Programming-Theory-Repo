using UnityEngine;
using UnityEngine.EventSystems;
public class ConstructionPlot : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject greenFieldModel; // Drag your 3D Green Field model here

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

        // ENCAPSULATION: We only open the manager if the plot is free and not blocked by UI
        RadialMenuManager.Instance.Open(this);
    }
    public void BuildAtIndex(int index)
    {
        if (index >= buildableOptions.Length || isOccupied) return;

        // 1. Activate the building
        GameObject selectedBuilding = buildableOptions[index];
        selectedBuilding.SetActive(true);
        isOccupied = true;

        // 2. Deactivate the Green Field model
        if(greenFieldModel != null)
            greenFieldModel.SetActive(false);

        // 3. Polymorphism: Trigger unique building logic
        Building buildingScript = selectedBuilding.GetComponent<Building>();
        if (buildingScript != null)
        {
            buildingScript.ApplyBuildingEffect();
        }
    }
}