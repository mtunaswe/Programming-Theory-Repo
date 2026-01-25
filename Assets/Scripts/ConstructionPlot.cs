using UnityEngine;

public class ConstructionPlot : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject greenFieldModel; // Drag your 3D Green Field model here

    [Header("Available Models in this Slot")]
    public GameObject[] buildableOptions; 
    
    private bool isOccupied = false;

    void OnMouseDown()
    {
        if (isOccupied) return;
        ShowBuildMenu();
    }

    void ShowBuildMenu()
    {
        // This is where you will trigger your Radial Menu later
        // For now, let's test building the first option
        BuildAtIndex(0); 
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