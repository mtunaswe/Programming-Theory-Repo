using UnityEngine;

public class Park : Building
{
    public override void ApplyBuildingEffect()
    {
        Debug.Log($"[Construction] {BuildingName} built! Citizens are happier. Eco-Score increased.");
    }
}