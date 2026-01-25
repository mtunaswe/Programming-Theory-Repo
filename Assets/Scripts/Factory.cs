using UnityEngine;

public class Factory : Building
{
    public override void ApplyBuildingEffect()
    {
        Debug.Log($"[Construction] {BuildingName} built! Industrial production started. Eco-Score decreased.");
    }
}