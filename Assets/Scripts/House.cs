using UnityEngine;

public class House : Building
{
    public override void ApplyBuildingEffect()
    {
        // For now, only a debug log as requested
        Debug.Log($"[Construction] {BuildingName} built! Housing capacity increased.");
    }
}